using UnityEngine;
using System.Collections;

public class CraneController : MonoBehaviour {
	
	public GameObject Explosion;
	public Animator craneAnimator;
	public int targetId = 2;

	// Use this for initialization
	void Start () {
		craneAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void TargetHit(int damage) {
		Debug.Log ("CONTROLLER::TARGET HIT " + damage);

		if (damage == 6) {
			Instantiate (Explosion, this.transform.position, Quaternion.identity);
			craneAnimator.SetTrigger ("CraneDestroyed");
			MothershipController2.Instance.RegisterDestroyed (targetId);
		}

	}
}
