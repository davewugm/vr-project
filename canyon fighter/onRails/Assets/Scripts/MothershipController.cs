using UnityEngine;
using System.Collections;

public class MothershipController : MonoBehaviour {

	public GameObject target1;
	public GameObject target2;
	public GameObject target3;
	public float spawnWait;
	public float rotationTime;
	public float range;

	private float timer;
	private GameObject[] targets;
	private int currentTarget;
	private int patternIndex = 0;

	private int[] pattern;
	private bool started = false;
	private GameObject playerShip;

	void Awake() {
	}


	// Use this for initialization
	void Start () {
		targets = new GameObject[3];
		targets [0] = target1;
		targets [1] = target2;
		targets [2] = target3;
		currentTarget = 0;

		target1.SetActive(false);
		target2.SetActive (false);
		target3.SetActive (false);

		pattern = new int[] {0, 1, 0, 2, 1, 0, 0, 2, 0, 2, 1, 1, 0, 2, 0};

		timer = 0;
		started = false;

		playerShip = GameObject.Find ("PlayerShip");
	}
	
	// Update is called once per frame
	void Update () {
		if (InRange() && started == false) {
			Debug.Log ("START COROUTINE");
			StartCoroutine (RotateTargets ());
			started = true;
		}

	}

	bool IsDestroyed() {
		return false;
	}
	
	bool InRange() {
		float dist = Vector3.Distance(playerShip.transform.position, this.transform.position);

		return dist < range;
	}

	IEnumerator RotateTargets ()
	{

		Debug.Log ("ROTATE TARGETS");
		while (IsDestroyed() == false)
		{
			Debug.Log ("ROTATE TARGETS WHILE");
			yield return new WaitForSeconds (rotationTime);
			RotateTarget ();
			
			yield return new WaitForSeconds (spawnWait);
			CloseTarget ();

		}

		StopCoroutine(RotateTargets());
	}

	void RotateTarget() {
		targets [currentTarget].SetActive (false);
		patternIndex++;
		patternIndex = patternIndex % pattern.Length;
		currentTarget = pattern [patternIndex];


		targets [currentTarget].SetActive(true);

	}

	void CloseTarget() {
		targets [currentTarget].SetActive (false);
	}
}
