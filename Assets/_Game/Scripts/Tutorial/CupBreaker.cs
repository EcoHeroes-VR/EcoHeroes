using System;
using _Game.Scripts.Helper;
using _Game.Scripts.Modules.SoundManager;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Random = UnityEngine.Random;

namespace Scripts.Tutorial
{
    /// <summary>
    /// Description:    Cup breaks into Parts\n
    /// Author:         Dannenberg\n
    /// </summary>
    public class CupBreaker : MonoBehaviour
    {

        private bool _isBroken;
        //public AudioElement _scream;

        public GameObject[] pieces;
        public GameObject Cup;
        
        /// <summary>
        /// Description:    Is called when the gameobject(Parent) collides with another. Activated the child with Tag "CupPart" and destroy the child with the Tag "Cup"\n
        /// Author:         Dannenberg, Guryanov, Sattler\n
        /// Args:           Collision\n
        /// Returns:        None\n
        /// </summary>
        // private void OnCollisionEnter(Collision collision)
        // {
        //     if (collision.gameObject.name == "FloorPlane" && !_isBroken)
        //     {
        //         Component[] childrenWithTag = gameObject.GetComponentsInChildren<Component>(true);
        //         foreach (Component child in childrenWithTag)
        //         {
        //             
        //             if (child.CompareTag("CupPart"))
        //             {
        //                 child.gameObject.SetActive(true);
        //                 child.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //             } else if (child.CompareTag("Cup"))
        //                 Destroy(child.gameObject);
        //         }
        //         SoundManager.GetInstance.StartDialogSequenz(_scream);
        //         _isBroken = true;
        //     }
        // }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name != "FloorPlane")
                return;

            if (_isBroken)
                return;
            
            foreach (var child in pieces) {
                    child.gameObject.SetActive(true);
                    child.GetComponent<XRGrabInteractable>().enabled = true;
            }
            
            SoundManager.GetInstance.StartSfx("scream");
            Destroy(Cup);
            _isBroken = true;
            
        }
    }
}