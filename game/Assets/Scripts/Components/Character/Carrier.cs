using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[RequireComponent(typeof(Rigidbody))]
public class Carrier : MonoBehaviour {

	private const float THROW_SPEED = 0.2f;
	
	private enum Controller {
		Left,
		Right
	}

	private enum CarryState {
		Nothing,
		Touch,
		Carry,
		HandOver,
	}
	
	[Header("Controller")] 
	[SerializeField] private Controller controller = Controller.Left;
	[SerializeField] private float velocityModifier = 1.1f;
	[SerializeField] private float angularModifier = 1.0f;
	
	[Header("Events")]
	[SerializeField] private UnityEvent onGrab;
	[SerializeField] private UnityEvent onDrop;
	[SerializeField] private UnityEvent OnThrown;

	private CarryState _carryState = CarryState.Nothing;
	private CarryableObject _carryableObject = null;
	private Quaternion _relativeRotation;

	private Quaternion _zeroRotation;
	private Vector3 _relativePosition;


	private void Update() {
		if (GameStats.Instance.isPaused) {
			return;
		}
		
		UpdateControllerTrigger();
		UpdateCarryableObject();
	}

	private void UpdateCarryableObject() {
		if (_carryState == CarryState.Carry) {
			
			if (_carryableObject == null) {
				_carryState = CarryState.Nothing;
				return;
			}
			
			Transform carryTransform = _carryableObject.transform;
			carryTransform.rotation = (transform.rotation * _relativeRotation);
			carryTransform.position = transform.TransformPoint(_relativePosition);
		}
	}

	private void UpdateControllerTrigger() {
		float trigger = Input.GetAxis((controller == Controller.Left) ? "TriggerLeft" : "TriggerRight");

		if (trigger == 1.0f && _carryState == CarryState.Touch) {
			OnGrab();
		}

		if (trigger != 1.0f && (_carryState == CarryState.Carry || _carryState == CarryState.HandOver)) {
			OnRelease();
		}
	}

	private void OnGrab() {
		if (_carryState == CarryState.Touch) {
			_carryState = CarryState.Carry;
			_carryableObject.OnGrab(OnHandOver);

			_relativeRotation = Quaternion.Inverse(transform.rotation) * _carryableObject.transform.rotation;
			_relativePosition = transform.InverseTransformPoint(_carryableObject.transform.position);
			
			_zeroRotation = transform.rotation;
			onGrab?.Invoke();
		}
	}

	private void OnRelease() {
		if (_carryState == CarryState.Carry) {
			_carryState = CarryState.Touch;
			Vector3[] velocity = GetVelocity();

			if (velocity[0].sqrMagnitude > THROW_SPEED) {
				_carryState = CarryState.Nothing;
				OnThrown?.Invoke();
			}else {
				onDrop?.Invoke();
			}
			
			_carryableObject.OnRelease(velocity[0] * velocityModifier, velocity[1] * angularModifier);
			
		}

		if (_carryState == CarryState.HandOver) {
			_carryState = CarryState.Nothing;
			_carryableObject = null;
		}
	}

	private void OnHandOver() {
		if (_carryState == CarryState.Carry) {
			_carryState = CarryState.HandOver;
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (_carryState == CarryState.Nothing) {
			CarryableObject touchedCarryableObject = other.GetComponent<CarryableObject>();

			if (touchedCarryableObject) {
				_carryState = CarryState.Touch;
				_carryableObject = touchedCarryableObject;
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (_carryState == CarryState.Touch) {
			_carryState = CarryState.Nothing;
			_carryableObject = null;
		}
	}

	private Vector3[] GetVelocity() {
		XRNode node = (controller == Controller.Left) ? XRNode.LeftHand : XRNode.RightHand;
		InputDevice device = InputDevices.GetDeviceAtXRNode(node);
		Vector3 velocity = Vector3.zero;
		Vector3 angularVelocity = Vector3.zero;
		
		device.TryGetFeatureValue(CommonUsages.deviceVelocity, out velocity);
		device.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out angularVelocity);

		return new []{velocity, angularVelocity};
	}
	
	private Vector3 RotateDirection(Quaternion quaternion, Vector3 vector3) {
		vector3 = (quaternion) * vector3;
		return vector3;
	}
	
}