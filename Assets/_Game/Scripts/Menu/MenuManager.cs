using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Game.Scripts.Menu.Manager
{
    public enum MenuType
    {
        MAIN_MENU,
        SETTINGS_MENU,
        NAME_CHANGE_MENU,
        NAME_GENERATOR_MENU,
        CREDITS_MENU

    }
    /// <summary>
    /// Description:    Manages the menu states in the game.
    /// Author:         Marc Fischer
    /// </summary>
    public static class MenuManager
    {
        private static Dictionary<MenuType, GameObject> Menus;

        /// <summary>
        /// Description:    Gets a value indicating whether the MenuManager has been initialized.
        /// Author:         Marc Fischer
        /// </summary>
        /// <returns> True if the MenuManager is initialized; otherwise, false.</returns>
        public static bool IsInitialised { get; private set; }

        /// <summary>
        /// Description: Initializes the MenuManager by finding and storing references to the menu GameObjects.
        /// Author: Marc Fischer
        /// </summary>
        private static void Init()
        {
            Menus = new();
            GameObject canvas = GameObject.Find("Menu").transform.Find("Canvas").gameObject;
            foreach (var menu in (MenuType[]) Enum.GetValues(typeof(MenuType)))
            {
                string menuName = Enum.GetName(typeof(MenuType), menu);
                GameObject menuObject = canvas.transform.Find(menuName).gameObject;
                Menus.Add(menu, menuObject);
            }
        }

        /// <summary>
        /// Description: Opens the specified menu and hides the calling menu.
        /// Author: Marc Fischer
        /// </summary>
        /// <param name="menuToOpen">The type of menu to open.</param>
        /// <param name="callingMenu">The menu that is currently active and calling the open operation.</param>
        public static void OpenMenu(MenuType menuToOpen, GameObject callingMenu)
        {
            if(!IsInitialised) 
            {
                Init();
            }
            Menus[menuToOpen].SetActive(true);
            callingMenu.SetActive(false);
        }
    }
}
