//#define DEBUG
//#define DEBUG_SET_ALL_FIELDS_FULL_GROWTH

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Description:    Holds main game field data (array representation) and handles interaction with it\n
/// Author:         Marc Fischer, Manuel Hagen\n
/// </summary>
public class FarmlandManager : MonoBehaviour
{
    private Level1GameManager _gameManager;

    // Floor Variables
    [SerializeField] private FloorSquare[,] FloorSquareArray;
    [SerializeField] private uint _squareCount;
    [SerializeField] private float _areaSize;
    private float _squareSize;
    private readonly float _colliderHeight = 1.0f;
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Transform _centerPoint;

    // Particle Variables
    [SerializeField] private ParticleSystem _seeds;
    private List<ParticleCollisionEvent> _collisionEvents;
    [SerializeField] private GameObject _beeParticleSystemPrefab;

    // Growth Sate Thresholds
    private int LOW_GROWTH_THRESHOLD = 10; // depends on seedSaturation
    private int MID_GROWTH_THRESHOLD = 2; // depends on time in seconds
    private int HIGH_GROWTH_THRESHOLD = 4; // depends on time in seconds

    // Growing Variables
    [SerializeField] private float _growthTicksPerSecond = 10f;
    [SerializeField] private float _growthTicksPerSecondHard = 5f;
    private float _growthTimeSinceLastTick = 0f;
    private readonly Dictionary<Vector2Int, float> _growingSquares = new();

    // Animal Variables
    private readonly List<Vector2Int> _targetSquares = new();
    [SerializeField] private int _targetLimit = 3;
    private int _currentApproachingAnimals;

    // Flower Variables
    [SerializeField] private Growable[] _flowers;
    [SerializeField] private Growable[] _plants;
    private const float Y_OFFSET = 0.015f;

    // Score Variables
    [SerializeField] private float _highGrowthScore, _midGrowthScore, _lowGrowthScore;

    // Structs and Enums
    [System.Serializable]
    [SerializeField]
    private struct Growable
    {
        public float minYScaleFactor;
        public float maxYScaleFactor;
        public GameObject model;
    }

    private struct FloorSquare
    {
        public int seedSaturation;
        public GROWTH_STATE growthState;
        public GameObject tile;
    }

    enum GROWTH_STATE
    {
        NO_GROWTH,
        LOW_GROWTH,
        MID_GROWTH,
        HIGH_GROWTH
    }


    /* -------------------------------------------------------------------------- */
    /*                                Unity Methods                               */
    /* -------------------------------------------------------------------------- */


    private void Start()
    {
        if (_gameManager == null)
        {
            _gameManager = Level1GameManager.GetInstance();
        }
        _collisionEvents = new List<ParticleCollisionEvent>();
        InitFloorSquareArray();
        InitBoxCollider();
        _centerPoint.SetLocalPositionAndRotation(new Vector3(transform.position.x + _areaSize/2, 0, transform.position.z + _areaSize / 2),Quaternion.identity);
        _currentApproachingAnimals = 0;
        if (_gameManager.hardMode)
        {
            _growthTicksPerSecond = _growthTicksPerSecondHard;
            LOW_GROWTH_THRESHOLD *= 2;
            MID_GROWTH_THRESHOLD *= 2;
            HIGH_GROWTH_THRESHOLD *= 2;
        }
    }

    private void Update()
    {
        _growthTimeSinceLastTick += Time.deltaTime;
        if (_growthTimeSinceLastTick > 1 / _growthTicksPerSecond)
        {
            UpdateGrowingSquares();
            _growthTimeSinceLastTick = 0;
        }
    }

    /// <summary>
    /// Description:    Handles seed particle collision\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="other">Seed particles</param>
    private void OnParticleCollision(GameObject other)
    {
        _seeds.GetCollisionEvents(gameObject, _collisionEvents);

        Vector3 pointOfCollision = _collisionEvents[0].intersection;
        Vector2Int arrayPosition = ConvertWorldPositionToArrayPosition(pointOfCollision.x, pointOfCollision.z);

        AddSeedSaturation(arrayPosition);
    }


    /* -------------------------------------------------------------------------- */
    /*                                Init Methods                                */
    /* -------------------------------------------------------------------------- */


