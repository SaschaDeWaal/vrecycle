using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FollowXRNode : MonoBehaviour {
    
    [Header("Controller")] [SerializeField]
    private XRNode xrNode = XRNode.LeftHand;

    private void Update()
    {
        transform.localPosition = InputTracking.GetLocalPosition(xrNode);
        transform.localRotation = InputTracking.GetLocalRotation(xrNode);

    }
}
