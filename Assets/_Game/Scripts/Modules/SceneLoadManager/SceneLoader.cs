using _Game.Scripts.Modules.SceneLoadManager.Fader;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using UnityEngine.EventSystems;
using _Game.Scripts.Helper;
using UnityEngine.Events;
using Unity.XR.CoreUtils;
using System.Collections;
using UnityEngine.XR;
using UnityEngine;

namespace _Game.Scripts.Modules.SceneLoadManager
{
    /// <summary>
    /// Description: The Sceneloader load and unload a scene\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class SceneLoader : MonoSingleton<SceneLoader>
    {
        public UnityEvent onLoadBegin = new();
        public UnityEvent onLoadEnd = new();
        
        [field: SerializeField]
        public ScreenFader ScreenFader { get; private set; }

        [SerializeField, Range(0f, 60f)] 
        [Tooltip("Time in seconds")]
        private float minLoadTime = 1.0f;
        
        [SerializeField] private XROrigin origin;
        [SerializeField] private Camera loadCam;
        
        private bool _isLoading;
        private Camera _mainCamera;

        protected void Start()
        {

            SceneManager.sceneLoaded += SetActiveScene;
            _mainCamera = Camera.main;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            SceneManager.sceneLoaded -= SetActiveScene;
        }

        /// <summary>
        /// Description: Load a new scene\n
        /// Author: Martin Sattler\n
        /// Args: sceneName
        /// Ret: None
        /// </summary>
        public void LoadNewScene(string sceneName)
        {
            if (!_isLoading)
                StartCoroutine(LoadScene(sceneName));
        }

        /// <summary>
        /// Description: Load a scene logic\n
        /// Author: Martin Sattler\n
        /// Args: sceneName
        /// Ret: IEnumerator
        /// </summary>
        private IEnumerator LoadScene(string sceneName)
        {
            _isLoading = true;

            var displaySubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRDisplaySubsystem>();
            if (displaySubsystem != null) displaySubsystem.Stop();

            var unloadScene = SceneManager.GetActiveScene();
            onLoadBegin?.Invoke();
            
            FindObjectOfType<EventSystem>().gameObject.SetActive(false);
            if (ScreenFader != null) yield return ScreenFader.StartFadeIn();
            yield return StartCoroutine(LoadNew(sceneName));

            yield return new WaitForSeconds(minLoadTime);
            
            yield return StartCoroutine(UnloadCurrent(unloadScene));
            if (ScreenFader != null) yield return ScreenFader.StartFadeOut();
            Debug.Log("Scene Loaded: " + sceneName);

            if (displaySubsystem != null) displaySubsystem.Start();
            onLoadEnd?.Invoke();
            
            _isLoading = false;
        }

        /// <summary>
        /// Description: Unload a scene\n
        /// Author: Martin Sattler\n
        /// Args: unloadScene
        /// Ret: IEnumerator
        /// </summary>
        private IEnumerator UnloadCurrent(UnityEngine.SceneManagement.Scene unloadScene)
        {
            var unloadOperation = SceneManager.UnloadSceneAsync(unloadScene);
            _mainCamera!.enabled = true;
            loadCam.enabled = false;
            origin.Camera = _mainCamera;

            while (!unloadOperation.isDone)
                yield return null;
        }

        /// <summary>
        /// Description: Load a new scene\n
        /// Author: Martin Sattler\n
        /// Args: sceneName
        /// Ret: IEnumerator
        /// </summary>
        private IEnumerator LoadNew(string sceneName)
        {
            var loadOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            _mainCamera!.enabled = false;
            loadCam.enabled = true;
            origin.Camera = loadCam;
            
            while (!loadOperation.isDone)
                yield return null;
            
            yield return null;
        }

        private void SetActiveScene(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
        {
            SceneManager.SetActiveScene(scene);
        }
    }
}
