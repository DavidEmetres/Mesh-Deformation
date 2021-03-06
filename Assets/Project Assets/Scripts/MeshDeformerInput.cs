﻿using UnityEngine;
using System.Collections;

public class MeshDeformerInput : MonoBehaviour {

	public float force = 10f;
	public float forceOffset = 0.1f;

	void Update() {
		if (Input.GetMouseButton (0)) {
			HandleInput ();
		}
	}

	void HandleInput() {
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (inputRay, out hit)) {
			PermanentMeshDeformer deformer = hit.collider.GetComponent<PermanentMeshDeformer> ();
			if (deformer) {
				Debug.Log ("HIT DEFORMED");
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce (point, force);
			}
		}
	}
}
