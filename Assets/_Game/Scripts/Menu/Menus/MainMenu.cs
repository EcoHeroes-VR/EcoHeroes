using Assets._Game.Scripts.Menu.Manager;
using UnityEngine;

namespace _Game.Scripts.Menu.Menus
{
    /// <summary>
    /// Description:    Handles button events and navigation for the main menu.
    /// Author:         Marc Fischer
    /// </summary>
    public class MainMenu : MonoBehaviour
    {

        [SerializeField] private Mascotchen _mascotchen;

        /// <summary>
        /// Description:    Starts the tutorial.
        /// Author:         Marc Fischer
        /// </summary>
        public void TutorialButton() 
        {
            _mascotchen.GetComponent<Tutorial.Tutorial>().StartD();
        }

        /// <summary>
        /// Description:    Opens the settings menu.
        /// Author:         Marc Fischer.
        /// </summary>
        public void SettingsButton()
        {
            MenuManager.OpenMenu(MenuType.SETTINGS_MENU, gameObject);
        }

        /// <summary>
        /// Description:    Opens the credits menu.
        /// Author:         Marc Fischer
        /// </summary>
        public void CreditsButton()
        {
            MenuManager.OpenMenu(MenuType.CREDITS_MENU, gameObject);
        }
    }
}