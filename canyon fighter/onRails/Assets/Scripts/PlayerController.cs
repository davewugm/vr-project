using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public GameObject shot;
	public GameObject[] sectionMarkers;
	public float rotationDamping;
	public float speed;
	public float speed2;
	public GameObject explosion;
	public int lastSection;
	public int damage = 2;

	private GameObject vrLauncher;

	private Vector3 waypoint;
	private Vector3[] waypoints;
	private int WPindexPointer;
	private int shields;
	private int numSections;
	private bool reachedNextWaypoint;
	private string state;
	private int startIndex = 0;

	public static PlayerController Instance;

	// Use this for initialization
	void Awake() {
		Instance = this;
		shields = 100;

		numSections = sectionMarkers.Length;
		waypoints = new Vector3[numSections];
		for (int i = 0; i < numSections; i++) {
			waypoints [i] = sectionMarkers [i].transform.position;
			Debug.Log ("WAYPOINTS : " + waypoints [i]);
		}
		WPindexPointer = 0;

	}

	void initGame() {
		if (startIndex == 0) {
			transform.position = new Vector3 (25, 5, 25);
		} else {
			transform.position = new Vector3 (waypoints [startIndex].x+5, 5, waypoints [startIndex].z);
			WPindexPointer = startIndex + 1;
		}
		state = "phase1";
	}

	void Start () {
		startIndex = GameControllerCanyonGame.Instance.StartIndex;
		initGame ();

		vrLauncher = 
			Cardboard.SDK.GetComponentInChildren<CardboardHead> ().gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if (state != "gameover") {
			if (state == "phase1") {
				approachWaypoint ();
			}
			if (state == "phase2") {
				//decelerate (0.9f);
				approachWaypoint ();
			}

			if (Cardboard.SDK.Triggered == true) {
				Fire ();
			}

			HudViewController.Instance.UpdateTargetDistance (5500 - Mathf.FloorToInt (vrLauncher.transform.position.x / 10) * 10);
		}
	}

	void decelerate(float rate) {
		speed = Mathf.Clamp (speed * rate, 0, 100);
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
			print("PLAYER: hit waypoint " + WPindexPointer);
			WPindexPointer++;
			reachedNextWaypoint = true;
			powerUp ();
			if (WPindexPointer == lastSection) {
				// end
				Debug.Log("LAST SECTION");
				state = "phase2";
				WPindexPointer = 5; //loop
				reachedNextWaypoint = false;
			}
			if (WPindexPointer == 6) {
				state = "phase2";
				speed = speed2;
			}
		} else if (reachedNextWaypoint == true) {
			//print("exiting waypoint " + WPindexPointer);
			reachedNextWaypoint = false;
		}

	}

	void Fire() {
		Instantiate (shot, vrLauncher.transform.position, Quaternion.LookRotation(vrLauncher.transform.forward));
	}
		
	void powerUp() {
		shields = Mathf.Clamp (shields + 10, 0, 100);
		HudViewController.Instance.UpdateShieldPower( shields);
	}

	void OnEnemyShotCollision() {

		if (shields == 0) {
			GameControllerCanyonGame.Instance.EndGame ();
			state = "gameover";
		} else {
			shields = shields - damage;
			Vector3 offset = new Vector3 (2, 0, 0);
			Instantiate (explosion, this.transform.position+offset, this.transform.rotation);
		}

		HudViewController.Instance.UpdateShieldPower( shields);

	}


}
