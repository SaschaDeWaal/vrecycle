using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashComponent : MonoBehaviour {

    [SerializeField] private TrashTypes trashType;

    public TrashTypes TrashType => trashType;

    private void OnEnable() {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        
        rigidbody.velocity = new Vector3(0f,0f,0f); 
        rigidbody.angularVelocity = new Vector3(0f,0f,0f);
    }
}
