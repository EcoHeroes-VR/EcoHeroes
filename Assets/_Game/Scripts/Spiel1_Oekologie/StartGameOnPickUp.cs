using UnityEngine;

/// <summary>
/// Description: This class provides the game start callback function.
/// Author: Marc Fischer, Manuel Hagen
/// </summary>
public class StartGameOnPickUp : MonoBehaviour
{
    private Level1GameManager gameManager;
    private bool gameRunning;

    public void Start()
    {
        gameManager = Level1GameManager.GetInstance();
    }

    /// <summary>
    /// Description: Switch the games running state (on/off).
    /// Author: Marc Fischer, Manuel Hagen
    /// </summary>
    public void StartGame()
    {
        if(gameRunning) 
        { 
            return; 
        }
        gameRunning = true;

        gameManager.StartGame();
    }
}
