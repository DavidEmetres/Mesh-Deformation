using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ShootObjectBehaviour : MonoBehaviour {

	private Rigidbody rigidbody;

	private float forceOffset = 0.1f;
	private float forceMultiplier = 3f;

	private bool alreadyHit = false;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();

		Invoke ("Destroy", 5f);
	}

	void Update () {
		
	}

	void OnCollisionEnter(Collision col) {
		PermanentMeshDeformer deformer = col.collider.GetComponent<PermanentMeshDeformer> ();

		if (deformer && !alreadyHit) {
			Vector3 point = col.contacts [0].point;
			point += col.contacts [0].normal * forceOffset;
			deformer.AddDeformingForce (point, rigidbody.velocity.magnitude * forceMultiplier);
		}

		alreadyHit = true;
		GetComponent<DontGoThroughThings> ().enabled = false;
	}

	void Destroy() {
		Destroy (this.gameObject);
	}
}
