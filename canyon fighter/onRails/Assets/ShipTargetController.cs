using UnityEngine;
using System.Collections;

public class ShipTargetController : MonoBehaviour {

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

		if (damage == 1) {
			Instantiate (Explosion, this.transform.position, Quaternion.identity);
			MothershipController2.Instance.RegisterDestroyed (targetId);
		}

	}
}
