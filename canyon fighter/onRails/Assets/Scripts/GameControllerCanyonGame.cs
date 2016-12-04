using UnityEngine;
using System.Collections;

// Rails Shooter GameController
public class GameControllerCanyonGame : MonoBehaviour {

	private float startWait;
	private float waveWait;
	private float spawnWait;
	public GameObject Enemy;
	private int hazardCount;

	private GameObject playerShip;

	public static GameControllerCanyonGame Instance;
	public int StartIndex = 0;
	public bool enableEnemyShots = true;
	public bool enableEnemyTurretShots = true;

	void Awake() {
		Instance = this;
	}

	// Use this for initialization
	void Start () {
		print ("GAME Controller Start");
		// init game dataxf
		GameData.Instance.resetLevels(StartIndex);
		playerShip = GameObject.Find ("PlayerShip");

		startSpawn ();
	}

	public void EndGame() {
		HudViewController.Instance.ShowFailed (true);
	}

	void startSpawn()
	{
		//Debug.Log ("START SPAWN");
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{

		//yield return new WaitForSeconds (startWait);
		string waveDataString;
		string levelDataString;
		string[] waveData = new string[3];
		string[] levelData = new string[5];
		int level;

		while (GameData.Instance.waveIsComplete == false)
		{
			waveDataString = GameData.Instance.getCurrentWaveData();
			waveData = waveDataString.Split(':');
			waveWait = float.Parse(waveData[0]);
			level = int.Parse(waveData[2]);

			levelDataString = GameData.Instance.getLevelData(level);
			levelData = levelDataString.Split(',');

			hazardCount = int.Parse (levelData[0]);
			spawnWait = float.Parse (levelData[3]);


			yield return new WaitForSeconds (waveWait);
			//Debug.Log ("SPAWN WAIT, WAVE WAIT " + spawnWait + " " + waveWait);
			//Debug.Log ("Hazard Count " + hazardCount);
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject enemy = Enemy;
				Vector3 playerPosition = playerShip.transform.position;
				Vector3 spawnPosition = new Vector3 ( playerPosition.x - 10f, playerPosition.y-10f, playerPosition.z+10f );

				Debug.Log ("Spawn New Enemy");
				Instantiate (enemy, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}
			//print ("Get Next Wave Data");
			GameData.Instance.getNextWaveData();

		}
		//print ("WAVE IS COMPLETE");
		StopCoroutine(SpawnWaves());
	}

	// Update is called once per frame
	void Update () {

	}

	void OnSectionEnter(int section) {
		GameData.Instance.getNextSection ();
		print ("NEXT SECTION " + GameData.Instance.getSectionIndex().ToString());
		// check if more data

		if (GameData.Instance.getCurrentWaveData () != "") {
			Debug.Log ("NEXT SECTION Wave Data: " + GameData.Instance.getCurrentWaveData ());
			startSpawn ();
		}
	}
}

