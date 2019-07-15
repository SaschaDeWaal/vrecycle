using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaEvents : MonoBehaviour {


	public void OnLeave() {
		if (GameStats.Instance.IsPlaying) {
			GameStats.Instance.PauseGame();
		}
	}

	public void OnReturn() {
		if (GameStats.Instance.isPaused) {
			GameStats.Instance.ResumeGame();
		}
	}
}
