using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceivePointsEffect : MonoBehaviour {

    
    [SerializeField] private Text text;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float height = 1f;
    
    private Vector3 _startPos;
    private int points = 0;
    private int penalty = 0;
    
    private void Start() {
        _startPos = transform.localPosition;
    }

    public void SetScoreAmounts(int points, int penalty) {
        this.points = points;
        this.penalty = penalty;
    }

    public void OnEnteredCorrect() {
        StopAllCoroutines();
        StartCoroutine(Animation(points, true));
    }
    
    public void OnEnteredIncorrect() {
        StopAllCoroutines();
        StartCoroutine(Animation(penalty, false));
    }

    private IEnumerator Animation(int points, bool up) {
        text.text = (up) ? "+" + points : "-" + points;
        transform.localPosition = _startPos;
                
        
        float timer = 0;

        while (timer < animationTime) {
            timer += Time.deltaTime;
            
            float delta = timer / animationTime;
            transform.localPosition = _startPos + (Vector3.up * delta * animationTime);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f-delta);
            
            
            yield return null;
        }

        transform.localPosition = _startPos;
        
    }

}
