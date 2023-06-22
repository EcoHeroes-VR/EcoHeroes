using Assets._Game.Scripts.Menu.Manager;
using UnityEditor;
using UnityEngine;


namespace _Game.Scripts.Modules.Menu.Main
{
    /// <summary>
    /// Description:    Handles button events and navigation for the credits menu.
    /// Author:         Marc Fischer 
    /// </summary>
    public class CreditsMenu : MonoBehaviour
    {
        /// <summary>
        /// Description:    Returns to the main menu.
        /// Author:         Marc Fischer
        /// </summary>
        public void ReturnButton()
        {
            MenuManager.OpenMenu(MenuType.MAIN_MENU, gameObject);
        }
    }
}