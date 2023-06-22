using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    This class makes sure that the correct types of ingredients are chosen by the player.\n
    ///                 This script should be attached to a GameObject with a surface.\n
    /// Author:         Theresa Mayer\n
    /// </summary>
    public class IngredientCheck : MonoBehaviour
    {
        [SerializeField] private KitchenGameManager _kitchenGameManager;
        private XRSocketInteractor[] _sockets;
        
        [SerializeField] private List<IngredientData> ingredientDataSauce;
        [SerializeField] private List<IngredientData> ingredientDataCheese;
        [SerializeField] private List<IngredientData> ingredientDataTopping;

        public static bool AllSocketsFilled = false;

        public int score =0;
        void Start()
        {
            _sockets = this.GetComponentsInChildren<XRSocketInteractor>();
        }
        
        /// <summary>
        /// Description:    Event function: Is called when the player interacts with the XRSocket.\n
        ///                 If all sockets are filled the next Game Phase begins.\n
        /// Author:         Theresa Mayer\n
        /// </summary>
        public void OnSelected()
        {
            if (CheckAllSocketsFilled())
            {
                CountScore();
                Debug.Log(score);
                
                // end gamephase 1
                _kitchenGameManager.OnAllSocketsFilled(score);
                AllSocketsFilled = true;
            }
            
            // continue gamephase 1
        }
        
        /// <summary>
        /// Description:    This method checks whether all XRSockets are filled.\n
        /// Author:         Theresa Mayer\n
        /// </summary>
        /// <returns></returns>
        private bool CheckAllSocketsFilled()
        {
            int count = 0;
            foreach (var socket in _sockets)
            {
                if (socket.GetOldestInteractableHovered() == null) continue;

                count++;
            }
            
            if (count == _sockets.Length) return true;

            return false;
        }

        /// <summary>
        /// Description:    Counts the points scored by the objects placed in the sockets.\n
        ///                 Score Points are assigned depending on the ingredients type of origin (see IngredientData.cs).\n
        ///                 Ingredients with conventional origin should be placed first in the inspector list, biological origin second.\n
        /// Author:         Theresa Mayer\n
        /// </summary>
        private void CountScore()
        {
            foreach (var socket in _sockets)
            {
                IXRSelectInteractable socketInteractable = socket.GetOldestInteractableSelected();
                GameObject objInSocket = socketInteractable.transform.gameObject;
                switch (objInSocket.tag)
                {   
                    case "Sauce":
                        if (objInSocket.name.Contains("Con"))
                        {
                            score += ingredientDataSauce[0].points;
                        }
                        if (objInSocket.name.Contains("Bio"))
                        {
                            score += ingredientDataSauce[1].points;
                        }
                        break;
                    
                    case "Cheese":
                        if (objInSocket.name.Contains("Con"))
                        {
                            score += ingredientDataCheese[0].points;
                        }

                        if (objInSocket.name.Contains("Bio"))
                        {
                            score += ingredientDataCheese[1].points;
                        }
                        break;
                    
                    case "Topping":
                        if (objInSocket.name.Contains("Con"))
                        {
                            score += ingredientDataTopping[0].points;
                        }
                        if (objInSocket.name.Contains("Bio"))
                        {
                            score += ingredientDataTopping[1].points;
                        }
                        break;
                }
            }
        }
    }
}
