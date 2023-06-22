using UnityEngine;

/// <summary>
/// Description: This class handels the outlines and events for the seed bag.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class ShowOutlines : MonoBehaviour
{

    [SerializeField] private Outline _outline;
    [SerializeField] private float _maxThickness;
    [SerializeField] private float _dropTime;
    private bool _isHeld = false;

    private float lastDrop;

    void Start()
    {
        lastDrop = Time.time;
        _outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
    }

    void Update()
    {
        // Enable outlines after x seconds when the player is not holding the object
        if (Time.time - lastDrop > _dropTime && !_isHeld) {
            _outline.enabled = true;
            _outline.OutlineWidth = (Mathf.Sin(Time.time*2f)+1.5f) * _maxThickness;
        }
    }

    /// <summary>
    /// Description: Enables outlines.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    public void Pickup()
    {
        _outline.enabled = false;
        _isHeld = true;
    }

    /// <summary>
    /// Description: Disables outlines.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    public void Drop()
    {
        lastDrop = Time.time;
        _isHeld = false;
    }

}
