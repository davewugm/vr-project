using UnityEngine;
using System.Collections;

public class GameData {

	private string[] sectionData;
	private string[] levelData;

	private int sectionIndex;
	private int waveIndex;
	private string[] waveData;

	private static GameData instance;

	private GameData() {
		// sectionData in the format of spawnWaveWaitTime : direction (L, R, Rnd) : level
		sectionData = new string[10];
		sectionData [0] = "6:R:0, 8:R:0, 7:R:0";
		sectionData [1] = "0:L:0, 5:R:1, 3:L:1, 5:R:2";
		sectionData [2] = "0:L:1, 3:R:2, 2:L:2, 2:L:2, 3:R:3";
		sectionData [3] = "1:R:2, 2:R:2, 2:L:4, 2:R:2, 1:L:4, 2:L:4, 2:R:4";
		sectionData [4] = "0:L:4, 2:R:5, 0:R:4, 0:L:5, 1:L:6, 0:R:5, 0:L:6";
		sectionData [5] = "4:L2:4,1:L:4"; //"6:L2:4, 10:R2:4, 12:L2:4, 11:L2:4, 12:R2:4";
		sectionData [6] = "1:L:4";
		sectionData [7] = "1:L2:4,1:L:5";
		sectionData [8] = "1:R2:4";
		sectionData [9] = "1:L2:4";
		//sectionData [5] = "2:L:5, 1:R:5, 1:L:6, 0:R:5";
		//sectionData [6] = "2:L:6, 2:R:6, 2:L:6";


		// levelData in the format of num ships, min attk dist:max attk distance, attk accuracy, spawnWait
		levelData = new string[8];
		levelData [0] = "2,150:200,0.25,3";
		levelData [1] = "3,150:200,0.3,3";
		levelData [2] = "2,150:200,0.4,2";
		levelData [3] = "3,150:200,0.5,3";
		levelData [4] = "2,150:200,0.7,2";
		levelData [5] = "3,150:200,0.7,3";
		levelData [6] = "2,150:200,0.8,2";
		levelData [7] = "3,150:200,0.8,2";
	}

	public static GameData Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new GameData();
			}
			return instance;
		}
	}

	public void resetLevels(int index = 0) {

		sectionIndex = index;
		nextWaveData ();

		Debug.Log ("RESET LEVELS" + sectionIndex);
	}

	void nextWaveData() {
		waveData = new string[5];
		waveData = sectionData[sectionIndex].Split(',');
		waveIndex = 0;
	}
	
	public string getCurrentWaveData() {
		string data = "";

		if (waveIndex < waveData.Length) {
			data = waveData[waveIndex];
		}
		//Debug.Log ("!!GetCurrentWaveData " + data);
		return data;
	}

	public string getLevelData(int level) {
		return levelData [level];
	}

	public void getNextWaveData() {
		Debug.Log ("--->GetNextWavedata " + waveIndex);
		if (waveIndex <= waveData.Length) {
			waveIndex++;
		}
	}

	public bool waveIsComplete {
		get 
		{
			return waveIndex >= waveData.Length;
		}
	}

	public void getNextSection() {
		if (sectionIndex < sectionData.Length - 1) {
			sectionIndex++;
		}
		Debug.Log ("---->Get Next Section " + sectionIndex);
		nextWaveData ();

	}

	public int getSectionIndex() {
		return sectionIndex;
	}
}
