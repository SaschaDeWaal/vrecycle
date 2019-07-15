using System.Collections.Generic;
using UnityEngine;

public class FollowTrack : MonoBehaviour {

    private readonly List<TrackPart> _currentTrackPart = new List<TrackPart>();
    private Rigidbody _rigidbody;

    [SerializeField] private bool followingTrack = false;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update() {
        if(!GameStats.Instance.IsPlaying) return;

        followingTrack = false;
        
        _currentTrackPart.ForEach(track => {;
            Vector3 newPos = transform.position + (track.Direction * Time.deltaTime * track.Speed);
            _rigidbody.MovePosition(newPos);

            followingTrack = true;
        });

    }

    private void OnEnable() {
        _currentTrackPart.Clear();
    }

    private void OnCollisionEnter(Collision other) {
        TrackPart track = other.gameObject.GetComponent<TrackPart>();

        if (track) {
            _currentTrackPart.Add(track);
        }
    }

    private void OnCollisionExit(Collision other) {
        TrackPart track = other.gameObject.GetComponent<TrackPart>();
        
        if (_currentTrackPart.Contains(track)) {
            _currentTrackPart.Remove(track);
        }
    }
}
