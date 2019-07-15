using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class TrackPart : MonoBehaviour {

	[Header("Track")] 
	[SerializeField] private float speed = 0.2f;
	[SerializeField] private Vector3 direction = Vector3.forward;


	public Vector3 Direction { get; private set; }

	public float Speed => speed;


	private void Start() {
		Direction = RotateDirection(direction).normalized;
	}

	private void Update() {
		if (!Application.isPlaying) {
			EditorUpdate();
		} else {
			PlayUpdate();
		}
	}

	private void EditorUpdate() {
		Debug.DrawLine(transform.position + new Vector3(0, 0.5f, 0), transform.position + new Vector3(0, 0.5f, 0) + Direction.normalized, Color.red);
		Debug.DrawLine(transform.position + new Vector3(0, -0.5f, 0) + Direction.normalized, transform.position + new Vector3(0, 1f, 0) + Direction.normalized, Color.blue);
	}

	private void PlayUpdate() {
		
	}

	private Vector3 RotateDirection(Vector3 dir) {
		dir = Quaternion.Euler(transform.eulerAngles) * dir;
		return dir;
	}
}