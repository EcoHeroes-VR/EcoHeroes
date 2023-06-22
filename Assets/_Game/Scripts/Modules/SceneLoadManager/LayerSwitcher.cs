using UnityEngine;

namespace _Game.Scripts.Modules.SceneLoadManager
{
    /// <summary>
    /// Description: Switch the gameobject to the loadinglayer and back\n
    /// Author: Martin Sattler\n
    /// </summary>
    public sealed class LayerSwitcher : MonoBehaviour
    {
        [SerializeField]
        private string targetLayer = "LoadingLayer";
        private string _originalLayer = string.Empty;

        private void Awake()
        {
            _originalLayer = LayerMask.LayerToName(gameObject.layer);
        }

        private void OnEnable()
        {
            SceneLoader.GetInstance.onLoadBegin?.AddListener(SwitchToLoadLayer);
            SceneLoader.GetInstance.onLoadEnd?.AddListener(ResetLayer);
        }

        private void OnDisable()
        {
            // SceneLoader.GetInstance!.onLoadBegin?.RemoveListener(SwitchToLoadLayer); // Commented, error with no reference when close the game
            // SceneLoader.GetInstance!.onLoadEnd?.RemoveListener(ResetLayer); // Commented, error with no reference when close the game
        }

        private void SwitchToLoadLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(targetLayer);
        }

        private void ResetLayer()
        {
            gameObject.layer = LayerMask.NameToLayer(_originalLayer);
        }
    }
}