using UnityEngine;
using System.Collections;

// Rails Shooter GameController
public class GameController : MonoBehaviour {

	public float startWait;
	public float waveWait;
	public float spawnWait;
	public GameObject Enemy;
	public int hazardCount;

	private GameObject playerShip;

	// Use this for initialization
	void Start () {
		//StartCoroutine (SpawnWaves ());
		playerShip = GameObject.Find ("PlayerShip");
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
			//if (!hasSpawned)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject enemy = Enemy;
				Vector3 playerPosition = playerShip.transform.position;
				Vector3 spawnPosition = new Vector3 ( playerPosition.x - 10f, playerPosition.y-10f, playerPosition.z+10f );

				Instantiate (enemy, spawnPosition, Quaternion.identity);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);


		}
	}

	// Update is called once per frame
	void Update () {

	}
}

