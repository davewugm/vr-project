using UnityEngine;
using System.Collections;

public class EnemyShip : MonoBehaviour {
	private GameObject playerShip;
	private GameObject thisShip;
	private bool inRange;
	private float lastShotTime = 0f;

	public GameObject shot;
	public float fireRate = 1f;
	public float maxRange = 200;
	public float minRange = 400;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.Find ("PlayerShip");
		thisShip = this.gameObject;
		inRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance(playerShip.transform.position, thisShip.transform.position);

		Vector3 targetDir = playerShip.transform.position - thisShip.transform.position;
		Vector3 forward = transform.forward;
		float angle = Vector3.Angle(targetDir, forward);

		lastShotTime += Time.deltaTime;

		if (dist > minRange && dist < maxRange && angle < 90 && lastShotTime > fireRate) {
			Fire ();
			lastShotTime = 0f;
		}

	}

	void Fire() {
		Instantiate (shot, thisShip.transform.position, Quaternion.identity);
	}
}
