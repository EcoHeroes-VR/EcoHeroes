using System;
using UnityEngine;
using TMPro;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    Manage the high score in the kitchen game\n
    /// Author:         Lukas Konietzka\n
    /// </summary>
    public class HighScoreKitchen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI highScoreText;
        private int _currentScore;
        private int _highScore;

        /// <summary>
        /// Description:    Returns the current score of the game\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        the current calculated score\n
        /// </summary>
        /// <returns></returns>
        public int GetCurrentScore()
        {
            return this._currentScore;
        }
        
        /// <summary>
        /// Description:    Returns the high score\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        highscore\n
        /// </summary>
        /// <returns></returns>
        public int GetHighScore()
        {
            return this._highScore;
        }
    
        /// <summary>
        /// Description:    Write the new highscore into the file\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void UpdateHighScore()
        {
            if (_currentScore > PlayerPrefs.GetInt("HighScoreKitchen", 0))
            {
                PlayerPrefs.SetInt("HighScoreKitchen", _currentScore);
            }
        }

        /// <summary>
        /// Description:    Set the high score, if the current score is higher than the high score\n
        /// Author:         Lukas Konietzka\n  
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void UpdateHighScoreText()
        {
            highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScoreKitchen", 0)}"; 
        }

        /// <summary>
        /// Description:    With this methode you can add 1 coin  to your score (Not in use)\n
        /// Author:         Lukas Konietzka\n
        /// Args:           inc is 1 by default\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="inc"></param>
        public void IncrementCurrentScore(int inc=1)
        {
            _currentScore = _currentScore + inc;
        }

        /// <summary>
        /// Description:        Algorithm for the score Calculation\n
        /// Author:             Lukas Konietzka\n
        /// Args:               basicScore, timeSpan\n
        /// Returns:            return true when the score was calculated\n
        /// </summary>
        /// <param name="basicScore"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool CalculateCurrentScore(int basicScore, TimeSpan timeSpan)
        {
            int currentMinutes = timeSpan.Minutes;
            int currentSeconds = timeSpan.Seconds;
            int maxTimeInSeconds = 1000;
            int timeTotalInSeconds = (currentMinutes * 60) + currentSeconds;
            if (timeTotalInSeconds < 10)
            {
                //YOU CHEATED!!
                _currentScore = 0;
                return true;
            }
            int userTime = maxTimeInSeconds - timeTotalInSeconds;
            _currentScore = (userTime / 10);
            return true;
        }
    }

    
   
}