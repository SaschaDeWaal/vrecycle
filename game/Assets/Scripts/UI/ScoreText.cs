using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour {

    private Text _text;
    
    private void Start() {
        _text = GetComponent<Text>();
        _text.text = GameStats.Instance.Score.ToString();

        GameStats.Instance.ScoreChanged += OnScoreChanged;
    }

    private void OnDestroy() {
        GameStats.Instance.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int changedAmount, int totalScore, bool added) {
        _text.text = totalScore.ToString();
    }
}
