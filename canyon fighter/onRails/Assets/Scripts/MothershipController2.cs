using UnityEngine;
using System.Collections;

public class MothershipController2 : MonoBehaviour {

	public static MothershipController2 Instance;
	private bool[] destroyed;
	private Animator animator;

	void Awake() {
		Instance = this;
	}


	// Use this for initialization
	void Start () {
		destroyed = new bool[3];
		animator = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RegisterDestroyed(int target) {
		destroyed[target] = true;

		int total = 0;
		for (int i = 0; i < destroyed.Length; i++) {
			if (destroyed [i]) {
				total++;
			}
		}

		if (total == 3) {
			DestroyMotherShip ();
		}
	}

	void DestroyMotherShip() {
		animator.SetTrigger ("MothershipDestroyed");
	}
	


}
