using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class ImpactAudio : MonoBehaviour {

    private const float MIN_TIME = 0.1f;
    private const float MIN_VELOCITY = 1f;
    private const float MIN_ANGULAR_VELOCITY = 30f;
    
    [SerializeField] private AudioClip[] audioClips;
    
    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private float _lastAngularVelocity = 0;
    private float _lastPlayedAudio = 0;
    private bool _checkAngular = false;
    
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        float impulse = other.impulse.sqrMagnitude;
        _checkAngular = true;
        
        if (impulse > MIN_VELOCITY) {
            PlayImpactSound();
        }
    }


    private void OnCollisionStay(Collision other) {

        if (_rigidbody.IsSleeping() || !_checkAngular) {
            return;
        }

        float curAngularVelocity = _rigidbody.angularVelocity.sqrMagnitude;
        float diff = _lastAngularVelocity - curAngularVelocity;
        bool hit = (diff >= MIN_ANGULAR_VELOCITY);
        
        _lastAngularVelocity = curAngularVelocity;
        
        if (hit) {
            PlayImpactSound();
        }

        if (curAngularVelocity < 0.001f) {
            _checkAngular = false;
        }
    }

    private void PlayImpactSound() {
        if (Time.time - _lastPlayedAudio > MIN_TIME) {
            AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length)];
            _audioSource.PlayOneShot(audioClip);
            _lastPlayedAudio = Time.time;
        }
    }
}
