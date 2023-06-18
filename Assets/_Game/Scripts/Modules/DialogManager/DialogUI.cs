using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace _Game.Scripts.Modules.DialogManager
{
    /// <summary>
    /// Description: Manage the UI for the dialog\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class DialogUI : MonoBehaviour
    {
        private Canvas _canvas;
        private DialogManager _dialogManager;

        [SerializeField] private Button nextButton;
        [SerializeField] private TextMeshProUGUI speaker;
        [SerializeField] private TextMeshProUGUI message;

        private bool activated = false;
        
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            
            nextButton.onClick.AddListener(NextDialog);
        }

        /// <summary>
        /// Description: Set the dialog texts with data\n
        /// Author: Martin Sattler\n
        /// Args: dialog\n
        /// Ret: None\n
        /// </summary>
        public void SetDialog(DialogManager manager, DialogElement dialog)
        {
            _dialogManager = manager;
            speaker.text = dialog.Speaker;
            message.text = dialog.DialogText;
            Debug.Log("setDialog");
            activated = true;
        }

        /// <summary>
        /// Description: Toggle the Dialog UI On or off\n
        /// Author: Martin Sattler\n
        /// Args: value\n
        /// Ret: None\n
        /// </summary>
        public void ToggleUI(bool value)
        {
            _canvas.enabled = value;
            nextButton.gameObject.SetActive(value);

            Debug.Log("toggleUI");
            if (!value) {
                Debug.Log("toggleUI - false");
                _dialogManager.OnCurrentDialogEnd.Invoke(_dialogManager.GetCurrentDialogName, _dialogManager.gameObject);
            }
        }

        private void NextDialog()
        {
            if (!activated) return;
            activated = false;
            
            Debug.Log("DialogUI - NextDialog");
            _dialogManager.NextDialog();
        }
        
    }
}