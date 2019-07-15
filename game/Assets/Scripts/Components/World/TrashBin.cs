using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBin : MonoBehaviour {

    [Header("Score system")]
    [SerializeField] private int points = 2;
    [SerializeField] private int penalty = 5;

    [Header("Components")] 
    [SerializeField] private TrashEliminatorTrigger trashEliminatorTrigger;
    [SerializeField] private ReceivePointsEffect receivePointsEffect;
    [SerializeField] private Text pointsText;
    

    private void Start() {
        trashEliminatorTrigger.SetScoreAmounts(points, penalty);
        receivePointsEffect.SetScoreAmounts(points, penalty);
        pointsText.text = (points == 0) ? $"-{penalty}" : points.ToString();
    }



}
