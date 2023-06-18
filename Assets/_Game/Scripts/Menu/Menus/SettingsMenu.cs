using _Game.Scripts.Modules.SoundManager;
using Assets._Game.Scripts.Menu.Manager;
using UnityEngine.UI;
using UnityEngine;

namespace _Game.Scripts.Menu.Menus
{
    /// <summary>
    /// Description:    Button/Slider Events for the settings menu
    /// Author:         Marc Fischer
    /// </summary>

    public class SettingsMenu : MonoBehaviour
    {
        public Slider sliderDifficulty;
        public Slider sliderSubtitles;
        public Slider sliderMainSound;
        
        private void Awake()
        {
            // Set the sliders to their corresponding saved preferences
            sliderDifficulty.value = PlayerPrefs.GetInt("difficultyLevel", 0);
            sliderSubtitles.value = PlayerPrefs.GetInt("subtitlesSetting", 1);
            sliderMainSound.value = SoundManager.GetInstance.MasterVolume;
        }

        /// <summary>
        /// Description:    Update the difficulty level preference when the slider value changes\n
        /// Author:         Marc Fischer, Nikita Guryanov, Dominik Wegner\n
        /// </summary>
        public void DifficultySlider(float value)
        {
            PlayerPrefs.SetInt("difficultyLevel", Mathf.RoundToInt(value));
        }

        /// <summary>
        /// Description:    Update the subtitles preference when the slider value changes\n
        /// Author:         Marc Fischer, Nikita Guryanov, Dominik Wegner\n
        /// </summary>
        public void SubtitlesSlider(float value)
        {
            PlayerPrefs.SetInt("subtitlesSetting", Mathf.RoundToInt(value));
            Modules.Player.Player.GetInstance.SetSubtitles(Mathf.RoundToInt(value) == 1);
        }

        /// <summary>
        /// Description:    Update the main sound volume when the slider value changes\n
        /// Author:         Marc Fischer, Nikita Guryanov, Dominik Wegner\n
        /// </summary>
        public void VolumeSlider(float value)
        {
            PlayerPrefs.SetFloat("mainSoundLevel", value);
            SoundManager.GetInstance.MasterVolume = value;
        }

        /// <summary>
        /// Description:    Go back to the main menu\n
        /// Author:         Marc Fischer, Nikita Guryanov, Dominik Wegner\n
        /// </summary>
        public void ReturnButton()
        {
            // Save the player's preferences
            PlayerPrefs.Save();
            MenuManager.OpenMenu(MenuType.MAIN_MENU, gameObject);
        }
    }
}
