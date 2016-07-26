using UnityEngine;
using System.Collections;

public class SaveData : MonoBehaviour {

	public static int[] firstClearCount = new int[15];


	public static void SaveFirstClearCount (int n) {
		firstClearCount [n]++;

		for (int i = 0; i < 15; i++) {
			PlayerPrefs.SetInt ("FirstClearCount_" + i, firstClearCount [i]);
		}
	}

	void Awake () {
		for (int i = 0; i < 15; i++) {
			firstClearCount [i] = PlayerPrefs.GetInt ("FirstClearCount_" + i, 0);
		}
	}
}
