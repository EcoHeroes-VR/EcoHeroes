using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Modules.DialogManager
{
    /// <summary>
    /// Description: Represent a list of dialogs\n
    /// Author: Martin Sattler\n
    /// </summary>
    [CreateAssetMenu(fileName = "DialogPackageSo", menuName = "_Game/Dialog Package So", order = 0)]
    public class DialogPackageSo : ScriptableObject
    {
        [field: SerializeField]
        public List<DialogElement> DialogList { get; set; }
        
        /// <summary>
        /// Description: Get an <see cref="DialogElement"/>  back specified from elementName\n
        /// Author: Martin Sattler\n
        /// Args: elementName\n
        /// Ret: An dialogElement\n
        /// </summary>
        public DialogElement GetElementFromName(string elementName)
        {
            return DialogList.Find(e => e.Name.Equals(elementName));
        }
    }
}