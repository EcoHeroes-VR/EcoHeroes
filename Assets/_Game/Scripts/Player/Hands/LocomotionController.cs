using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{

    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;

    public bool enableLeftTeleport { get; set; } = true;
    public bool enableRightTeleport { get; set; } = true;

    void Update()
    {
        Vector3 pos = new();
        Vector3 norm = new();
        int index = 0;
        bool validTarget = false;

        if(leftTeleportRay)
        {
            bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            leftTeleportRay.gameObject.SetActive(enableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
        }

        if (rightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            rightTeleportRay.gameObject.SetActive(enableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
