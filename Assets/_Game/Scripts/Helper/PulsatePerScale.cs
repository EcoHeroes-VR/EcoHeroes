using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Helper
{
    /// <summary>
    /// Description: Pulsate (per scaling) a object\n
    /// Author: Martin Sattler\n
    /// </summary>
    public class PulsatePerScale : MonoBehaviour
    {
        [SerializeField] 
        [Range(1.0f, 100.0f)]
        private float speed = 2.0f;
        
        [SerializeField]
        [Range(0.1f, 5.0f)]
        private float minPulse = 0.5f;
        
        [SerializeField]
        [Range(0.2f, 10.0f)]
        private float maxPulse = 1.5f;
        
        [SerializeField]
        private Vector3 direction = Vector3.forward;

        private readonly bool _runnable = true;
        
        private void Awake()
        {
            StartCoroutine(Pulse());
        }

        private IEnumerator Pulse()
        {
            while (_runnable) {
                // Scale to max
                while (gameObject.transform.localScale.x <= maxPulse) {
                    Pulsating(direction);
                    yield return null;
                }
                yield return new WaitForEndOfFrame();
                 
                // Scale to min
                while (gameObject.transform.localScale.x >= minPulse) {
                    Pulsating(-direction);
                    yield return null;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void Pulsating(Vector3 directionAxis)
        {
            var o = gameObject;
            o.transform.localScale = o.transform.localScale + directionAxis * (speed * Time.deltaTime);
        }
    }
}