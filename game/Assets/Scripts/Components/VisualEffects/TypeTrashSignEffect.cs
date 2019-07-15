using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeTrashSignEffect : MonoBehaviour {

	private const string PARAMETER_NAME = "IncorrectHasEntered";

	[SerializeField] private Animator _animator;

	public void OnIncorrectEnteredType() {
		StartCoroutine(AnimationTimer());
	}

	private IEnumerator AnimationTimer() {
		_animator.SetBool(PARAMETER_NAME, true);
		yield return new WaitForSeconds(2);
		
		_animator.SetBool(PARAMETER_NAME, false);
	}
	
}
