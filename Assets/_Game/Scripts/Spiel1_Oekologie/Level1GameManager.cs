using System.Collections.Generic;
using UnityEngine;
using TMPro;
using _Game.Scripts.Modules.DialogManager;
using _Game.Scripts.Modules.SoundManager;
using static UnityEngine.Rendering.ReloadAttribute;

/// <summary>
/// Description: Manages the gameplay of Level 1, including timing, scoring, and interactions with the farmland and animals.
/// Authors: Marc Fischer, Manuel Hagen
/// </summary>
public class Level1GameManager : MonoBehaviour
{
    private static Level1GameManager _gameManager;
    
    [SerializeField] private FarmlandManager _farmlandManager;
    [SerializeField] private List<Transform> _animalSpawnPoints;
    [SerializeField] private float _gameDurationInSeconds;
    [SerializeField] private DialogManager _dialogManager;

    [SerializeField] private AudioSource _backgroundMusic;
    [SerializeField] private SoundPackageSo _backgroundSoundpack;
    [SerializeField] private SoundPackageSo _sfxSoundpack;

    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private bool _gameRunning;
    private float _gameStartTime;

    private bool _20SecondsRemainingReminded;
    private bool _halfTimeReminded;
    private bool _after30seconds;
    private bool _after100seconds;

    private int _score;
    public bool hardMode { get; private set; }

    /// <summary>
    /// Description: Retrieves the instance of the Level1GameManager.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <returns>The instance of the Level1GameManager</returns>
    public static Level1GameManager GetInstance() 
    {
        return _gameManager;
    }

    private void Awake()
    {
        hardMode = PlayerPrefs.GetInt("difficultyLevel") == 1;
        _gameManager = this;
        _timeText.text = "Noch " + _gameDurationInSeconds.ToString() + " Sekunden";
        if (_farmlandManager == null)
        {
            Debug.LogError("Critical error: no FarmlandManager Reference");
            _farmlandManager = GameObject.Find("Farmland").GetComponent<FarmlandManager>();
        }
    }

    private void Start()
    {
        SoundManager.GetInstance.BackgroundPackageSo = _backgroundSoundpack;
        SoundManager.GetInstance.SfxPackageSo = _sfxSoundpack;
        _dialogManager.NextDialog("Spiel1_Erklärung1");
        SoundManager.GetInstance.StartBackground("parkAmbience");
    }

    public void Update()
    {
        if (_gameRunning)
        {
            GameLoop();
        }
    }

    public void StartGame()
    {
        if (_gameRunning)
        {
            return;
        }
        _gameStartTime = Time.time;
        _gameRunning = true;
        _backgroundMusic.Play();
    }

    /// <summary>
    /// Description: Executes the main game loop, updating the game state and handling time-based events.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    private void GameLoop()
    {
        float timeDelta = Time.time - _gameStartTime;
        // refresh time text and show time left in seconds
        _timeText.text = "Noch " + (_gameDurationInSeconds - Mathf.Round(timeDelta)).ToString() + " Sekunden";

        if (timeDelta >= _gameDurationInSeconds)
        {
            StopGame();
            return;
        }

        if (timeDelta >= 30 && !_after30seconds)
        {
            _dialogManager.NextDialog("Spiel1_Alert1");
            _after30seconds = true;
        }

        if (timeDelta >= 100 && !_after100seconds)
        {
            _dialogManager.NextDialog("Spiel1_Alert2");
            _after100seconds = true;
        }

        if (timeDelta >= _gameDurationInSeconds/2 && !_halfTimeReminded)
        {
            _dialogManager.NextDialog("Spiel1_Alert3");
            _halfTimeReminded = true;
        }

        if (timeDelta >= _gameDurationInSeconds - 20 && !_20SecondsRemainingReminded)
        {
            _dialogManager.NextDialog("Spiel1_Alert4");
            _20SecondsRemainingReminded = true;
        }

    }

    /// <summary>
    /// Description: Stops the game, performs final actions.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    private void StopGame()
    {
        _gameRunning = false;
        // count fields
        // stop animals (Attack no fields)
        //
        _dialogManager.NextDialog("Spiel1_End1");
        _score = _farmlandManager.GetScore();
        _scoreText.text = _score.ToString() + "/100";
        _backgroundMusic.Stop();
    }

    /// <summary>
    /// Description: Retrieves the target position from the FarmlandManager.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <returns>The target position as a nullable Vector3</returns>
    public Vector3? GetTargetPosition()
    {
        return _farmlandManager.GetTargetPosition();
    }

    /// <summary>
    /// Description: Notifies the FarmlandManager that an animal is eating at the specified target position.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="targetPosition">The position where the animal is eating</param>
    public void Eat(Vector3 targetPosition)
    {
        _farmlandManager.Eat(targetPosition);
    }

    /// <summary>
    /// Description: Notifies the FarmlandManager that the animal has finished eating and releases the target position.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    /// <param name="targetPosition">The position to release</param>
    public void StopEating(Vector3 targetPosition)
    {
        _farmlandManager.PutBackTargetPosition(targetPosition);
    }
}
