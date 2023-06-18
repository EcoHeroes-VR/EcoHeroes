using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Modules.Menu.SettingsScript
{
    public class SettingsScripts : MonoBehaviour
    {

        public Slider sliderDifficulty;
        public Slider sliderSubtitles;
        public Slider sliderMainSound;
        public Slider sliderSoundEffects;
        
        public InputActionReference backButton = null;
        public InputActionReference generalButton = null;

        /// <summary>
        /// Description:    Start is called before the first frame update\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void Start()
        {
            // Set the sliders to their corresponding saved preferences
            sliderDifficulty.value = PlayerPrefs.GetInt("difficultyLevel", 0);
            sliderSubtitles.value = PlayerPrefs.GetInt("subtitlesSetting", 1);
            sliderMainSound.value = PlayerPrefs.GetFloat("mainSoundLevel", 0.5f);
            sliderSoundEffects.value = PlayerPrefs.GetFloat("effectsSoundLevel", 0.5f);

            // Attach event listeners to the buttons and sliders
            sliderDifficulty.onValueChanged.AddListener(OnSliderDifficultyValueChanged);
            sliderSubtitles.onValueChanged.AddListener(OnSliderSubtitlesValueChanged);
            sliderMainSound.onValueChanged.AddListener(OnSliderMainSoundValueChanged);
            sliderSoundEffects.onValueChanged.AddListener(OnSliderSoundEffectsValueChanged);
        }

        /// <summary>
        /// Description:    Update the difficulty level preference when the slider value changes\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void OnSliderDifficultyValueChanged(float value)
        {
            PlayerPrefs.SetInt("difficultyLevel", Mathf.RoundToInt(sliderDifficulty.value));
        }

        /// <summary>
        /// Description:    Update the subtitles preference when the slider value changes\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void OnSliderSubtitlesValueChanged(float value)
        {
            PlayerPrefs.SetInt("subtitlesSetting", Mathf.RoundToInt(sliderSubtitles.value));
        }

        /// <summary>
        /// Description:    Update the main sound volume when the slider value changes\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void OnSliderMainSoundValueChanged(float value)
        {
            // Get the value of the slider and update the audio source volume
            var sliderValue = (float)sliderMainSound.value;
            
            // Update the saved preference
            PlayerPrefs.SetFloat("mainSoundLevel", sliderValue);
        }

        /// <summary>
        /// Description:    Update the sound effects volume when the slider value changes\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void OnSliderSoundEffectsValueChanged(float value)
        {
            // Get the value of the slider and update the saved preference
            var sliderValue = (float)sliderSoundEffects.value;

            PlayerPrefs.SetFloat("effectsSoundLevel", sliderValue);
        }

        /// <summary>
        /// Description:    Go back to the main menu\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void BackMenu()
        {
            // Save the player's preferences
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Description:    Update is called once per frame\n
        /// Author:         Nikita Guryanov, Dominik Wegner\n
        /// Args:           None\n
        /// Ret:            None
        /// </summary>
        void Update()
        {

        }
    }
}