using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct AudioClipsGroup {
    public string name;
    public AudioClip[] audioClips;

    private int playIndex;
    private AudioClip[] shuffledClips;
    
    public bool ContainsAudio => audioClips.Any();

    public void ShuffleList() {
        playIndex = 0;
        shuffledClips = audioClips.OrderBy(x => Random.value).ToArray();
    }

    public AudioClip Next() {
        if (audioClips.Length == 0) {
            Debug.LogError("The audioClips list is empty");
            return null;
        }
        
        playIndex++;

        if (shuffledClips == null || playIndex >= shuffledClips.Length) {
            ShuffleList();
        }
        
        return shuffledClips[playIndex];
    }

}

[RequireComponent(typeof(AudioSource))]
public class SoundEventComponent : MonoBehaviour {

    [SerializeField] private AudioClipsGroup[] audioClipsGroup;
    
    private AudioSource _audioSource;
    
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void TriggerRandomSound(int groupIndex) {
        if (groupIndex < 0 || groupIndex >= audioClipsGroup.Length || !audioClipsGroup[groupIndex].ContainsAudio) {
            Debug.LogError($"Sound group {groupIndex} doesn't exist or doesn't have sounds");
            return;
        }

        AudioClip audioClip = audioClipsGroup[groupIndex].Next();
        _audioSource.PlayOneShot(audioClip);
    }

    public void TriggerRandomSound(string groupName) {
        for(int i = 0; i < audioClipsGroup.Length; i++) {
            if (audioClipsGroup[i].name == groupName) {
                TriggerRandomSound(i);
                return;
            }
        }
        
        Debug.LogError($"Sound group ${groupName} doesn't exist");
    }
}
