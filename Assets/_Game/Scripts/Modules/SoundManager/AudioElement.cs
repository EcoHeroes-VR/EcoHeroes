using UnityEngine;
using System;

namespace _Game.Scripts.Modules.SoundManager
{
    /// <summary>                                                                                              
    /// Description: Audio element with some Information\n                                                     
    /// Author: Martin Sattler\n                                                                               
    /// </summary>                                                                                             
    [Serializable]                                                                                             
    public class AudioElement                                                                                  
    {                                                                                                          
        [field: SerializeField] public string Name { get; set; }                                               
        [field: SerializeField] public AudioClip AudioClip { get; set; }

        [field: Header("Preferences")]
        [field: SerializeField]
        [field: Range(0f,1f)]
        [field: Tooltip("Standard: 1")]
        public float Volume { get; set; } = 1;

        [field: SerializeField] 
        [field: Range(-3f, 3f)]
        [field: Tooltip("Standard: 1")]
        public float Pitch { get; set; } = 1;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
                return $"{Name}";
            else
                return $"{AudioClip.ToString()}";
        }
    }   
}