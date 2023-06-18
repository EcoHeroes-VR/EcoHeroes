using UnityEngine;

namespace _Game.Scripts.Modules.Scene
{
    /// <summary>
    /// Description: To mark a start point in a scene and move the player to it\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class StartPoint : MonoBehaviour
    {
        private void Start()
        {
            Player.Player.GetInstance.transform.position = transform.position;
        }
    }
}