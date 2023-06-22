using _Game.Scripts.Modules.SoundManager;
using UnityEngine;
using System;

namespace _Game.Scripts.Modules.DialogManager
{
    /// <summary>
    /// Description: Dialog data\n
    /// Author: Martin Sattler\n
    /// </summary>
    [Serializable]  
    public class DialogElement
    {
        [field: SerializeField]
        public string Name { get; set; }
        [field: SerializeField]
        public string Speaker { get; set; }
        [field: SerializeField]
        [field: TextArea]
        public string DialogText { get; set; }
        
        [field: SerializeField]
        public string NextDialogName { get; set; }

        [field: SerializeField] 
        public AudioElement DialogSound { get; set; } = new();
    }
}