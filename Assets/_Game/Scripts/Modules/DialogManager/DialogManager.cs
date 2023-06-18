using _Game.Scripts.Modules.SoundManager;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine;
using System;

namespace _Game.Scripts.Modules.DialogManager
{
    /// <summary>
    /// Description: Manage all the dialogs\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class DialogManager : MonoBehaviour
    {
        private DialogUI _dialogUi;
        private string _currentDialog = "";
        private string _nextDialog = "";
        private bool _subtitle = true;
        
        [SerializeField]
        private InputActionReference dialogNextStartReference;
        
        [field: SerializeField]
        public DialogPackageSo DialogPackage { get; set; }

        public UnityEvent<string, GameObject> OnCurrentDialogEnd;

        public string GetCurrentDialogName => _currentDialog;
        
        private void Start()
        {
            var player = Player.Player.GetInstance;
            _dialogUi = player.DialogUI;
            _subtitle = player.Subtitles;
            
            SoundManager.SoundManager.GetInstance.OnAudioEnd.AddListener(OnAudioDialogEnd);
        }

        private void OnAudioDialogEnd(string audioName, AudioArt audioArt)
        {
            if (audioArt != AudioArt.DIALOG) return;
            var elementName = DialogPackage.GetElementFromName(_currentDialog)?.DialogSound.Name;
            if (!audioName.Equals(elementName)) return;
            
            OnCurrentDialogEnd.Invoke(_currentDialog, gameObject);
        }

        public void OnActionAdd(Action<InputAction.CallbackContext> action)
        {
            dialogNextStartReference.action.started += action;
        }
        
        public void OnActionRemove(Action<InputAction.CallbackContext> action)
        {
            dialogNextStartReference.action.started -= action;
        }


        /// <summary>
        /// Description: Trigger the next dialog or start a new dialog with the given dialogName.\n
        /// Author: Martin Sattler\n
        /// Args: dialogName\n
        /// Ret: None\n
        /// </summary>
        public void NextDialog(string dialogName = "")
        {
            Debug.Log("nextDialog");
            // Start the given dialog
            if (!string.IsNullOrEmpty(dialogName)) {
                PlayDialog(dialogName);
                
                Debug.Log("NextDialog - dialogName");
            }
            // Start the next dialog
            else if (!string.IsNullOrEmpty(_nextDialog)) {
                PlayDialog(_nextDialog);
                
                Debug.Log("NextDialog - _nextDialog");
            }
            // End of dialog
            else {
                Debug.Log("NextDialog - endDialog");
                _dialogUi.ToggleUI(false);
                SoundManager.SoundManager.GetInstance.StopDialogSequenz();
            }
        }

        private void PlayDialog(string dialogName)
        {
        //Debug.Log("Play Dialog");
            var package = DialogPackage.GetElementFromName(dialogName);
            if (package is not null) {
                _currentDialog = package.Name;
                _nextDialog = package.NextDialogName;
            
                if (package.DialogSound.AudioClip != null)
                    SoundManager.SoundManager.GetInstance.StartDialogSequenz(package.DialogSound);

                if (package.DialogSound.AudioClip != null || _subtitle) {
                    _dialogUi.SetDialog(this, package);
                    _dialogUi.ToggleUI(true);
                }
                else
                    _dialogUi.ToggleUI(false);
            }
        }
    }
}