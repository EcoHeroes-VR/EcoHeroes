using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Game.Scripts.Modules.HubWorld
{

    public class EventManager : MonoBehaviour
    {
        private string activeScene;
        public string thisScene;
        public GameObject inventory;
        public string moveIntoScene;

        public void Start()
        {
            activeScene = null;
        }

        public void Update()
        {
            activeScene = SceneManager.GetActiveScene().name;
            //ActivateEvents();
        }
    }
}