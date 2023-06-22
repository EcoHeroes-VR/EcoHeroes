using UnityEngine;
using Random = UnityEngine.Random;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    This class handles all interactions with the dough GameObject to build a pizza object.\n
    /// Author:         Theresa Mayer, Lukas Konietzka\n
    /// </summary>
    public class DoughManager : MonoBehaviour
    {   
        private bool _isSauce;
        //uniform transform.position y value for topping Objects
        private const float _Y_OFFSET = 5.29f;
        //possible topping objects
        [SerializeField] private GameObject[] toppings;

        /// <summary>
        /// Description:    Method to handle particle collision events with the particle systems of sauce and cheese GameObjects\n
        /// Author:         Theresa Mayer\n
        /// </summary>
        /// <param name="other"></param>
        private void OnParticleCollision(GameObject other)
        {
            CreateToppingAt(PickUp.selectedTagObject);
        }

        /// <summary>
        /// Description:    Create a new Topping Object (sauce or cheese) after particle collision events. New objects are parented to the dough object.\n
        /// Author:         Theresa Mayer\n
        /// </summary>
        /// <param name="toppingTag"></param>
        private void CreateToppingAt(string toppingTag)
        {
            Vector3 toppingPos = GenerateToppingSpawnPos();
            switch (toppingTag)
            {
                case "Sauce":
                    GameObject sauceObjOriginal = toppings[0];
                    GameObject newSauceObject = Instantiate(sauceObjOriginal, toppingPos,Quaternion.Euler(-90f, 0f, 0f));
                    newSauceObject.transform.localScale = new Vector3(15f, 15f, 15f);
                    break;
            
                case "Cheese":
                    GameObject cheeseObjOriginal = toppings[1];
                    GameObject newCheeseObject = Instantiate(cheeseObjOriginal, toppingPos, Quaternion.Euler(-90f, 0f, 0f));
                    newCheeseObject.transform.localScale = new Vector3(3f, 3f, 3f);
                    break;
            }
        }
        
        /// <summary>
        /// Description:    Method to create the specific coordinates to spawn a pizza topping object.\n
        /// Author:         Theresa Mayer\n
        /// Args:           None\n
        /// Returns:        Vector for the spawnpoint\n
        /// </summary>
        /// <returns></returns>
        private Vector3 GenerateToppingSpawnPos()
        {
            float minXPos = 9.475f;
            float maxXPos = 9.89f;
            float minZPos = -13.575f;
            float maxZPos = -13.155f;

            Vector3 randomPos = new Vector3(Random.Range(minXPos, maxXPos),  _Y_OFFSET, Random.Range(minZPos, maxZPos));
            return randomPos;
        }
    }
}


