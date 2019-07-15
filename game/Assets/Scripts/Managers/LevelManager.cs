using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    private void OnEnable() {
        GameStats.Instance.LevelChanged += OnLevelChange;
    }

    private void OnDisable() {
        GameStats.Instance.LevelChanged -= OnLevelChange;
    }


    private void OnLevelChange(int level) {
        for(int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(level - 1 == i);
        }
        
    }

}
