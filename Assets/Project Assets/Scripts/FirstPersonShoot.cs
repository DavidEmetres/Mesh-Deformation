using UnityEngine;
using System.Collections;

public class FirstPersonShoot : MonoBehaviour {

	private GameObject[] shootedObjects;
	private int shooted = 0;
	private float lastShootTime = -5f;
	private float timeBetweenShoots = 1f;
	private float shootingForce = 2000f;

	public GameObject shootObject;
	public Transform shootPoint;

	public void Start() {
		shootedObjects = new GameObject[3];
	}

	public void FixedUpdate() {
		//UpdateAiming ();

		UpdateAiming ();
	}

	private void UpdateAiming() {
		Ray aimingRay = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f));
		RaycastHit hit;

		if (Input.GetMouseButton(0)) {
			if (Time.realtimeSinceStartup >= lastShootTime + timeBetweenShoots)
				Shoot (aimingRay.direction);
		}
	}

	private void Shoot(Vector3 dir) {
		lastShootTime = Time.realtimeSinceStartup;

		GameObject obj = Instantiate (shootObject, shootPoint.position, Quaternion.identity) as GameObject;

		if (shooted < 3) {
			shooted++;
			shootedObjects [shooted-1] = obj;
		} else {
			Destroy (shootedObjects [0]);
			shootedObjects [0] = shootedObjects [1];
			shootedObjects [1] = shootedObjects [2];
			shootedObjects [2] = obj;
		}
			
		obj.GetComponent<Rigidbody> ().AddForce (dir * shootingForce);
	}
}
