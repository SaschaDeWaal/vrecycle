using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashSpawner : MonoBehaviour {

    [SerializeField] private TrashTypes[] trahTypes;
    [SerializeField] private float interval;
    [SerializeField] private float startDelay = 0;
    
    [SerializeField] private UnityEvent onSpawnedTrash;

    private float timer = 0;
    private readonly List<GameObject> _createdObjects = new List<GameObject>();

    private void Start() {
        GameStats.Instance.PlayingStateChanged += OnPlayingStateChanged;
        GameStats.Instance.LevelChanged += OnLevelChanged;
        
        enabled = GameStats.Instance.IsPlaying;
    }
    
    private void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0.0f) {
            timer += interval;
            CreateTrash();
        }
    }

    private void OnDestroy() {
        GameStats.Instance.PlayingStateChanged -= OnPlayingStateChanged;
        GameStats.Instance.LevelChanged -= OnLevelChanged;
    }

    private void OnPlayingStateChanged(bool startPlaying, bool wasPaused) {
        if (startPlaying && !wasPaused) {
            RemoveAllTrash();
        }

        enabled = startPlaying;
        timer = interval + startDelay;
    }

    private void OnLevelChanged(int level) {
        RemoveAllTrash();
    }

    private void CreateTrash() {
        int random = Random.Range(0, trahTypes.Length);
        GameObject obj = Factory.Instance.CreateTrash(trahTypes[random], transform.position, Quaternion.Euler(0, 0, 0));
        
        _createdObjects.Add(obj);
        onSpawnedTrash?.Invoke();
    }

    private void RemoveAllTrash() {
        _createdObjects.ForEach(o => {
            if (o != null && o.activeSelf) {
                Factory.Instance.RemoveTrash(o);
            }
        });
        _createdObjects.Clear();
    }
}
