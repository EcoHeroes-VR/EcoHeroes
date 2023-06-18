using System;
using _Game.Scripts.Modules.SceneLoadManager;
using UnityEngine;
using UnityEngine.InputSystem;


namespace _Game.Scripts.Modules.HubWorld
{
    /// <summary>
    /// Description:    Handle the scene change when pointing on the map.\n
    /// Author:         Konietzka\n
    /// </summary>
    public class PointingOnMap : MonoBehaviour
    {
        [SerializeField] private GameObject _hoverObject;
        private Animator _animator;
        private Renderer _renderer;
        
        /// <summary>
        /// Description:    Is called in the first frame and handle access to the animator.\n
        /// Author:         Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Start()
        {
            _animator = _hoverObject.GetComponent<Animator>();
            _renderer = _hoverObject.GetComponent<Renderer>();
        }

        /// <summary>
        /// Description:    Set "Emission" on enter hover and start animation.\n
        /// Author:         Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void HoverOver()
        {
            _renderer.material.EnableKeyword("_EMISSION");
            ActivateAnimation();
        }
        
        /// <summary>
        /// Description:    Remove "Emission" on hover exit, and end animation.\n
        /// Author:         Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void HoverEnd()
        {
            _renderer.material.DisableKeyword("_EMISSION");
            DeactivateAnimation();
        }
        
        /// <summary>
        /// Description:    Activate the animation.\n
        /// Author          Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void ActivateAnimation()
        {
            _animator.SetBool("IsMoving", true);
        }
        
        /// <summary>
        /// Description:    Deactivate the animation.\n
        /// Author:         Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void DeactivateAnimation()
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}
