using _Game.Scripts.Modules.Player;
using UnityEngine;

namespace _Game.Scripts
{
    /// <summary>
    /// Description: Set the main camera to canvas\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class StartScene : MonoBehaviour
    {
        [SerializeField]
        private Canvas[] canvas;

        [SerializeField]
        private Camera mainCamera;
        
        private void Start()
        {
            mainCamera = Player.GetInstance.MainCamera;

            foreach (var c in canvas)
                c.worldCamera = mainCamera;
        }
    }
}