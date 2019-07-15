using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions {
	
	public static Vector3 RotateY(this Vector3 v, float angle) 
	{
		float sin = Mathf.Sin(angle);
		float cos = Mathf.Cos(angle);

		float tx = v.x;
		float tz = v.z;
		v.x = (cos * tx) + (sin * tz);
		v.z = (cos * tz) - (sin * tx);

		return v;
	}

	public static Vector3 To2D(this Vector3 v) 
	{
		v = new Vector3(v.x, 0, v.z);
		return v;
	}

	public static int GetClosestPointIndex(this Vector3 v, Vector3[] points) {
		int closestIndex = 0;
		float closestSqrMagnitude = (points[0] - v).sqrMagnitude;
            
		for(int i = 0; i < points.Length; i++) {
			float sqrToThisPoint = (points[i] - v).sqrMagnitude;
			if (closestSqrMagnitude > sqrToThisPoint) {
				sqrToThisPoint = closestSqrMagnitude;
				closestIndex = i;
			}
		}

		return closestIndex;
	}
}