using System.Collections.Generic;
using _Game.Scripts.Helper;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine;
using System;

namespace _Game.Scripts.Modules.SoundManager
{
    public enum AudioArt
    {
        SFX, BACKGROUND, DIALOG
    }
    
    /// <summary>
    /// Description: Manage all the sound and music\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class SoundManager : MonoSingleton<SoundManager>
    {
        private const string ExposedMaster = "Master_Volume";
        private const string ExposedBackground = "Background_Volume";
        private const string ExposedSfx = "SFX_Volume";
        private const string ExposedOther = "Other_Volume";
        private const string ExposedDialog = "Dialog_Volume";

        public UnityEvent<string, AudioArt> OnAudioEnd; 

        [field: Space]
        [field: SerializeField] 
        private AudioMixer AudioMixer { get; set; }
        [field: SerializeField] 
        private AudioMixerGroup AudioSfxGroup{ get; set; } 
        [field: Space]
        [field: SerializeField] 
        public SoundPackageSo BackgroundPackageSo { get; set; }
        [SerializeField]
        private SoundPackageSo sfxPackageSo;
        public SoundPackageSo SfxPackageSo
        {
            get => sfxPackageSo;
            set {
                sfxPackageSo = value;
                InitialSfxSounds();
            } 
        }
        [field: Space]
        [field: Header("Audio Sources")]
        [field: SerializeField] 
        private AudioSource BackgroundSource { get; set; }
        [field: SerializeField]
        private AudioSource DialogSource { get; set; }
        [field: SerializeField]
        private GameObject SfxSourceParentObject { get; set; }

        private readonly Dictionary<string, AudioSource> _sfxSourceList = new();
        
        protected override void Awake()
        {
            base.Awake();
            InitialSfxSounds();
        }

        private void Start()
        {
            StartBackground("city1");
        }

        /// <summary>                                                                             
        /// Description: Get or set the master volume\n      
        /// Author: Martin Sattler\n                                                                                                        
        /// </summary>                                                                            
        public float MasterVolume
        {
            get {
                AudioMixer.GetFloat(ExposedMaster, out var value);
                return value;
            }
            set => AudioMixer.SetFloat(ExposedMaster, value);
        }
        
        /// <summary>                                                                             
        /// Description: Get or set the backgorund volume\n      
        /// Author: Martin Sattler\n                                                                                                                  
        /// </summary>                                                                            
        public float BackgroundVolume
        {
            get {
                AudioMixer.GetFloat(ExposedBackground, out var value);
                return value;
            }
            set => AudioMixer.SetFloat(ExposedBackground, value);
        }
        
        /// <summary>                                                                             
        /// Description: Get or set the sfx volume\n      
        /// Author: Martin Sattler\n                                                                                                         
        /// </summary>                                                                            
        public float SfxVolume
        {
            get {
                AudioMixer.GetFloat(ExposedSfx, out var value);
                return value;
            }
            set => AudioMixer.SetFloat(ExposedSfx, value);
        }
        
        /// <summary>                                                                             
        /// Description: Get or set the other volume\n      
        /// Author: Martin Sattler\n                                                                                                       
        /// </summary>                                                                            
        public float OtherVolume
        {
            get {
                AudioMixer.GetFloat(ExposedOther, out var value);
                return value;
            }
            set => AudioMixer.SetFloat(ExposedOther, value);
        }
        
        /// <summary>                                                                             
        /// Description: Get or set the dialog volume\n      
        /// Author: Martin Sattler\n                                                                                                                       
        /// </summary>                                                                            
        public float DialogVolume
        {
            get {
                AudioMixer.GetFloat(ExposedDialog, out var value);
                return value;
            }
            set => AudioMixer.SetFloat(ExposedDialog, value);
        }
        
        // Background
        // --- --- --- --- --- --- --- --- --- --- 
        /// <summary>                                                                                 
        /// Description: Start a specific background sound from the background sound package\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: title\n                                                                       
        /// Ret: None\n                                                                    
        /// </summary>                                                                                
        public void StartBackground(string title)
        {
            // TODO: Add Musik Continue Loop
            
            if (BackgroundPackageSo.SoundList == null || BackgroundPackageSo.SoundList.Count <= 0) {
                Debug.LogError("Music list is empty or null", BackgroundPackageSo);
                return;
            }

            SetAudio(BackgroundSource, BackgroundPackageSo.GetElementFromName(title));
            BackgroundSource.Play();
            
            StartCoroutine(WaitForEndSound(BackgroundSource, BackgroundPackageSo.GetElementFromName(title), AudioArt.BACKGROUND));
        }

        /// <summary>                                                                                 
        /// Description: Start a random background sound from the background sound package\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: None\n                                                                       
        /// Ret: None\n                                                                    
        /// </summary>                                                                                
        public void StartRandomBackground()
        {
            if (BackgroundPackageSo.SoundList == null || BackgroundPackageSo.SoundList.Count <= 0) {
                Debug.LogError("Music list is empty or null", BackgroundPackageSo);
                return;
            }

            var index = UnityEngine.Random.Range(0, BackgroundPackageSo.SoundList.Count);
            StartBackground(BackgroundPackageSo.SoundList[index].Name);
        }
        
        /// <summary>                                                                                 
        /// Description: Stop the background sound\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: None\n                                                                       
        /// Ret: None\n                                                                    
        /// </summary>      
        public void StopBackground()
        {
            BackgroundSource.Stop();
        }

        // SFX
        // --- --- --- --- --- --- --- --- --- --- 
        /// <summary>                                                                                 
        /// Description: Start a Effect sound\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: sfxName from the sfx sound package\n                                                                       
        /// Ret: None\n                                                                    
        /// </summary>     
        public void StartSfx(string sfxName)
        {
            if (!_sfxSourceList.Keys.Contains(sfxName)) {
                Debug.LogError("No sfx sample with this name found");
                return;
            }
            
            _sfxSourceList[sfxName].Play();
            StartCoroutine(WaitForEndSound(_sfxSourceList[sfxName], sfxPackageSo.GetElementFromName(sfxName), AudioArt.SFX));
        }

        /// <summary>                                                                                 
        /// Description: Stop a Effect sound\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: sfxName from the sfx sound package\n                                                                       
        /// Ret: None\n
        /// </summary>   
        public void StopSfx(string sfxName)
        {
            if (!_sfxSourceList.Keys.Contains(sfxName)) {
                Debug.LogError("No sfx sample with this name found");
                return;
            }
            
            _sfxSourceList[sfxName].Stop();
        }
        
        // Dialog
        // --- --- --- --- --- --- --- --- --- --- 
        /// <summary>                                                                                 
        /// Description: Start a dialog sound\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: audioElement\n                                                                       
        /// Ret: None\n
        /// </summary>   
        public void StartDialogSequenz(AudioElement audioElement)
        {
            if (audioElement == null) {
                Debug.LogError("Audio Element is empty or null", this);
                return;
            }

            SetAudio(DialogSource, audioElement);
            DialogSource.Play();
            StartCoroutine(WaitForEndSound(DialogSource, audioElement, AudioArt.DIALOG));
        }

        /// <summary>                                                                                 
        /// Description: stop the dialog sound\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: None\n                                                                       
        /// Ret: None\n
        /// </summary>   
        public void StopDialogSequenz()
        {
            DialogSource.Stop();
        }
        
        // Other
        // --- --- --- --- --- --- --- --- --- --- 
        /// <summary>                                                                                 
        /// Description: Initial sfx sounds\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: None\n                                                                       
        /// Ret: None\n
        /// </summary>   
        private void InitialSfxSounds()
        {
            if (SfxSourceParentObject == null) {
                Debug.LogException(new NullReferenceException("SfxSourceParentObject is null or empty"), SfxSourceParentObject);
                return;
            }
            
            // TODO: Objectpooling, to recycle the audiosources
            foreach (var audioSource in SfxSourceParentObject.GetComponents<AudioSource>()) {
                Destroy(audioSource);
            }
            
            foreach (var element in SfxPackageSo.SoundList) {
                var audioSource = SfxSourceParentObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = AudioSfxGroup;
                
                SetAudio(audioSource, element);
                
                _sfxSourceList.Add(element.Name, audioSource);
            }
        }
        
        /// <summary>                                                                                 
        /// Description: Set the audio source with data from <see cref="AudioElement"/>\n         
        /// Author: Martin Sattler\n                                                                  
        /// Args: AudioSource, AudioElement\n            
        /// Ret: None\n
        /// </summary> 
        private void SetAudio(AudioSource audioSource, AudioElement audioElement)
        {
            audioSource.clip = audioElement.AudioClip;
            audioSource.volume = audioElement.Volume;
            audioSource.pitch = audioElement.Pitch;
        }

        private IEnumerator WaitForEndSound(AudioSource audioSource, AudioElement audioElement, AudioArt audioArt)
        {
            if (string.IsNullOrEmpty(audioElement.Name))
                Debug.LogError("The name from an audio is not set.", audioElement.AudioClip);
            
            yield return new WaitUntil(() => !audioSource.isPlaying);
            
            OnAudioEnd.Invoke(audioElement.Name, audioArt);
        }
    }
}