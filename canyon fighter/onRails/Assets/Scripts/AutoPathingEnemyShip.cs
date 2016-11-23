using UnityEngine;
using System.Collections;

public class AutoPathingEnemyShip : MonoBehaviour {
	private GameObject playerShip;
	private GameObject thisShip;
	private bool inRange;
	private float lastShotTime = 0f;

	public GameObject shot;
	public GameObject explosion;
	public float fireRate;
	public float maxRange;
	public float minRange;
	public float rotationDamping;
	public float speed;
	public int damageThreshold = 2;

	private Vector3 waypoint;
	private Vector3[] waypoints;
	private int WPindexPointer;
	private bool reachedNextWaypoint;
	private int level;
	private int damage = 0;

	// Use this for initialization
	void Start () {
		Debug.Log ("Enemy::Start");
		playerShip = GameObject.Find ("PlayerShip");
		thisShip = this.gameObject;
		inRange = false;

		generateWaypoints();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (GameControllerCanyonGame.Instance.enableEnemyShots) {
			fireIfInRange ();
		}
		approachWaypoint ();

	}

	void fireIfInRange() {
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
		string levelDataString = GameData.Instance.getLevelData (level);
		string[] levelData = levelDataString.Split(',');
		float accuracy = float.Parse (levelData [2]);

		float rnd = Random.Range (0f, 1f);
		print ("Fire Rnd" + rnd + " accuracy" + accuracy);
		Vector3 offset;
		Vector3[] offsetArray = new Vector3[4];
		offsetArray [0] = new Vector3 (0, 9, 0);  	// second number controls height, miss above
		offsetArray [1] = new Vector3 (0, -4, 0);	// miss, in front
		offsetArray [2] = new Vector3 (0, 0, 10);	// miss to the left
		offsetArray [3] = new Vector3 (0, -0, -10);	// miss to the right

		if (false) {
		//if (rnd <= accuracy) {
			// will hit the ship
			offset = new Vector3(0, 0, 2);
		} else {
			// will miss the ship
			int rnd1 = Random.Range(0,4);
			//int rnd1 = 2;
			offset = offsetArray[rnd1];
			print ("rand " + rnd1);
		}

		Vector3 shipOffset = new Vector3 (-3, -3, 0);
		Vector3 target = new Vector3 (playerShip.transform.position.x, playerShip.transform.position.y-5, playerShip.transform.position.z);
		Instantiate (shot, thisShip.transform.position + shipOffset, Quaternion.LookRotation(target + offset - thisShip.transform.position));
	}

	void generateWaypoints() {
		Vector3 shipPosition = playerShip.transform.position;
		int direction = Random.Range (-1, 2);

		//print ("Direction " + direction);
		string waveDataString = GameData.Instance.getCurrentWaveData ();
		//Debug.Log ("WaveDataString " + waveDataString);

		string[] waveData = waveDataString.Split(':');
	
		if (waveData [1] == "R") {
			direction = -1;
		} else if (waveData [1] == "L") {
			direction = 1;
		}
		level = int.Parse (waveData [2]);

		//print ("Wave Data String " + waveDataString + " dir " + direction);

		waypoints = new Vector3[4];
		waypoints [0] = new Vector3 (shipPosition.x + 600, shipPosition.y + 200, shipPosition.z + direction*200);
		waypoints [1] = new Vector3 (shipPosition.x + 400, shipPosition.y + 50, shipPosition.z + Random.Range(-3,3)*10 );
		waypoints [2] = new Vector3 (shipPosition.x + 100, shipPosition.y + 30 + Random.Range(-5,5), shipPosition.z + Random.Range(-3,3)*10 );
		waypoints [3] = new Vector3 (shipPosition.x - 50, shipPosition.y + 200, shipPosition.z + direction*200);
		WPindexPointer = 1;

			transform.position = waypoints [0];

	}

	void approachWaypoint ()
	{                    
		waypoint = waypoints [WPindexPointer];
		var rotation = Quaternion.LookRotation(waypoint - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);

	
		transform.Translate (0,0,Time.deltaTime * speed);


		float dist = Vector3.Distance(waypoint, transform.position);

		//print ("dist " + dist);

		if (dist < 2 && reachedNextWaypoint == false) {
			//print("hit waypoint " + WPindexPointer);
			WPindexPointer++;
			reachedNextWaypoint = true;
			if (WPindexPointer > 3) {
				Destroy (transform.gameObject);
			}
		} else if (reachedNextWaypoint == true) {
			//print("exiting waypoint " + WPindexPointer);
			reachedNextWaypoint = false;
		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("EnemyShot"))
		{
			return;
		}
		print ("enemy collsion " + other.gameObject);
		Vector3 offset = transform.up * 1;

		Instantiate (explosion, this.transform.position+offset, this.transform.rotation);

		damage++;

		if (damage == damageThreshold) {
			Destroy (gameObject);
		}

		Destroy (other.gameObject);

	}
	


	


}
