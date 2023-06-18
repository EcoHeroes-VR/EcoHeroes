using UnityEngine;

namespace _Game.Scripts.Helper
{
    /// <summary>
    /// Description: Rotate the gameobject\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class Rotate : MonoBehaviour
    {
        [SerializeField] 
        [Range(1.0f, 100.0f)]
        private float speed = 10.0f;
        
        [SerializeField]
        private Vector3 direction = Vector3.forward;
        
        private void Update()
        {
            transform.Rotate(direction, speed * Time.deltaTime);
        }
    }
}