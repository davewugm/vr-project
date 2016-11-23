using UnityEngine;
using System.Collections;

public class TargetCollider : MonoBehaviour {


	public int maxDamage;

	private int damage;


	// Use this for initialization
	void Start () {
		damage = 0;
	}



	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("TARGET HIT");
		damage++;
		SendMessageUpwards ("TargetHit", damage);
	}

	void TargetHit() {
	}
}
