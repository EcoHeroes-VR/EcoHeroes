using UnityEngine;

namespace _Game.Scripts.Modules.SceneLoadManager.Fader
{
    /// <summary>
    /// Description: The abstract implementation for a screen fader\n
    /// Author: Martin Sattler\n
    /// </summary>
    public abstract class ScreenFader : MonoBehaviour, IScreenFader
    {
        /*
         * TODO: Screenfader not work
         * Description: Implement the Singleton Pattern for MonoBehaviour\n
         * Author: Martin Sattler\n
         */
        
        private static readonly int IntensityId = Shader.PropertyToID("_Intensity");
        private static readonly int FadeColorId = Shader.PropertyToID("_FadeColor");
        
        
        [SerializeField]
        [Range(0.1f, 1.0f)]
        protected float speed = 1.0f;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        protected float intensity;
        
        [SerializeField] 
        private Color color = Color.black;
        
        [SerializeField] 
        private Material fadeMaterial;
        
        /// <summary>
        /// Unity Methode to manupilate the rendertexture
        /// </summary>
        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            fadeMaterial.SetFloat(IntensityId, intensity);
            fadeMaterial.SetColor(FadeColorId, color);
            Graphics.Blit(source, destination, fadeMaterial);
        }

        /// <summary>
        /// Description: Coroutine for fade in\n
        /// Author: Martin Sattler\n
        /// </summary>
        public abstract Coroutine StartFadeIn();
        
        /// <summary>
        /// Description: Coroutine for fade out\n
        /// Author: Martin Sattler\n
        /// </summary>
        public abstract Coroutine StartFadeOut();
    }
}