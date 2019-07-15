using System;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class CarryableObject : MonoBehaviour {
	
	private Rigidbody _rigidbody;
	private bool _isCarried = false;
	private Action _handOverAction;
	
	private void Start() {
		_rigidbody = GetComponent<Rigidbody>();
	}

	public void OnGrab(Action onHandOver) {
		if (_isCarried) {
			_handOverAction();
		}
		
		_isCarried = true;
		_handOverAction = onHandOver;
		_rigidbody.isKinematic = true;
	}

	public void OnRelease(Vector3 velocity, Vector3 angularVelocity) {
		_isCarried = false;
		_rigidbody.isKinematic = false;
		_rigidbody.velocity = velocity;
		_rigidbody.angularVelocity = angularVelocity;
	}
	
}
