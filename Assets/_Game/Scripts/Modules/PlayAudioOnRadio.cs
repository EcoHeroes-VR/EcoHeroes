using UnityEngine;

namespace _Game.Scripts.Modules{
    
/// <summary>
    /// Description:    This class handles the audio witch is played on the radio.\n
    /// Author:         Konietzka\n
    /// </summary>
    public class PlayAudioOnRadio : MonoBehaviour
    {
        public AudioSource source;
        /// <summary>
        /// Description:    Play the audio on enter select.\n
        /// Author:         Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void OnSelect()
        {
            SoundManager.SoundManager.GetInstance.StartSfx("radio");
        }

        /// <summary>
        /// Description:    Stop the audio on exit select.\n
        /// Author:         Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void OnDrop()
        {
            SoundManager.SoundManager.GetInstance.StopSfx("radio");
        }

    }
}
