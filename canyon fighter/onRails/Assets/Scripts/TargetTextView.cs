using UnityEngine;
using System.Collections;

public class TargetTextView : MonoBehaviour {
	private TextMesh textMesh;
	public static TargetTextView Instance;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	// Update is called once per frame
	public void UpdateTargetDistance (int distance) {
		textMesh = gameObject.GetComponent( typeof(TextMesh) ) as TextMesh;
		textMesh.text = "Target " + distance + "m";
	}
}
