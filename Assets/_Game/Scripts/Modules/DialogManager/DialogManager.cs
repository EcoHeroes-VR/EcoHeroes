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
            // Start the given dialog
            if (!string.IsNullOrEmpty(dialogName)) {
                PlayDialog(dialogName);
            }
            // Start the next dialog
            else if (!string.IsNullOrEmpty(_nextDialog)) {
                PlayDialog(_nextDialog);
            }
            // End of dialog
            else {
                _dialogUi.ToggleUI(false);
                SoundManager.SoundManager.GetInstance.StopDialogSequenz();
            }
        }

        private void PlayDialog(string dialogName)
        {
            var package = DialogPackage.GetElementFromName(dialogName);

            if (package == null) {
                Debug.LogError("package is null!");
                return;
            }
            
            _currentDialog = package.Name;
            _nextDialog = package.NextDialogName;
        
            // play the audio when not null
            if (package.DialogSound.AudioClip != null)
                SoundManager.SoundManager.GetInstance.StartDialogSequenz(package.DialogSound);

            _dialogUi.SetDialog(this, package); 
            _dialogUi.ToggleUI(true);
        }
    }
}