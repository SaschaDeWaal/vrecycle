using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class StackableMovement : MonoBehaviour {

	private Rigidbody _rigidbody;
    
    private StackableMovement _followStackable = null;
    private Vector3 _relativePosition = Vector3.zero;
    private Quaternion _relativeRotation;
    private LayerMask _layerMask;

    private void Start() {
	    enabled = false;
	    
	    _rigidbody = GetComponent<Rigidbody>();
	    _layerMask = LayerMask.GetMask("StackableMovement");
    }

    private void Update() {
	    return;
	    
	    if (_followStackable) {

		    Vector3 newPos = _followStackable.transform.position + RotateDirection(_relativePosition);
		    newPos.y = transform.position.y;
		    _rigidbody.MovePosition(newPos);
		    		    
		    RaycastHit hit;		    
		    if (!Physics.Raycast(transform.position, Vector3.down, out hit, 1, _layerMask)) {
			    _followStackable = null;
			    enabled = false;
		    }
		    
	    }

    }

    private void OnCollisionEnter(Collision other) {
	    	    
	    if (other.transform.position.y < transform.position.y && _rigidbody.velocity.sqrMagnitude < 0.5f) {

		    StackableMovement stackableMovement = other.transform.GetComponent<StackableMovement>();

		    if (stackableMovement) {
			    _followStackable = stackableMovement;
			    _relativePosition = transform.position - other.transform.position;
			    enabled = true;
		    }
	    }
    }

    private void OnCollisionExit(Collision other) {
	    StackableMovement stackableMovement = other.transform.GetComponent<StackableMovement>();

	    if (stackableMovement && stackableMovement == _followStackable) {
		    enabled = false;
		    _followStackable = null;
	    }
    }
    
    private Vector3 RotateDirection(Vector3 dir) {
	    dir = Quaternion.Euler(_followStackable.transform.eulerAngles) * dir;
	    return dir;
    }
}