    /// <summary>
    /// Description:    Initializes an array representation of the game's field & spawns ground Tiles\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    private void InitFloorSquareArray()
    {
        UpdateSquareSize();
        float squareHalf = _squareSize / 2;
        FloorSquareArray = new FloorSquare[_squareCount, _squareCount];

        for (int i = 0; i < _squareCount; i++)
        {
            for (int j = 0; j < _squareCount; j++)
            {
                FloorSquare currentFloorSquare = new()
                {
                    seedSaturation = 0,
                    growthState = GROWTH_STATE.NO_GROWTH
                };

                GameObject currentTile = Instantiate(_tilePrefab, Vector3.zero, Quaternion.Euler(0.0f, (int)Random.Range(0f, 3f) * 90, 0.0f), this.transform);
                currentTile.transform.localPosition = new Vector3(i * _squareSize + squareHalf, 0.01f, j * _squareSize + squareHalf);
                currentTile.transform.localScale = new Vector3(_squareSize, _squareSize * Random.Range(0.3f, 0.95f), _squareSize);
                currentTile.name = $"Tile_{i}_{j}";
                currentFloorSquare.tile = currentTile;
                FloorSquareArray[i, j] = currentFloorSquare;
#if DEBUG_SET_ALL_FIELDS_FULL_GROWTH
if (Random.Range(0f, 1f) > 0.00f)
{
    Vector2Int arrayPosition = new(i, j);
    GrowFloor(arrayPosition);
    _growingSquares.Remove(arrayPosition);
    _growingSquares.Add(arrayPosition, Time.time);
    UpdateSquare(arrayPosition);
}
#endif
            }
        }
    }

    /// <summary>
    /// Description:    Sets box collider position and size to fit the game field\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    private void InitBoxCollider()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.size = new Vector3(_areaSize, _colliderHeight, _areaSize);
        boxCollider.center = new Vector3(_areaSize / 2, 0.01f - _colliderHeight / 2, _areaSize / 2);

