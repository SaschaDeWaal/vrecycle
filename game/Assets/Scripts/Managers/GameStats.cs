using System;
using UnityEngine;

public delegate void ScoreChangedEventHandler(int scoreChangedAmount, int totalScore, bool added);
public delegate void LevelChangedEventHandler(int level);
public delegate void PlayStateChangedEventHandler(bool started, bool wasPaused);

public class GameStats : MonoBehaviour {

	private const int LEVELS = 10;

	private int _score = 0;
	private int _level = 1;
	private bool _playing = false;
	private float _timer = 0;

	public int Score => _score;
	public int Level => _level;
	public float PlayTime => _timer;
	public bool IsPlaying => _playing;
	public bool isPaused => !_playing && _timer > 0;
	public bool isNextLevel => _level < LEVELS;
	public bool isPrevLevel => _level > 1;
	
	public event ScoreChangedEventHandler ScoreChanged;
	public event PlayStateChangedEventHandler PlayingStateChanged;
	public event LevelChangedEventHandler LevelChanged;

	private void Update() {
		if (_playing) {
			_timer -= Time.deltaTime;

			if (_timer <= 0.0f) {
				OnTimeOut();
			}
		}
	}

	public void AddScore(int score) {
		if (!IsPlaying) return;
		
		_score += Math.Max(0, score);
		ScoreChanged?.Invoke(score, _score, true);
	}

	public void RemoveScore(int score) {
		if (!IsPlaying) return;
		
		_score -= Mathf.Max(0, score);
		ScoreChanged?.Invoke(score, _score, false);
	}

	public void NextLevel() {
		_level = Mathf.Clamp(_level+1, 1, LEVELS);
		LevelChanged?.Invoke(_level);
	}

	public void PrevLevel() {
		_level = Mathf.Clamp(_level-1, 1, LEVELS);
		LevelChanged?.Invoke(_level);
	}

	public void StartGame(float time) {
		if (IsPlaying) return;
		
		_timer = time;
		_score = 0;
		_playing = true;
		
		PlayingStateChanged?.Invoke(true, false);
		ScoreChanged?.Invoke(0, _score, false);
	}

	public void StopGame() {
		_playing = false;
		_timer = 0;
		_score = 0;
		
		PlayingStateChanged?.Invoke(false, false);
		ScoreChanged?.Invoke(0, _score, false);
	}

	public void PauseGame() {
		if (!IsPlaying) return;
		
		_playing = false;
		PlayingStateChanged?.Invoke(false, true);
	}

	public void ResumeGame() {
		if (IsPlaying) return;
		
		_playing = true;
		PlayingStateChanged?.Invoke(true, true);
	}

	private void OnTimeOut() {
		_playing = false;
		PlayingStateChanged?.Invoke(false, false);
	}

	private static GameStats _gameStats = null;
	public static GameStats Instance {
		get {
			if (_gameStats == null) {
				GameObject obj = Instantiate(new GameObject());
				_gameStats = obj.AddComponent<GameStats>();
				_gameStats.name = "GameStats";
			}
			return _gameStats;
		}
	}
}
