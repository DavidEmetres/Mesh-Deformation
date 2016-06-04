using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class PermanentMeshDeformer: MonoBehaviour {

	Mesh deformingMesh;
	Vector3[] originalVertices, displacedVertices;
	Vector3[] vertexVelocities;
	float uniformScale = 1f;
	MeshCollider meshCol;

	public float springForce = 20f;
	public float damping = 5f;

	private float maximunDeformation = 1f;
	private float deformationLevel = 0f;

	void Start() {
		deformingMesh = GetComponent<MeshFilter> ().mesh;
		meshCol = GetComponent<MeshCollider> ();
		originalVertices = deformingMesh.vertices;
		displacedVertices = new Vector3[originalVertices.Length];
		for (int i = 0; i < originalVertices.Length; i++) {
			displacedVertices [i] = originalVertices [i];
		}
		vertexVelocities = new Vector3[originalVertices.Length];
	}

	void Update() {
		uniformScale = transform.localScale.x;
		for (int i = 0; i < displacedVertices.Length; i++) {
			UpdateVertex (i);
		}
		deformingMesh.vertices = displacedVertices;
		UpdateMeshCollider ();
	}

	public void AddDeformingForce(Vector3 point, float force) {
		deformationLevel += force;
		point = transform.InverseTransformPoint (point);
		for (int i = 0; i < displacedVertices.Length; i++) {
				AddForceToVertex (i, point, force);
		}
	}

	void AddForceToVertex(int i, Vector3 point, float force) {
		Vector3 pointToVertex = displacedVertices [i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities [i] += pointToVertex.normalized * velocity;
	}

	void UpdateVertex(int i) {
		Vector3 velocity = vertexVelocities [i];
		Vector3 displacement = displacedVertices [i] - originalVertices [i];
		displacement *= uniformScale;
		if (displacement.magnitude < maximunDeformation) {
			velocity *= 1f - damping * Time.deltaTime;
			vertexVelocities [i] = velocity;
			displacedVertices [i] += velocity * (Time.deltaTime / uniformScale);
		}
	}

	void UpdateMeshCollider() {
		meshCol.sharedMesh = null;
		meshCol.sharedMesh = deformingMesh;
	}
}
