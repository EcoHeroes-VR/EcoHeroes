using UnityEngine;

namespace _Game.Scripts.Modules.SceneLoadManager.Fader
{
    /// <summary>
    /// Description: Interface for a screenfader\n
    /// Author: Martin Sattler\n
    /// </summary>
    public interface IScreenFader
    {
        /// <summary>
        /// Description: Coroutine for fade in\n
        /// Author: Martin Sattler\n
        /// </summary>
        Coroutine StartFadeIn();
        
        /// <summary>
        /// Description: Coroutine for fade out\n
        /// Author: Martin Sattler\n
        /// </summary>
        Coroutine StartFadeOut();
    }
}