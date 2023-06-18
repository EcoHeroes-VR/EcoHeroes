using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace _Game.Scripts
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public abstract class XrHovering : MonoBehaviour
    {
        public abstract void OnHoverEnter();
        public abstract void OnHoverExit();
    }
}