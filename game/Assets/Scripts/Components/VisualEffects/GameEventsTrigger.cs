using UnityEngine;
using UnityEngine.Events;

public class GameEventsTrigger : MonoBehaviour {

    [Header("Events")]
    [SerializeField] private UnityEvent onStartPlaying;
    [SerializeField] private UnityEvent onStopPlaying;
    [SerializeField] private UnityEvent onScoreAdded;
    [SerializeField] private UnityEvent onScoreRemoved;
    
    private void OnEnable() {
        GameStats.Instance.PlayingStateChanged += OnPlayingStateChanged;
        GameStats.Instance.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable() {
        GameStats.Instance.PlayingStateChanged -= OnPlayingStateChanged;
        GameStats.Instance.ScoreChanged -= OnScoreChanged;
    }

    private void OnPlayingStateChanged(bool state, bool wasPaused) {
        if (state) {
            onStartPlaying?.Invoke();
        } else {
            onStopPlaying?.Invoke();
        }
    }

    private void OnScoreChanged(int scoreChangedAmount, int totalScore, bool added) {
        if (added) {
            onScoreAdded?.Invoke();
        }
        else {
            onScoreRemoved?.Invoke();
        }
    }

}
