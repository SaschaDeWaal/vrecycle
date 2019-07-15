using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitch : MonoBehaviour {
    
	private enum Direction {
		NextLevel,
		PrevLevel
	}

	[SerializeField] private Direction direction = Direction.NextLevel;
	[SerializeField] private VrButton vrButton;

	private void Start() {
		GameStats.Instance.PlayingStateChanged += OnPlayingStateChanged;
		GameStats.Instance.LevelChanged += OnLevelChanged;

	}

	private void OnDestroy() {
		GameStats.Instance.PlayingStateChanged -= OnPlayingStateChanged;
		GameStats.Instance.LevelChanged -= OnLevelChanged;
	}
	public void OnPressed() {
		if (GameStats.Instance.IsPlaying || GameStats.Instance.isPaused) {
			return;
		}

		if (direction == Direction.NextLevel) {
			GameStats.Instance.NextLevel();
		} else {
			GameStats.Instance.PrevLevel();
		}
			}
	
	private void OnPlayingStateChanged(bool startPlaying, bool wasPaused) {
		vrButton.Enable(CanBePressed());
	}

	private void OnLevelChanged(int level) {
		vrButton.Enable(CanBePressed());
	}

	private bool CanBePressed() {
		return  GameStats.Instance.IsPlaying == false &&
		        GameStats.Instance.isPaused == false &&
		        ((direction == Direction.NextLevel) ? GameStats.Instance.isNextLevel : GameStats.Instance.isPrevLevel);
	}

}
