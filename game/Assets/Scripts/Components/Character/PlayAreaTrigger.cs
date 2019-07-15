using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class PlayAreaTrigger : MonoBehaviour {

    [Header("Events")] 
    [SerializeField] private UnityEvent onLeavePlayArea;
    [SerializeField] private UnityEvent onReturnPlayArea;
    
    private GameObject _camera;
    private Collider _collider;
    private bool _wasInPlayArea = false;
    
    private void Start() {
        Renderer renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        _camera = Camera.main.gameObject;

        if (renderer) {
            renderer.enabled = false;
        }
    }

    private void Update() {
        Vector3 camPos = _camera.transform.position;
        bool isInPlayArea = _collider.bounds.Contains(camPos);        

        if (_wasInPlayArea != isInPlayArea) {
            _wasInPlayArea = isInPlayArea;

            if (isInPlayArea) {
                OnReturnPlayArea();
            } else {
                OnLeftPlayArea();
            }
        }
    }

    private void OnLeftPlayArea() {
        onLeavePlayArea?.Invoke();

        if (GameStats.Instance.IsPlaying) {
            GameStats.Instance.PauseGame();
        }
    }

    private void OnReturnPlayArea() {
        onReturnPlayArea?.Invoke();

        if (GameStats.Instance.isPaused) {
            Debug.Log("resumed");
            GameStats.Instance.ResumeGame();
        }
    }

}
