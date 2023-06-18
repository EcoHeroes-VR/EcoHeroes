using Assets._Game.Scripts.Menu.Manager;
using UnityEngine;


namespace _Game.Scripts.Modules.Menu.NameChange
{
    /// <summary>
    /// Description:    Button Events for the name change menu
    /// Author:         Marc Fischer
    /// </summary>
    public class NameChangeMenu : MonoBehaviour
    {
        /// <summary>
        /// Description:    Event handler for the "Yes" button in the name change menu. Opens the name generator menu.
        /// Author:         Marc Fischer
        /// </summary>
        public void YesButton()
        {
            MenuManager.OpenMenu(MenuType.NAME_GENERATOR_MENU, gameObject);
        }

        /// <summary>
        /// Description:    Event for the "No" button in the name change menu. Goes back to the main menu.
        /// Author:         Marc Fischer
        /// </summary>
        public void NoButton()
        {
            MenuManager.OpenMenu(MenuType.MAIN_MENU, gameObject);
        }
    }
}