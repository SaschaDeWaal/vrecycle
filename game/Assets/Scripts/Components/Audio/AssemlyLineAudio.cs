using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AssemlyLineAudio : MonoBehaviour {

    [SerializeField] private float volumeSpeed = 0.5f;

    private AudioSource _audioSource;
    private bool isPlaying = false;
    private void OnEnable() {
        _audioSource = GetComponent<AudioSource>();
        
        GameStats.Instance.PlayingStateChanged += OnPlayingStateChanged;
        _audioSource.time = Random.Range(0, _audioSource.clip.length);
        _audioSource.volume = 0f;
        _audioSource.Play();
    }

    private void OnDisable() {
        GameStats.Instance.PlayingStateChanged -= OnPlayingStateChanged;
    }

    private void Update() {
        float add = Time.deltaTime * volumeSpeed * (isPlaying ? 1f : -1f);
        _audioSource.volume = Mathf.Clamp(_audioSource.volume + add, 0f, 1f);
    }

    private void OnPlayingStateChanged(bool state, bool wasPaused) {
        isPlaying = state;
    }
}
