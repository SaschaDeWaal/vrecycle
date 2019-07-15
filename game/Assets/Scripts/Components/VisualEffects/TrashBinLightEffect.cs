using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class TrashBinLightEffect : MonoBehaviour {
	[SerializeField] private float maxLightIntense = 20;
	[SerializeField] private float fadeOutTime = 0.5f;
	[SerializeField] private float waitTime = 0.1f;
	[SerializeField] private Color correctColor = Color.green;
	[SerializeField] private Color incorrectColor = Color.red;

	private Light _light;
	private float _timer = 0;

	private void Start() {
		_light = GetComponent<Light>();

		_light.intensity = 0f;
	}

	public void OnCorrectEntered() {
		StopAllCoroutines();
		StartCoroutine(LightAnimation(correctColor));
	}

	public void OnIncorrectEntered() {
		StopAllCoroutines();
		StartCoroutine(LightAnimation(incorrectColor));
	}


	private IEnumerator LightAnimation(Color color) {
		_light.color = color;
		_light.intensity = maxLightIntense;

		yield return new WaitForSeconds(waitTime);

		// fade out
		_timer = 0;
		while (_timer < fadeOutTime) {
			_timer += Time.deltaTime;
			_light.intensity = (1f - Mathf.Sin((_timer / fadeOutTime))) * maxLightIntense;
			yield return null;
		}

		_light.intensity = 0f;
	}
}