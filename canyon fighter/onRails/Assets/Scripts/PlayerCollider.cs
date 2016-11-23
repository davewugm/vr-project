using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		//print ("!!!PLAYER Collider TRIGGERED " + other.gameObject);

		if (other.CompareTag ("EnemyShot")) {
			PlayerController.Instance.SendMessage("OnEnemyShotCollision");
			Destroy(other.gameObject);
		}

	}
}
