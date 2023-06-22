using UnityEngine;
using EzySlice;
using _Game.Scripts.Modules.SoundManager;
using UnityEngine.XR.Interaction.Toolkit;

namespace _Game.Scripts.Spiel2Kitchen
{
    /// <summary>
    /// Description:    With this class its possible to cut any objekt with the layer "Sliceable" into to parts.\n
    ///                 For this Job we use the package "EzySlice" from David Arayan.\n
    /// Author:         Konietzka, Lukas\n
    ///                 Arayan, David\n
    /// </summary>
    public class SliceObject : MonoBehaviour
    {
        public Transform startSlicePoint;
        public Transform endSlicePoint;
        public VelocityEstimator velocityEstimator;
        public LayerMask sliceable;
        public Material crossSectionMaterial;
        public float cutForce = 1;
        
        /// <summary>
        /// Description:    Is called every frame.\n
        ///                 Detect an collision between the knife and an object with the layer "Slicable"\n
        ///                 and trigger the funktion Slice().\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void FixedUpdate()
        {
            bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit,
                sliceable);
            if (hasHit)
            {
                GameObject target = hit.transform.gameObject;
                Slice(target);
            }
        }
        
        /// <summary>
        /// Description:    This method use the EzySlice package and cut an given gameobject into two parts\n
        ///                 by adding two new objeckts and destroying the old one.\n
        /// Author:         Lukas Konietzka, David Arayan\n
        /// Args:           target: Object, that has to be cut.\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="target"></param>
        public void Slice(GameObject target)
        {
            var planeNormal = CalculateSlicingDirection();

            SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
            if (hull != null)
            {
                GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
                SetupSliceComponent(upperHull);
                GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
                SetupSliceComponent(lowerHull);
                PlayKnifeAudio();
                Destroy(target);
            }
        }
        
        /// <summary>
        /// Description:    There is a plane attached to the knife,\n
        ///                 A perpendicular vector is determined for this plane,\n
        ///                 This vector indicates the direction in which you have to cut,\n
        ///                 Therefor we use the crossproduct to calculate this vector.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           None\n
        /// Returns:        The slicing direction\n
        /// </summary>
        /// <returns></returns>
        private Vector3 CalculateSlicingDirection()
        {
            Vector3 velocity = velocityEstimator.GetVelocityEstimate();
            Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
            planeNormal.Normalize();
            return planeNormal;
        }

        /// <summary>
        /// Description:    Add all necessary Components to the given gameobject.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           sliceObject: Object that get the following components.\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        public void SetupSliceComponent(GameObject sliceObject)
        {
            AddPhysics(sliceObject);
            AddCollider(sliceObject);
            AddXRGrab(sliceObject);
            ChangeLayerMask(sliceObject);
            ChangeTag(sliceObject);
        }
        
        /// <summary>
        /// Description:    Add an Rigidbody to the given gameobject.\n
        /// Author:         Lukas Konietzka.\n
        /// Args:           sliceObject: Object that get a Rigidbody.\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        private void AddPhysics(GameObject sliceObject)
        {
            Rigidbody rb = sliceObject.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            //rb.AddExplosionForce(cutForce, sliceObject.transform.position, 0.1f);
        }
        
        /// <summary>
        /// Description:        Add an Meshcollider to the given gameobject.\n
        /// Author:             Lukas Konietzka\n
        /// Args:               sliceObject: Object that get a Meshcollider.\n    
        /// Returns:            None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        private static void AddCollider(GameObject sliceObject)
        {
            MeshCollider mc = sliceObject.AddComponent<MeshCollider>();
            mc.convex = true;
        }
        
        /// <summary>
        /// Description:    Add an XRGrabInteractable script to the given GameObject.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           sliceObject: Object that get a XRGrabInteractable script.\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        private static void AddXRGrab(GameObject sliceObject)
        {
            XRGrabInteractable xrGrab = sliceObject.AddComponent<XRGrabInteractable>();
            xrGrab.movementType = XRBaseInteractable.MovementType.VelocityTracking;
        }

        /// <summary>
        /// Description:    Add the "Sliceable" Layer Mask to the given GameObject.\n
        /// Author:         Lukas Konietzka\n
        /// Args:           sliceObjects: Object that get a new Layermask.\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        public void ChangeLayerMask(GameObject sliceObject)
        {
            int changingLayer = 15;

            // Getting the index of the Layer "Sliceable" = 15
            int LayerSliceable = LayerMask.NameToLayer("Slicable");
            sliceObject.layer = LayerSliceable;
            sliceObject.layer = changingLayer;
        }
        
        /// <summary>
        /// Description:    Change the tag of the given GameObject to "Topping"\n
        /// Author:         Lukas Konietzka\n
        /// Args:           GameObject that has to bee changed\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        public void ChangeTag(GameObject sliceObject)
        {
            sliceObject.gameObject.tag = "Topping";
        }
        
        /// <summary>
        /// Description:    Disable the XRInteractable on the given GameObject\n
        /// Author:         Lukas Konietzka\n
        /// Args:           GameObject that has to bee changed\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        public void DisableXRGrab(GameObject sliceObject)
        {
            XRGrabInteractable xrGrab = sliceObject.GetComponent<XRGrabInteractable>();
            xrGrab.enabled = !xrGrab.enabled;
        }

        /// <summary>
        /// Description:    Enable the XRInteractable on the given GameObject\n
        /// Author:         Lukas Konietzka\n
        /// Args:           GameObject that has to bee changed\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        public void EnableXRGrab(GameObject sliceObject)
        {
            XRGrabInteractable xrGrab = sliceObject.GetComponent<XRGrabInteractable>();
            xrGrab.enabled = xrGrab.enabled;
        }

        /// <summary>
        /// Description:    Disable physics an the given GameObject\n
        /// Author:         Lukas Konietzka\n
        /// Args:           GameObject that has to bee changed\n
        /// Returns:        None\n
        /// </summary>
        /// <param name="sliceObject"></param>
        public void DisablePhysics(GameObject sliceObject)
        {
            Rigidbody rb = sliceObject.GetComponent<Rigidbody>();
            UnityEngine.Object.Destroy(rb);
        }

        /// <summary>
        /// Description:    Start playing cutting sound by calling the SoundManager\n
        /// Author:         Theresa Mayer\n
        /// Args:           None\n
        /// Returns:        None\n
        /// </summary>
        private void PlayKnifeAudio()
        {
            SoundManager.GetInstance.StartSfx("knife");
        }
    }
}