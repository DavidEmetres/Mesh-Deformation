using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject spawnObject;

	void Start () {
		InvokeRepeating ("Spawn", 0f, 0.5f);
	}

	void Update () {
		
	}

	void Spawn() {
		Instantiate (spawnObject, this.transform.position, Quaternion.identity);
	}
}
