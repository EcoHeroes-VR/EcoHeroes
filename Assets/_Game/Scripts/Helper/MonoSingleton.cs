using UnityEngine;

namespace _Game.Scripts.Helper
{
    /// <summary>
    /// Description: Implement the Singleton Pattern for MonoBehaviour\n
    /// Author: Martin Sattler\n
    /// </summary>
    /// <typeparam name="T">The type of instance\n</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;

        /// <summary>
        /// Description: Get the instance back from type T\n
        /// Author: Martin Sattler\n
        /// </summary>
        public static T GetInstance
        {
            get {
                if (_instance == null) {
                    // Suchen Sie nach einer vorhandenen Instanz in der Szene
                    _instance = FindObjectOfType<T>();

                    // Wenn keine Instanz gefunden wurde, erstellen Sie eine neue GameObject-Instanz
                    if (_instance == null) {
                        var singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString();

                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null) {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else {
                if (_instance.gameObject != gameObject)
                    Destroy(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this) 
                _instance = null;
        }
    }
}