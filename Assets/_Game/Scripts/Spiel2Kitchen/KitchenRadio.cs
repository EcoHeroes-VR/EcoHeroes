using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Modules.SoundManager;
using UnityEngine;
using Random = System.Random;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    Class for starting the background music in the game using the SoundManager.\n
    /// Author:         Theresa Mayer\n
    /// </summary>
    public class KitchenRadio : MonoBehaviour
    {

        [SerializeField] private SoundPackageSo _backgroundSoundpack;
        [SerializeField] private SoundPackageSo _sfxSoundpack;

        private void Start()
        {
            SoundManager.GetInstance.BackgroundPackageSo = _backgroundSoundpack;
            SoundManager.GetInstance.SfxPackageSo = _sfxSoundpack;
            SoundManager.GetInstance.StartBackground("chill_lofi_background");
        }
        
        /// <summary>
        /// Description:    Using the onAudioEnd Event of the SoundManager play one of two background sounds.\n
        /// Author:         Theresa Mayer\n
        /// </summary>
        public void OnBackgroundAudioFinished()
        {
            Random rand = new Random();

            SoundManager.GetInstance.StartBackground(rand.Next(0, 2) == 0 ? "chill_lofi_background" : "lofi-loop-2023");
        }
    }
}

