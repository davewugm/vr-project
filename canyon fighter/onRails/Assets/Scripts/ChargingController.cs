using UnityEngine;
using System.Collections;

public class ChargingController : MonoBehaviour {
	public GameObject EffectObject;
	public GameObject Explosion;
	public int targetId;

	// Use this for initializationr
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TargetHit(int damage) {
		Debug.Log ("CONTROLLER::TARGET HIT " + damage);
		if (damage == 3) {
			EffectObject.SetActive (false);
		}
		if (damage == 6) {
			Instantiate (Explosion, this.transform.position, Quaternion.identity);
			this.gameObject.SetActive (false);
			MothershipController2.Instance.RegisterDestroyed (targetId);
		}

	}
}
