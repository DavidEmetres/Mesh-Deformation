using UnityEngine;
using System.Collections;

public class ObjectBehaviour : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Finish")
			Destroy (this.gameObject);
	}
}
