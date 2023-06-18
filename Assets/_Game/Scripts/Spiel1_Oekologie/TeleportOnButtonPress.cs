using _Game.Scripts.Modules.SceneLoadManager;
using UnityEngine;

/// <summary>
/// Description: This class handles the scene loading event.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class TeleportOnButtonPress : MonoBehaviour
{
    [SerializeField] private string _scene;

    private SceneLoader _sceneLoader;
    private void Start()
    {
        _sceneLoader = SceneLoader.GetInstance;
    }

    /// <summary>
    /// Description: Event for switching to a scene.
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    public void LoadScene()
    {
        _sceneLoader.LoadNewScene(_scene);
    }
}