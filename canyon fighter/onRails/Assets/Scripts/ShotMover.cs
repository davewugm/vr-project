using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {

	public float speed;
	public float duration;

	private float timer;
	private Rigidbody rb;

	void Start()
	{

		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;

		Destroy(transform.gameObject, duration);
	
	}

	void OnTriggerEnter(Collider other) {
		//print ("!!ShotMover OnTriggerEnter " + other);

		if (other.CompareTag ("Terrain")) {
			Destroy(gameObject);
		}

	}



}
