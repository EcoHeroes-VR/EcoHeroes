using System.Collections.Generic;
using _Game.Scripts.Helper;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System;

namespace _Game.Scripts.Modules.Highscore
{
    /// <summary>
    /// Description: To load, save and edit the high score\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class HighscoreManager : MonoSingleton<HighscoreManager>
    {
        private const string FileName = "highscore.json";

        [SerializeField] 
#if UNITY_EDITOR
        [ContextMenuItem("Delete highscore.json", nameof(DeleteHighscoreFile))]
#endif
        private List<PlayerHighScore_So> _highscoreList = new();

        [SerializeField]
        private PlayerHighScore_So _highscore;

        private void HighscoreTest()
        {
            var score = CreateHighScore("Klasse 7b");

            var room1Score = ScriptableObject.CreateInstance<RoomHighscore_So>();
            room1Score.RoomName = "Room1";
            room1Score.Score = 30;
            score.AddRoomHighScore(room1Score);
            
            var room2Score = ScriptableObject.CreateInstance<RoomHighscore_So>();
            room2Score.RoomName = "Room2";
            room2Score.Score = 50;
            score.AddRoomHighScore(room2Score);
            
            Debug.Log($"Total Score: {score.TotalScore()}");
        }

        protected override void Awake()
        {
            base.Awake();
            
            Load();
            
            HighscoreTest();
        }

        protected void OnApplicationQuit()
        {
            Save();
        }
        
        /// <summary>
        /// Description: Creates a new object with player and high score data\n
        /// Author: Martin Sattler\n
        /// Args: playerName
        /// Return: The new Hisghcore object
        /// </summary>
        public PlayerHighScore_So CreateHighScore(string playerName)
        { 
            Save();
            
            var highScore = ScriptableObject.CreateInstance<PlayerHighScore_So>();
            highScore.PlayerName = playerName;

            highScore.name = highScore.GetPlayerNameWithTime();

            _highscore = highScore;
            return highScore;
        }

        /// <summary>
        /// Description: Returns a complete list of saved high scores.\n
        /// Author: Martin Sattler\n
        /// </summary>
        public PlayerHighScore_So[] GetPlayerHighscoreList()
        {
            return _highscoreList.ToArray();
        }
        
        private void Load()
        {
            // TODO: Try/Catch
            if (!File.Exists(GetFilePath())) return;
            
            using var sr = new StreamReader(GetFilePath());
            var json = sr.ReadLine();

            if (string.IsNullOrEmpty(json)) return;
            
            // TODO: Gives a warning because SO is to be created with Instantiate or CreateInstantiate
            _highscoreList = JsonConvert.DeserializeObject<List<PlayerHighScore_So>>(json);
        }

        private void Save()
        {
            // TODO: Try/Catch
            if (_highscore is not null && _highscore.RoomHighscoreList.Count > 0)
                _highscoreList.Add(_highscore);

            using var sw = new StreamWriter(GetFilePath());
            sw.Write(JsonConvert.SerializeObject(_highscoreList));
        }
        
        private static string GetFilePath() => Path.Combine(Application.persistentDataPath, FileName);
        
#if UNITY_EDITOR
        private void DeleteHighscoreFile()
        {
            if (!File.Exists(GetFilePath())) return;
            
            try {
                File.Delete(GetFilePath());
                Debug.Log("The file 'highscore.json' was successfully deleted.");
            }
            catch (Exception e) {
                Debug.LogError($"Error deleting the file: /n {e.Message}");
            }
        }
#endif
    }
}
