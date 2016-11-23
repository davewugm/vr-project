using UnityEngine;
using System.Collections;

public class ShieldTextView : MonoBehaviour {
	private TextMesh textMesh;
	public static ShieldTextView Instance;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	public void UpdateShieldPower (int power) {
		textMesh = gameObject.GetComponent( typeof(TextMesh) ) as TextMesh;
		textMesh.text = "Shields " + power + "%";
	}
}
