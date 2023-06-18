using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Modules.SoundManager
{
    /// <summary>
    /// Description: Represent a list of music titles\n
    /// Author: Martin Sattler\n
    /// </summary>
    [CreateAssetMenu(fileName = "SoundPackageSO", menuName = "_Game/Sound Package SO", order = 0)]
    public class SoundPackageSo : ScriptableObject
    {
        [field: SerializeField]
        public List<AudioElement> SoundList { get; set; }

        /// <summary>
        /// Description: Get an <see cref="AudioElement"/>  back specified from elementName\n
        /// Author: Martin Sattler\n
        /// Args: elementName\n
        /// Ret: An audioElement\n
        /// </summary>
        public AudioElement GetElementFromName(string elementName)
        {
            return SoundList.Find(e => e.Name.Equals(elementName));
        }
    }
}