using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrashDatabase", menuName = "ScriptableObjects/TrashDatabase", order = 1)]
public class TrashDatabase : ScriptableObject {
	public List<GameObject> templates;
}
