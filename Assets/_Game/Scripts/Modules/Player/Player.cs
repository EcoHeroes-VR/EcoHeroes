using _Game.Scripts.Modules.DialogManager;
using _Game.Scripts.Helper;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Game.Scripts.Modules.Player
{
    /// <summary>
    /// Description: The player\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class Player : MonoSingleton<Player>
    {
        [field: SerializeField]
        public DialogUI DialogUI { get; private set; }
        
        [field: SerializeField]
        public bool Subtitles { get; private set; }
        
        [field: SerializeField]
        public Camera MainCamera { get; private set; }

        [SerializeField] private XRRayInteractor interactorRight;
        [SerializeField] private XRRayInteractor interactorLeft;

        public GameObject Inventory;

        protected override void Awake()
        {
            base.Awake();
            
            MainCamera = Camera.main;
        }
        
        protected void Start()
        {
            SetSubtitles(PlayerPrefs.GetInt("subtitlesSetting", 1) == 1);

            var masterVolume = PlayerPrefs.GetFloat("mainSoundLevel", 0.0f);
            SoundManager.SoundManager.GetInstance.MasterVolume = masterVolume;
            
            SceneLoadManager.SceneLoader.GetInstance.onLoadBegin.AddListener(() => {
                interactorRight.enabled = false;
                interactorLeft.enabled = false;
            });
            SceneLoadManager.SceneLoader.GetInstance.onLoadEnd.AddListener(() => {
                gameObject.SetActive(false);
                gameObject.SetActive(true);
                interactorRight.enabled = true;
                interactorLeft.enabled = true;
            });
        }
        
        /// <summary>
        /// Description: Set the subtitle for dialog\n
        /// Author: Martin Sattler\n
        /// </summary>
        public void SetSubtitles(bool value)
        {
            Subtitles = value;
        }
    }
}