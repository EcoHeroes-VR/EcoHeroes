using System.Collections;
using UnityEngine;

namespace _Game.Scripts.Modules.SceneLoadManager.Fader
{
    /// <summary>
    /// Description: A basic screen fader\n
    /// Author: Martin Sattler\n
    /// </summary>
    public sealed class BasicScreenFader : ScreenFader
    {
        /// <summary>
        /// Description: Coroutine for fade in\n
        /// Author: Martin Sattler\n
        /// </summary>
        public override Coroutine StartFadeIn()
        {
            StopAllCoroutines();
            return StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            while (intensity <= 1.0f) {
                intensity += speed * Time.deltaTime;
                yield return null;
            }   
        }
        
        /// <summary>
        /// Description: Coroutine for fade out\n
        /// Author: Martin Sattler\n
        /// </summary>
        public override Coroutine StartFadeOut()
        {
            StopAllCoroutines();
            return StartCoroutine(FadeOut());
        }
        
        private IEnumerator FadeOut()
        {
            while (intensity >= 1.0f) {
                intensity -= speed * Time.deltaTime;
                yield return null;
            }   
        }
    }
}