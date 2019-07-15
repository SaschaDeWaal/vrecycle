using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelText : MonoBehaviour {
    
    private Text _text;
    
    private void Start() {
        _text = GetComponent<Text>();
        _text.text = GameStats.Instance.Level.ToString();

        GameStats.Instance.LevelChanged += OnLevelChanged;
    }

    private void OnDestroy() {
        GameStats.Instance.LevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level) {
        _text.text = level.ToString();
    }
}
