using _Game.Scripts.Modules.DialogManager;
using UnityEngine.InputSystem;
using UnityEngine;

namespace _Game.Scripts
{
    [RequireComponent(typeof(DialogManager))]
    public class PlantDialogTest : XrHovering
    {
        private DialogManager _dialogManager;
        private bool _dialogStart = false;

        private void Awake()
        {
            _dialogManager = GetComponent<DialogManager>();
            _dialogManager.OnCurrentDialogEnd.AddListener((dialogName, go) =>
            {
                if (go.name == this.name)
                {
                    _dialogManager.NextDialog();
                }
            });
        }

        public override void OnHoverEnter()
        {
            _dialogManager.OnActionAdd(StartDialog);
        }

        public override void OnHoverExit()
        {
            _dialogManager.OnActionRemove(StartDialog);
        }

        private void StartDialog(InputAction.CallbackContext context)
        {
            if (!_dialogStart) {
                _dialogManager.NextDialog("Start");
                _dialogStart = true;
            }
            else {
                _dialogManager.NextDialog();
                _dialogStart = false;
            }
        }
    }
}