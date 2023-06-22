using _Game.Scripts.Modules.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    Handle the Inventory\n
    /// Author:         Lukas Konietzka\n
    /// </summary>
    public class InventoryVR : MonoBehaviour
    {
        public GameObject inventory;
        public InputActionReference openInventoryOnButton;
        public GameObject activateInventoryOnObject;
        private bool _uiIActive;
        private bool _wasSelected;
        [SerializeField] private GameObject _gameManager;

        /// <summary>
        /// Description:    Setting the Inventory the disabled by default.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Start()
        {
            if(inventory != null)
            {
                inventory.SetActive(false);
            }
            _uiIActive = false;
            _wasSelected = false;
        }

        /// <summary>
        /// Description:    Disable or able the inventory depending on the backpack.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Update()
        {
            if (!_wasSelected)
            {
                _wasSelected = IsSelected();
                if (_wasSelected)
                {
                    StartTimer();
                }
                return;
            }
            activateInventoryOnObject.SetActive(false);
            openInventoryOnButton.action.started += OnButtonPress;
        }

        /// <summary>
        /// Description:    Handle the button press,\n  
        /// Author:         Lukas Konietzka\n
        /// Args:           context\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="context"></param>
        private void OnButtonPress(InputAction.CallbackContext context)
        {
            _uiIActive = !_uiIActive;
            inventory.SetActive(_uiIActive);
        }

        /// <summary>
        /// Description:    Get access to the backback object and return true when it is selected.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        True or False for selected or not selected.\n
        /// </summary>
        /// <returns></returns>
        private bool IsSelected()
        {
            if (activateInventoryOnObject == null) return false;
            
            return activateInventoryOnObject.GetComponent<XRGrabInteractable>().isSelected;
        }

        /// <summary>
        /// Description:    Start the GameLoop and the Timer.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void StartTimer()
        {
            _gameManager.GetComponent<KitchenGameManager>().OnBackpackPickup();
        }
    }
}



