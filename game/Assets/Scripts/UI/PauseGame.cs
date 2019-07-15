using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

    [SerializeField] private Text text;
    [SerializeField] private VrButton vrButton;

    private void Start() {
        GameStats.Instance.PlayingStateChanged += OnPlayingStateChanged;

        
    }


    public void OnPressed() {
        UpdateButtonState();
        
        if (GameStats.Instance.isPaused) {
            GameStats.Instance.ResumeGame();
        } else {
            GameStats.Instance.PauseGame();
        }
    }

    private void OnPlayingStateChanged(bool startPlaying, bool wasPaused) {
        UpdateButtonState();
    }

    private void UpdateButtonState() {
        vrButton.Enable(GameStats.Instance.IsPlaying || GameStats.Instance.isPaused);
        text.text = (GameStats.Instance.isPaused) ? "Resume" : "Pause";
    }
}
