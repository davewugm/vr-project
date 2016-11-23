using UnityEngine;
using System.Collections;

public class HudViewController : MonoBehaviour {
	private TextMesh targetText;
	private TextMesh shieldText;
	private GameObject gameControlObject;
	private GameObject failedTextObject;

	private TextMesh textMesh;

	public static HudViewController Instance;

	// Use this for initialization
	void Awake () {
		Instance = this;
		targetText = GameObject.FindGameObjectWithTag("TargetText").GetComponent< TextMesh> ();
		shieldText = GameObject.FindGameObjectWithTag("ShieldText").GetComponent< TextMesh> ();
		gameControlObject = GameObject.FindGameObjectWithTag ("GameControlText");
		failedTextObject = GameObject.FindGameObjectWithTag ("GameControlTextFailed");

		gameControlObject.SetActive (false);
		failedTextObject.SetActive (false);

	}

	public void UpdateTargetDistance (int distance) {
		targetText.text = "Target " + distance + "m";
	}


	public void UpdateShieldPower (int power) {
		shieldText.text = "Shields " + power + "%";
	}

	public void ShowFailed(bool isVisible) {
		gameControlObject.SetActive (isVisible);
		failedTextObject.SetActive (isVisible);
	}

}
