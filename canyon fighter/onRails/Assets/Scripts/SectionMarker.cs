using UnityEngine;
using System.Collections;

public class SectionMarker : MonoBehaviour {
	public int section;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		print ("SECTION MARKER TRIGGER " + other);
		if (other.CompareTag("Player"))
		{
			GameControllerCanyonGame.Instance.SendMessage("OnSectionEnter", section);
		}
	}
}
