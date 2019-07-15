using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Factory : MonoBehaviour {

	private readonly Dictionary<TrashTypes, List<GameObject>> _availableObjects = new Dictionary<TrashTypes, List<GameObject>>();

	private TrashDatabase _trashDatabase;
	
	public GameObject CreateTrash(TrashTypes type) {
		if (_availableObjects.ContainsKey(type) && _availableObjects[type].Any()) {
			GameObject obj = _availableObjects[type][0];
			_availableObjects[type].Remove(obj);

			obj.SetActive(true);

			return obj;
		}

		return Instantiate(TrashDatabase.templates[(int) type]);
	}

	public GameObject CreateTrash(TrashTypes type, Vector3 position, Quaternion rotation) {
		GameObject obj = CreateTrash(type);
		obj.transform.position = position;
		obj.transform.rotation = rotation;

		return obj;
	}

	public void RemoveTrash(GameObject gameObject) {
		TrashTypes trashType = gameObject.GetComponent<TrashComponent>().TrashType;

		if (!_availableObjects.ContainsKey(trashType)) {
			_availableObjects[trashType] = new List<GameObject>();
		}
		
		_availableObjects[trashType].Add(gameObject);
		gameObject.SetActive(false);
		
		return;
	}

	private TrashDatabase TrashDatabase {
		get {
			if (!_trashDatabase) {
				_trashDatabase = Resources.Load<TrashDatabase>("TrashDatabase");
			}

			return _trashDatabase;
		}
	}

	private static Factory _factory = null;
	public static Factory Instance {
		get {
			if (_factory == null) {
				GameObject obj = Instantiate(new GameObject());
				_factory = obj.AddComponent<Factory>();
				_factory.name = "Factory";
			}
			return _factory;
		}
	}

}
