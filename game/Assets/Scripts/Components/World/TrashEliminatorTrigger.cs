using System.Collections;
using System.Collections.Generic;
using Attribute.MyBox;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class TrashEliminatorTrigger : MonoBehaviour {

	[Header("Score system")]
	[SerializeField] private bool receivePoints;
	
	[ConditionalField("receivePoints")]
	[SerializeField] private TrashTypes correctType;
	
	[ConditionalField("receivePoints")]
	[SerializeField] private int correctPoints = 0;
	
	[ConditionalField("receivePoints")]
	[SerializeField] private int penaltyPoints = 0;

	[Header("Events")] 
	[SerializeField] private UnityEvent onEnteredCorrect;
	[SerializeField] private UnityEvent onEnteredIncorrect;

	private LevelGroup _levelGroup;
	
	private void Start() {
		Renderer renderer = GetComponent<Renderer>();
		
		if (renderer) {
			renderer.enabled = false;
		}
	}

	public void SetScoreAmounts(int score, int penalty) {
		receivePoints = true;
		correctPoints = score;
		penaltyPoints = penalty;
	}

	private void OnEnteredCorrect(TrashComponent trashComponent) {
		if (!GameStats.Instance.IsPlaying) {
			return;
		}
		
		GameStats.Instance.AddScore(correctPoints);
		onEnteredCorrect?.Invoke();
	}

	private void OnEnteredIncorrect(TrashComponent trashComponent) {
		if (!GameStats.Instance.IsPlaying) {
			return;
		}
		
		GameStats.Instance.RemoveScore(penaltyPoints);
		onEnteredIncorrect?.Invoke();
	}

	private void OnTriggerEnter(Collider other) {
		TrashComponent trashComponent = other.transform.GetComponent<TrashComponent>();

		if (trashComponent) {
			if (receivePoints) {
				if (correctType == trashComponent.TrashType) {
					OnEnteredCorrect(trashComponent);
				} else {
					OnEnteredIncorrect(trashComponent);
				}
			}
			
			Factory.Instance.RemoveTrash(other.gameObject);
		}
	}
}