using System;
using System.Collections;
using System.Collections.Generic;
using Attribute.MyBox;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))] [RequireComponent(typeof(AudioSource))] [RequireComponent(typeof(Image))]
public class VrButton : MonoBehaviour {

    [Header("Click Effect")] 
    [SerializeField] private bool changeColorOfImage;

    [SerializeField] private bool pressTwoTimes = false;
    [SerializeField] private bool startEnabled = true;
    [SerializeField] private bool triggerOnStart = false;

    [ConditionalField("changeColorOfImage")] [SerializeField] private Color colorWhenPressed = Color.white;
    [ConditionalField("changeColorOfImage")] [SerializeField] private Color colorWhenNotPressed = Color.white;
    [ConditionalField("changeColorOfImage")] [SerializeField] private Color disabledColor = Color.white;


    [ConditionalField("pressTwoTimes")] [SerializeField] private Text text;
    
    [Header("Click events")]
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;

    private Image _image;
    private AudioSource _audioSource;
    private bool _enabled = true;
    private bool _pressedBefore = false;
    
    private void Start() {
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();

        _enabled = startEnabled;
                
        if (changeColorOfImage) {
            _image.color = (startEnabled) ? colorWhenNotPressed : disabledColor;
        }

        if (triggerOnStart) {
            StartCoroutine(Trigger());
        }
    }

    public void Enable(bool state) {
        _enabled = state;
        
        if (changeColorOfImage) {
            _image.color = (_enabled) ? colorWhenNotPressed : colorWhenPressed;
        }
    }

    public void SetNeedAccept(bool state) {
        pressTwoTimes = state;
    }

    private void OnTriggerEnter(Collider other) {
        if (!_enabled) return;
        
        _audioSource.Play();

        if (pressTwoTimes && !_pressedBefore) {
            _pressedBefore = true;
            StartCoroutine(TimeOut());
            return;
        }
        StopAllCoroutines();
        _pressedBefore = false;
        
        onPressed?.Invoke();

        if (changeColorOfImage) {
            _image.color = colorWhenPressed;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (!_enabled) return;
        
        onReleased?.Invoke();
        
        if (changeColorOfImage) {
            _image.color = colorWhenNotPressed;
        }
    }
    
    private IEnumerator TimeOut() {
        string originalText = text.text;

        for (int i = 3; i > 0; i--) {
            text.text = $"Accept ({i})";
            yield return new WaitForSeconds(1f);
        }

        _pressedBefore = false;
        text.text = originalText;
    }

    private IEnumerator Trigger() {
        yield return new WaitForSeconds(1);
        onPressed?.Invoke();
    }
}
