using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Modules.Mascot
{ 
    /// <summary>
    /// Description:    Mascot follow player\n
    /// Author:         Dannenberg\n
    /// </summary>
    public class MascotFollow : MonoBehaviour
    {
        public float _speed;

        private Transform _target;
        
        /// <summary>
        /// Description:    Is called in the first frame and handle access to the target.\n
        /// Author:         Dannenberg\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        /// <summary>
        /// Description:    Is called every frame and move the mascot to the target.\n
        /// Author:         Dannenberg\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        void Update()
        {
            if (Vector3.Distance(transform.position, _target.position) > 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(_target.position.x, transform.position.y, _target.position.z), _speed * Time.deltaTime);
            }
            transform.LookAt(_target);
        }
        
        
    }
}
