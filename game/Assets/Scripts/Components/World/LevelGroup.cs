using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class LevelGroup : MonoBehaviour {
        
    public bool IsActive => enabled;
    
    private void OnEnable() {
        StartCoroutine(EnableAnimation());
    }

    private IEnumerator EnableAnimation() {
        float scale = 0;
        float time = 1f;

        while (time >= 0.0f) {
            time -= Time.deltaTime * 4;

            transform.localScale = Vector3.Lerp(new Vector3(1, 0, 1), Vector3.one, 1f - time);
                
            yield return name;
        }

    }
}
