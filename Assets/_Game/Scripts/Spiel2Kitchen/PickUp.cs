using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:        Manage the access to th object that is attach to\n
    /// Author:             Lukas Konietzka\n
    /// </summary>
    public class PickUp : MonoBehaviour
    {
        private bool _needToChange;
        public static string selectedTagObject;

        private void Start()
        {
            _needToChange = true;
            selectedTagObject = null;
        }

        /// <summary>
        /// Description:    Is called every frame and check if the object is selected\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Update()
        {
            if (IsSelected() && _needToChange)
            {
                ChangeLayerOnTags("Topping", "Sauce", "Cheese");
                selectedTagObject = GetSelectedTagObject();
            }
        }

        /// <summary>
        /// Description:    Check if the object is selected\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        true when the object is selected\n
        /// </summary>
        /// <returns></returns>
        private bool IsSelected()
        {
            return gameObject.GetComponent<XRGrabInteractable>().isSelected;
        }
        
        /// <summary>
        /// Description:    This method change the layer to "inventory" when the object is selected an the given\n
        ///                 tags ar correct\n
        /// Author:         Lukas Konietzka\n
        /// Args:           valid tags\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="topping"></param>
        /// <param name="sauce"></param>
        /// <param name="cheese"></param>
        private void ChangeLayerOnTags(string topping, string sauce, string cheese)
        {
            if (gameObject.tag.Equals(topping) || gameObject.tag.Equals(sauce) || gameObject.tag.Equals(cheese))
            {
                gameObject.GetComponent<XRGrabInteractable>().interactionLayers =
                    InteractionLayerMask.GetMask("Inventory");
                _needToChange = false;
            }
        }
        
        /// <summary>
        /// Description:    Get Access to the object that is selected and return it\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        Object that is selected\n
        /// </summary>
        /// <returns></returns>
        public string GetSelectedTagObject()
        {
            if (IsSelected())
            {
                  return gameObject.tag;
            }
            return "";
        }
    }
}