        BoxCollider triggerCollider = gameObject.AddComponent<BoxCollider>();
        triggerCollider.size = boxCollider.size;
        triggerCollider.center = new Vector3 (boxCollider.center.x, boxCollider.center.y + 0.1f, boxCollider.center.z);
        triggerCollider.isTrigger = true;
    }

    /// <summary>
    /// Description:    Calculates size of an individual square by using the total area size and square count\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    private void UpdateSquareSize()
    {
        _squareSize = (float)_areaSize / _squareCount;
    }


    /* -------------------------------------------------------------------------- */
    /*                                   Methods                                  */
    /* -------------------------------------------------------------------------- */


    /// <summary>
    /// Description:    Add +1 seed saturation to a field at given array position dependent of growth state\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array position</param>
    private void AddSeedSaturation(Vector2Int arrayPosition)
    {
        ref FloorSquare currentSquare = ref FloorSquareArray[arrayPosition.x, arrayPosition.y];
        if (currentSquare.growthState != GROWTH_STATE.NO_GROWTH)
        {
            return;
        }

        currentSquare.seedSaturation++;

        if (currentSquare.seedSaturation >= LOW_GROWTH_THRESHOLD)
        {
            GrowFloor(arrayPosition);
            _growingSquares.Remove(arrayPosition);
            _growingSquares.Add(arrayPosition, Time.time);
            UpdateSquare(arrayPosition);
        }
    }

    /// <summary>
    /// Description:    Updates all growing squares in the _growingSquares dictionary, dependent on time and threshold values. Removes fields that are finished\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    private void UpdateGrowingSquares()
    {
        List<Vector2Int> squaresToRemove = new();

        foreach (KeyValuePair<Vector2Int, float> kvp in _growingSquares)
        {
            float timeExpired = Time.time - kvp.Value;

            if (timeExpired >= HIGH_GROWTH_THRESHOLD)
            {
                GrowFloor(kvp.Key);
                UpdateSquare(kvp.Key);
                squaresToRemove.Add(kvp.Key);
            }
            else if (timeExpired >= MID_GROWTH_THRESHOLD)
            {
                if (FloorSquareArray[kvp.Key.x, kvp.Key.y].growthState != GROWTH_STATE.LOW_GROWTH)
                    return;

                GrowFloor(kvp.Key);
                UpdateSquare(kvp.Key);

                _targetSquares.Add(kvp.Key);
            }
        }

        foreach (Vector2Int arrayPosition in squaresToRemove)
        {
            _growingSquares.Remove(arrayPosition);
        }
    }

    /// <summary>
    /// Description:    Increase growth state of a field at given array position\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array position</param>
    private void GrowFloor(Vector2Int arrayPosition)
    {
        ref FloorSquare currentSquare = ref FloorSquareArray[arrayPosition.x, arrayPosition.y];
        if (currentSquare.growthState != GROWTH_STATE.HIGH_GROWTH)
        {
            currentSquare.growthState++;
        }
    }

    /// <summary>
    /// Description:    Updates field on given array position based on its growth state\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array Position</param>
    private void UpdateSquare(Vector2Int arrayPosition)
    {
        switch (FloorSquareArray[arrayPosition.x, arrayPosition.y].growthState)
        {
            case GROWTH_STATE.NO_GROWTH:
                ResetSquareAt(arrayPosition);
                break;
            case GROWTH_STATE.LOW_GROWTH:
                for (int i = 0; i < 5; i++)
                {
                    CreateGrowableAt(arrayPosition, _plants);
                }
                break;
            case GROWTH_STATE.MID_GROWTH:
                for (int i = 0; i < 5; i++)
                {
                    CreateGrowableAt(arrayPosition, _flowers);
                }
                break;
            case GROWTH_STATE.HIGH_GROWTH:
                if (FloorSquareArray[arrayPosition.x, arrayPosition.y].tile.transform.childCount >= 15)
                {
                    // needed to prevent an out of bounds error
                    return;
                }
                for (int i = 0; i < 5; i++)
                {
                    CreateGrowableAt(arrayPosition, _flowers);
                }
                // spawn a bee with 50%
                if (Random.Range(0f, 1f) > 0.5f)
                {
                    GameObject beeParticles = Instantiate(_beeParticleSystemPrefab, Vector3.zero, Quaternion.identity, transform);
                    beeParticles.name = $"BeeParticles_{arrayPosition.x}_{arrayPosition.y}";
                    beeParticles.transform.position = new Vector3(arrayPosition.x * _squareSize, transform.position.y + 1f, arrayPosition.y * _squareSize);
                    beeParticles.transform.SetParent(FloorSquareArray[arrayPosition.x, arrayPosition.y].tile.transform);
                }
                break;
        }
    }

    /// <summary>
    /// Description:    Resets state of field at given array position (resets seed saturation & growth state + deletes child objects) and removes it from the growing list dependent of its prior state\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array position</param>
    private void ResetSquareAt(Vector2Int arrayPosition)
    {
        ref FloorSquare currentSquare = ref FloorSquareArray[arrayPosition.x, arrayPosition.y];

        if (currentSquare.growthState == GROWTH_STATE.MID_GROWTH)
        {
            _growingSquares.Remove(arrayPosition);
        }

        currentSquare.seedSaturation = 0;

        foreach (Transform child in currentSquare.tile.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Description:    Spawns a growable object at given array position. Object model, scale and position on the field are randomized in specified ranges\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array position</param>
    /// <param name="growables">Array of growables</param>

    private void CreateGrowableAt(Vector2Int arrayPosition, Growable[] growables)
    {
        Growable currentGrowableStruct = growables[(int)Random.Range(0.0f, growables.Length - 1)];
        GameObject currentGrowableObject = Instantiate(currentGrowableStruct.model, Vector3.zero, Quaternion.Euler(0.0f, Random.Range(0f, 360f), 0.0f), transform);
        currentGrowableObject.transform.position = GenerateRandomSpawnPositionAt(arrayPosition);
        currentGrowableObject.transform.localScale = GenerateRandomScaleFor(currentGrowableStruct);
        currentGrowableObject.name = $"Growable_{currentGrowableStruct.model.name}";
        currentGrowableObject.transform.SetParent(FloorSquareArray[arrayPosition.x, arrayPosition.y].tile.transform);
    }

    /// <summary>
    /// Description:    Generates a random spawn position on a field at a given array position\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array position</param>
    /// <returns>Position on a field at a given array position as Vector3</returns>
    private Vector3 GenerateRandomSpawnPositionAt(Vector2Int arrayPosition)
    {
        return new Vector3(
            arrayPosition.x * _squareSize + Random.Range(0.0f, _squareSize) + transform.position.x,
            transform.position.y + Y_OFFSET,
            arrayPosition.y * _squareSize + Random.Range(0.0f, _squareSize) + transform.position.z);
    }

    /// <summary>
    /// Description:    Generates a random scale for a flower within a specified min and max range\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="flower">Flower</param>
    /// <returns>Scale for a flower as Vector3</returns>
    private Vector3 GenerateRandomScaleFor(Growable flower)
    {
        return new Vector3(1f, 1f * Random.Range(flower.minYScaleFactor, flower.maxYScaleFactor), 1f);
    }

    /// <summary>
    /// Description:    Convert world position to array position without y position\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="xPos">x Position</param>
    /// <param name="zPos">z Position</param>
    /// <returns>Converted array position as Vector2Int</returns>
    private Vector2Int ConvertWorldPositionToArrayPosition(float xPos, float zPos)
    {
        return new Vector2Int(Mathf.Clamp((int)(xPos / _squareSize - transform.position.x), 0, (int)_squareCount-1), 
            Mathf.Clamp((int)(zPos / _squareSize - transform.position.z), 0, (int)_squareCount-1));
    }

    /// <summary>
    /// Description:    Convert array position to world position with y position from transform\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="arrayPosition">Array Position</param>
    /// <returns>Converted world position as Vector3</returns>
    private Vector3 ConvertArrayPositionToWorldPosition(Vector2Int arrayPosition)
    {
        return new Vector3(arrayPosition.x * _squareSize, transform.position.y, arrayPosition.y * _squareSize) + transform.position;
    }

    /// <summary>
    /// Description:    Selects a field stored as a possible target and returns its position\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <returns>Position of a target field as Vector3 or null if no field is available</returns>
    public Vector3? GetTargetPosition()
    {
        if (_targetSquares.Count == 0) return null;

        if(_currentApproachingAnimals >= _targetLimit) return null;
        _currentApproachingAnimals++;

        Vector2Int arrayPosition = _targetSquares[(int)Random.Range(0f, _targetSquares.Count - 1f)];
        _targetSquares.Remove(arrayPosition);

        float squareHalf = _squareSize / 2;
        Vector3 targetPosition = ConvertArrayPositionToWorldPosition(arrayPosition);
        return new Vector3(targetPosition.x + squareHalf, targetPosition.y, targetPosition.z + squareHalf);
    }

    /// <summary>
    /// Description:    Adds the target back in the target array, when an animal was petted\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="position">World position of the target</param>
    public void PutBackTargetPosition(Vector3 position)
    {
        _targetSquares.Add(ConvertWorldPositionToArrayPosition(position.x,position.z));
        _currentApproachingAnimals--;
    }

    /// <summary>
    /// Description:    Removes a fields content at a given position, when an animal has eaten\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <param name="position">World position of the target</param>
    public void Eat(Vector3 position)
    {
#if DEBUG
        Debug.Log("Animal has eaten");
#endif
        _currentApproachingAnimals--;
        Vector2Int arrayPosition = ConvertWorldPositionToArrayPosition(position.x, position.z);
        FloorSquareArray[arrayPosition.x, arrayPosition.y].growthState = GROWTH_STATE.NO_GROWTH;
        UpdateSquare(arrayPosition);
    }

    /// <summary>
    /// Description:    Calculates the number of points of current game fields growth states\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    /// <returns>Score count for all the fields</returns>
    public int GetScore()
    {
        float maxScore = _squareCount * _squareCount * _highGrowthScore;
        float currentScore = 0f;
        for (int i = 0; i < _squareCount; i++)
        {
            for (int j = 0; j < _squareCount; j++)
            {
                switch (FloorSquareArray[i, j].growthState)
                {
                    case GROWTH_STATE.LOW_GROWTH:
                        currentScore += _lowGrowthScore;
                        break;
                    case GROWTH_STATE.MID_GROWTH:
                        currentScore += _midGrowthScore;
                        break;
                    case GROWTH_STATE.HIGH_GROWTH:
                        currentScore += _highGrowthScore;
                        break;
                }
            }
        }
        gameObject.GetComponent<BoxCollider>().enabled = false;
        return (int)(currentScore/maxScore*100f);
    }


    /* -------------------------------------------------------------------------- */
    /*                                Debug Methods                               */
    /* -------------------------------------------------------------------------- */


    /// <summary>
    /// Description:    Visualize generation parameters for field/array in scene view\n
    /// Author:         Marc Fischer, Manuel Hagen\n
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var squareSizeDebug = (float)_areaSize / _squareCount;
        var height = transform.position.y;

        //Corners
        var lowerLeftCorner = new Vector3(transform.position.x, height, transform.position.z);
        var lowerRightCorner = new Vector3(lowerLeftCorner.x + _areaSize, height, lowerLeftCorner.z);
        var upperLeftCorner = new Vector3(lowerLeftCorner.x, height, lowerLeftCorner.z + _areaSize);
        var upperRightCorner = new Vector3(lowerRightCorner.x, height, upperLeftCorner.z);

        //Draw Grid

        // Vertical lines
        for (int i = 0; i < _squareCount; i++)
        {
            var startPoint = new Vector3(lowerLeftCorner.x + squareSizeDebug * i, height, lowerLeftCorner.z);
            var endPoint = new Vector3(upperLeftCorner.x + squareSizeDebug * i, height, upperLeftCorner.z);
            Gizmos.DrawLine(startPoint, endPoint);
        }
        // Horizontal lines
        for (int i = 0; i < _squareCount; i++)
        {
            var startPoint = new Vector3(lowerLeftCorner.x, height, lowerLeftCorner.z + squareSizeDebug * i);
            var endPoint = new Vector3(lowerRightCorner.x, height, lowerRightCorner.z + squareSizeDebug * i);
            Gizmos.DrawLine(startPoint, endPoint);
        }

        // Draw the missing lines to connect the corners of the area
        Gizmos.DrawLine(upperLeftCorner, upperRightCorner);
        Gizmos.DrawLine(lowerRightCorner, upperRightCorner);
    }
}
