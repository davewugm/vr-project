using UnityEngine;
using System.Collections;

public class TurretController : MonoBehaviour {
	private GameObject playerShip;
	private float lastShotTime;
	public float alertRange = 200f;
	public float fireRange = 70f;
	public float fireRate = 0.5f;
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
		if (GameControllerCanyonGame.Instance.enableEnemyTurretShots) {
			fireIfInRange ();
		}
	}

	void fireIfInRange() {
		float dist = Vector3.Distance(playerShip.transform.position, this.transform.position);
		
		//Vector3 targetDir = playerShip.transform.position - this.transform.position;
		//Vector3 forward = transform.forward;
		//float angle = Vector3.Angle(targetDir, forward);
		
		lastShotTime += Time.deltaTime;

		if (dist < alertRange) {
			//this.transform.LookAt (playerShip.transform);
			var rotation = Quaternion.LookRotation(playerShip.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
		}
		if (dist < fireRange && lastShotTime > fireRate) {
			Vector3 target = new Vector3 (playerShip.transform.position.x+10, playerShip.transform.position.y, playerShip.transform.position.z);

			var rotation = Quaternion.LookRotation(target - transform.position);
			//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
			transform.rotation = rotation;

			cannon++;
			cannon = cannon % 2;
			print ("Cannon " + cannon);
			Transform cannon_1 = this.transform.GetChild(cannon);


			Instantiate (shot, cannon_1.position, this.transform.rotation);
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
