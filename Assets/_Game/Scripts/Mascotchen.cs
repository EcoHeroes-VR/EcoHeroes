using _Game.Scripts.Modules.DialogManager;
using _Game.Scripts.Spiel2Kitchen;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace _Game.Scripts
{
    /// <summary>
    /// Description: Manages dialogue options for the mascot\n
    /// Author: Martin Sattler\n
    /// </summary>
    [RequireComponent(typeof(DialogManager))]
    public class Mascotchen : XrHovering
    {
        private DialogManager _dialogManager;

        private bool _dialogWelcome;

        private void Awake()
        {
            _dialogManager = GetComponent<DialogManager>();
            // _dialogManager.OnCurrentDialogEnd.AddListener((dialogName, go) =>
            // {
            //     if (go.name == this.name)
            //         Debug.Log(">>>>>>>> Dialog");
            // });
        }

        private void Start()
        {
            // StartWelcomeDialog();
        }

        public override void OnHoverEnter()
        {
            _dialogManager.OnActionAdd(StartDialog);
        }


        public override void OnHoverExit()
        {
            _dialogManager.OnActionRemove(StartDialog);
        }

        private void StartWelcomeDialog()
        {
            _dialogManager.NextDialog("Einleitung");
            _dialogWelcome = true;
        }

        private void StartDialog(InputAction.CallbackContext context)
        {
            if (!_dialogWelcome)
            {
                StartWelcomeDialog();
            }
            else
            {
                _dialogManager.NextDialog();    
            }
        }
       
    }
}