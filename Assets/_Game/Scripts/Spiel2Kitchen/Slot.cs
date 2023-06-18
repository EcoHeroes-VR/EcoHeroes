using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    Manage the objects in the inventory via Unity sockets\n
    /// Author:         Lukas Konietzka\n
    /// </summary>
    public class Slot : MonoBehaviour
    {
        public GameObject inventory; 
        public InputActionReference hideObjectOnButton;
        public int slotIndex;
        private bool _uIActive;
        private bool _isInSlot;
        private IXRSelectInteractable _objInSlot;

        /// <summary>
        /// Description:    Is called in the first frame and set all booleans to false\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Return:         None\n
        /// </summary>
        private void Start()
        {
         _uIActive = false;
            _isInSlot = false;
        }

        /// <summary>
        /// Description:    Is called every frame, when there is an object in the slot we get access to the slot and the object.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Update()
        {
            if (_isInSlot)
            {
                GameObject slot = AccessToSlot();
                AccessToObjectInSlot(slot);
                if (_objInSlot != null)
                {
                    hideObjectOnButton.action.started += HandleButtonPress;
                }
            }
        }

        /// <summary>
        /// Description:    Set the boolean to notify that an object entered the socket\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void ONSelectEntry()
        {
            // Getting the object in the slot, when something is in the slot:
            _isInSlot = true;
        }
    
        /// <summary>
        /// Description:    Getting access to the object in the slot when it exists\n
        /// Author:         Lukas Konietzka\n
        /// Args:           slot is the object that contains the object we need access.\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="slot"></param>
        private void AccessToObjectInSlot(GameObject slot)
        {
            XRSocketInteractor slotXRSocketInteractor = slot.GetComponent<XRSocketInteractor>();
            _objInSlot = slotXRSocketInteractor.GetOldestInteractableSelected();
        }

        /// <summary>
        /// Description:    Getting access to the slot via inventory.GetChild()\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        The object slot, it is a child of inventory\n
        /// </summary>
        /// <returns></returns>
        private GameObject AccessToSlot()
        {
            GameObject slot = inventory.transform.GetChild(slotIndex).gameObject;
            return slot;
        }
    
        /// <summary>
        /// Description:    Deactivate the object in the slot via pressing the button.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           context\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="context"></param>
        private void HandleButtonPress(InputAction.CallbackContext context)
        {
                //Hide the object in the slot
                _uIActive = !_uIActive;
                _objInSlot.transform.gameObject.SetActive(!_uIActive);
        }
    }
}

