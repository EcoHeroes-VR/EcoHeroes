using UnityEngine;

namespace _Game.Scripts.Modules.Highscore
{
    /// <summary>
    /// Description: High score for a single room\n
    /// Author: Martin Sattler\n
    /// </summary>
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class RoomHighscore_So : ScriptableObject
    {
        /// <summary>
        /// Description: The room name\n
        /// Author: Martin Sattler\n
        /// </summary>
        [field: SerializeField]
        public string RoomName { get; set; }
        
        /// <summary>
        /// Description: The score achieved\n
        /// Author: Martin Sattler\n
        /// </summary>
        [field: SerializeField]
        public int Score { get; set; }
    }
}