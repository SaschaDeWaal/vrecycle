using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeText : MonoBehaviour{
    private Text _text;
    
    private void Start() {
        _text = GetComponent<Text>();
        _text.text = GameStats.Instance.Score.ToString();
    }

    private void Update() {
        if (GameStats.Instance.IsPlaying) {
            _text.text = Mathf.Round(GameStats.Instance.PlayTime).ToString();
        }
        else {
            _text.text = "--";
        }
    }

}
