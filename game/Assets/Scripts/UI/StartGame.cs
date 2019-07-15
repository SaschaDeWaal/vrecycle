using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	[SerializeField] private float time = 60 * 3;
	[SerializeField] private VrButton vrButton;
	[SerializeField] private Text text;
	
	private void Start() {
		GameStats.Instance.PlayingStateChanged += OnPlayingStateChanged;
	}

	private void OnDestroy() {
		GameStats.Instance.PlayingStateChanged -= OnPlayingStateChanged;
	}

	public void OnPressed() {
		if (!GameStats.Instance.IsPlaying) {
			GameStats.Instance.StartGame(time);
		}
		else {
			GameStats.Instance.StopGame();
		}
	}
	
	private void OnPlayingStateChanged(bool startPlaying, bool wasPaused) {
		vrButton.SetNeedAccept(startPlaying);

		text.text = (startPlaying) ? "Stop" : (wasPaused) ? "Restart" : "Start";
	}
}
