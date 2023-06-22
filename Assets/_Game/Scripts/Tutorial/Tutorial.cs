
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using _Game.Scripts.Modules.DialogManager;

namespace _Game.Scripts.Tutorial
{
    /// <summary>
    /// Description:    Cup breaks into Parts\n
    /// Author:         Dannenberg, Guryanov\n
    /// </summary>
    public class Tutorial : MonoBehaviour
    {
        public DialogPackageSo zirbusPackage;
        public DialogPackageSo tutorialPackage;
        [SerializeField]
        private DialogManager _dialogManager;
        public TeleportationProvider teleportationProvider;
        public GameObject cupPrefab;
        private bool _cupDialog = false;

        /// <summary>
        /// Description:    Defines _dialogManager and teleportationProvider\n
        /// Author:         Dannenberg, Guryanov\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void Start()
        {
            //_dialogManager = GetComponent<DialogManager>();
           
            teleportationProvider = FindObjectOfType<TeleportationProvider>();
            
            // Add a listener to the OnCurrentDialogEnd event
            _dialogManager.OnCurrentDialogEnd.AddListener((dialogName, go) =>
            {
                // Check if the dialog name is "end5"
                if (dialogName == "end6")
                {
                    // Set the dialog package to zirbusPackage (default)
                    _dialogManager.DialogPackage = zirbusPackage;

                    // Disable the current script
                    enabled = false;
                    return;
                }
                
                // Check if the game object's name matches the current component's name
                // if (go.name == this.name)
                // {
                //     // Move to the next dialog
                //     _dialogManager.NextDialog();
                // }
            });
        }
        
        /// <summary>
        /// Description:    Is called in the first frame and disable teleportationProvider and calls StartTutorialDialog\n
        /// Author:         Dannenberg, Guryanov\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        public void StartD()
        {
            if (teleportationProvider != null)
            {
                teleportationProvider.enabled = false;
            }
            
            if (!CheckIfCupExists())
            {
                GameObject tutorialCup = Instantiate(cupPrefab);
                tutorialCup.SetActive(true);
                GameObject cupObject = GameObject.FindGameObjectWithTag("Cup");
                cupObject.transform.parent.name = "Cup";
                tutorialCup.transform.name = "TutorialCup";
            }

            _cupDialog = false;

            StartTutorialDialog();
        }

        /// <summary>
        /// Description:    Is called evry frame. Enable teleportationProviader and starts next dialog, if Tutorialcups breaks\n
        /// Author:         Dannenberg, Guryanov\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        void Update()
        {
            if (!CheckIfCupExists() && !_cupDialog) {
                if (_dialogManager.DialogPackage != tutorialPackage)
                    return;

                _cupDialog = true;
                if (teleportationProvider != null)
                    teleportationProvider.enabled = true;
                _dialogManager.NextDialog("end1");
            }
                
            //Miss Change DialogManager if dialog grip is finished back
        }
        
        /// <summary>
        /// Description:    Check if the Cup exists\n
        /// Author:         Dannenberg, Guryanov\n
        /// Args:           None\n
        /// Returns:        bool\n
        /// </summary>
        private bool CheckIfCupExists()
        {
            GameObject cupObject = GameObject.FindGameObjectWithTag("Cup");
            return cupObject != null && cupObject.transform.parent.name == "TutorialCup";
        }
        
        /// <summary>
        /// Description:    Starts Introduction of the Tutorial\n
        /// Author:         Dannenberg, Guryanov\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void StartTutorialDialog()
        {
            _dialogManager.DialogPackage = tutorialPackage;
            _dialogManager.NextDialog("Introduction1");
        }
    }
}