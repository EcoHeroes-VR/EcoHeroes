using UnityEngine.XR.Interaction.Toolkit;

namespace _Game.Scripts
{
    /// <summary>
    /// Description:    Creates custom XRSocketInteractor. Default socket is expanded by a tag check.\n
    /// Author:         Theresa Mayer\n
    /// </summary>
    public class SocketWithTagCheck : XRSocketInteractor
    {
        public string targetTag = string.Empty;

        /*----------------------------------------*/
        /*          override unity methods        */
        /* expand by using function MatchUsingTag */
        /*----------------------------------------*/
        public override bool CanHover(IXRHoverInteractable interactable)
        {
            return base.CanHover(interactable) && MatchUsingTag(interactable);
        }

        public override bool CanSelect(IXRSelectInteractable interactable)
        {
            return base.CanSelect(interactable) && MatchUsingTag(interactable);
        }

        /// <summary>
        /// Description: Compares the tag of an IXRInteractable Object with the in the inspector assigned tag.\n
        /// Author:      Theresa Mayer\n
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        private bool MatchUsingTag(IXRInteractable interactable)
        {
            return interactable.transform.CompareTag(targetTag);
        }
    }
} 


