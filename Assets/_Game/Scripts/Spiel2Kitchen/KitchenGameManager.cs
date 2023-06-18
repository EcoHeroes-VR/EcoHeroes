using UnityEngine;
using System;
using System.Diagnostics;
using _Game.Scripts.Modules.DialogManager;
using TMPro;
using _Game.Scripts.Modules.SoundManager;
using _Game.Scripts.Modules.Player;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    Class to handle the game loop.\n
    /// Author:         Theresa Mayer, Lukas Konietzka\n
    /// </summary>
    public class KitchenGameManager : MonoBehaviour
    {
        private Stopwatch _stopwatch;
        private HighScoreKitchen _score;
        private bool _timerWasStarted;
        private bool _wasCalculated;

        [SerializeField] private InventoryVR _inventoryUI;

        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TextMeshProUGUI time;

        [SerializeField] private DialogManager _dialogManager;

        /// <summary>
        /// Description:    Is called in the first frame\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Start()
        {
            _inventoryUI.inventory = Player.GetInstance.Inventory;
            _dialogManager.NextDialog("KitchenEinleitung");

            _stopwatch = new Stopwatch();
            _score = gameObject.AddComponent<HighScoreKitchen>();
            _timerWasStarted = false;
        }

        /// <summary>
        /// Description:    Is called every frame\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Update()
        {
            if (_timerWasStarted)
            {
                 ShowTime(_stopwatch.Elapsed); 
            }
          
        }

        /// <summary>
        /// Description:    Is called when the backpack is picked up to start a timer.\n
        /// Author:         Theresa Mayer, Lukas Konietzka\n  
        /// </summary>
        public void OnBackpackPickup()
        {
            _stopwatch.Start();
            _timerWasStarted = true;
            SoundManager.GetInstance.StartSfx("task-successful");
        }

        /// <summary>
        /// Description:    Is called when all sockets with tag check are filled. Stops timer and calculates final score.\n
        /// Author:         Theresa Mayer, Lukas Konietzka\n
        /// </summary>
        public void OnAllSocketsFilled(int basicScore)
        {
            //end timer on allSocketsFilled = true;
            _stopwatch.Stop();
            _timerWasStarted = false;
            TimeSpan timeSpan = _stopwatch.Elapsed;
            
            if (_timerWasStarted && !_wasCalculated)
            {
                _wasCalculated = _score.CalculateCurrentScore(basicScore, timeSpan);
                ShowScore();
            }
            _timerWasStarted = false;
            _score.CalculateCurrentScore(basicScore, timeSpan);
            ShowScore();
            SoundManager.GetInstance.StartSfx("task-successful");
            
            _dialogManager.NextDialog("KitchenPhase2");
        }
        
        /// <summary>
        /// Description:    Show the score in the scene\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void ShowScore()
        {
            scoreText.text = $"Score: {_score.GetCurrentScore()}/100";
        }

        /// <summary>
        /// Description:    Show the time in the scene\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="timeInSeconds"></param>
        private void ShowTime(TimeSpan timeInSeconds)
        {
            time.text = $"Zeit: {timeInSeconds.Minutes}:{timeInSeconds.Seconds}";
        }
        
    }
}