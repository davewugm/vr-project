using UnityEngine;
using System.Collections;

public class CannonController_MotherShip : MonoBehaviour {
	private GameObject playerShip;
	private float lastShotTime;
	public float fireRange = 250f;
	public float fireRate = 2.0f;
	public GameObject shot;
	public GameObject explosion;
	public int maxDamage = 2;

	private float rotationDamping = 2f;
	private int cannon = 0;
	private int damage = 0;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.Find ("PlayerShip");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameControllerCanyonGame.Instance.enableEnemyShots) {
			fireIfInRange ();
		}
	}

	void fireIfInRange() {
		float dist = Vector3.Distance(playerShip.transform.position, this.transform.position);
		
		lastShotTime += Time.deltaTime;

		if (dist < fireRange && lastShotTime > fireRate) {
			Vector3 target = new Vector3 (playerShip.transform.position.x+10, playerShip.transform.position.y, playerShip.transform.position.z);

			var rotation = Quaternion.LookRotation(target - transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
			transform.rotation = rotation;


			Instantiate (shot, this.transform.position, this.transform.rotation);
			lastShotTime = 0f;
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("EnemyShot"))
		{
			return;
		}
		print ("collsion " + other.gameObject);

		damage++;

		if (damage == maxDamage) {
			Vector3 offset = transform.up * 1;

			Instantiate (explosion, this.transform.position + offset, this.transform.rotation);
			Destroy (gameObject);

			Destroy (other.gameObject);
		}

	}

}
