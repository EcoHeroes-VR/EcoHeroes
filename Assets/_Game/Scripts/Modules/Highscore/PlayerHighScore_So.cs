using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Game.Scripts.Modules.Highscore
{
    /// <summary>
    /// Description: Highscore for the player\n
    /// Author: Martin Sattler\n
    /// </summary>
    //[CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class PlayerHighScore_So : ScriptableObject
    {
        /// <summary>
        /// Description: A unique ID\n
        /// Author: Martin Sattler\n
        /// </summary>
        [field: SerializeField] 
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Description: The player name\n
        /// Author: Martin Sattler\n
        /// </summary>
        [field: SerializeField] 
        public string PlayerName  { get; set; }
        
        /// Description: When the high score was created\n
        /// Author: Martin Sattler\n
        /// </summary>
        [field: SerializeField]
        public DateTime Time  { get; set; } = DateTime.UtcNow;
        
        // Highscore Daten
        /// Description: A list with highscores from the individual rooms\n
        /// Author: Martin Sattler\n
        /// </summary>
        [field: SerializeField]
        public List<RoomHighscore_So> RoomHighscoreList { get; set; } = new ();

        // Methods
        /// <summary>
        /// Description: Returns the name of the player with the addition of the month and year.\n
        /// Author: Martin Sattler\n
        /// </summary>
        public string GetPlayerNameWithTime() => PlayerName + "_" + Time.Month + "." + Time.Year;

        /// <summary>
        /// Description: Adds a room with high score\n
        /// Author: Martin Sattler\n
        /// Args:  rScore: Room with hidghscore data
        /// </summary>
        public void AddRoomHighScore(RoomHighscore_So rScore)
        {
            Assert.IsNotNull(rScore);

            // Check if the room is already in the list
            if (!RoomHighscoreList.Exists(r => r.RoomName.Equals(rScore.RoomName))) {
                RoomHighscoreList.Add(rScore);
                return;
            }
            
            // Get the room
            var room = RoomHighscoreList.First(r => r.RoomName.Equals(rScore.RoomName));

            // Überprüfe ob der neue Score besser ist, wenn ja übernehme den neuen Score
            if (room.Score < rScore.Score) {
                room.Score = rScore.Score;
                
                // Weitere ...
            }
        }
        
        /// <summary>
        /// Description: The total score, from all rooms\n
        /// Author: Martin Sattler\n
        /// </summary>
        public int TotalScore()
        {
            return RoomHighscoreList.Sum(rScore => rScore.Score) / RoomHighscoreList.Count;
        }
    }
}
