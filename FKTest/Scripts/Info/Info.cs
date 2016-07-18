using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

	public static void FPS (int frameRate)
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = frameRate;
		Time.captureFramerate = frameRate;
	}


	public static float RatioMap(float value, float start1, float end1, float start2, float end2)
	{
		return value / (start1 - end1) * (start2 - end2) + start2;
	}


	public static void MotionDataLog (MotionData m) {
		Debug.Log ("IsLoop = " + m.IsLoop);
		Debug.Log ("MaxFrame = " + m.MaxFrame);

		for (int i = 0; i < m.KeyFrame.Length; i++) {
			Debug.Log ("KeyFrame " + i + " = " + m.KeyFrame [i]);
		}

		for (int i = 0; i < m.SlerpT.Length; i++) {
			Debug.Log ("SlerpT " + i + " = " + m.SlerpT [i]);
		}

		for (int i = 0; i < m.Pos.Length; i++) {
			Debug.Log ("Pos " + i + " = " + m.Pos [i]);
		}

		for (int i = 0; i < m.Angles.Length; i++) {
			for (int j = 0; j < m.Angles [i].Length; j++) {
				Debug.Log ("Angles" + i + " " + j + " = " + m.Angles [i][j]);
			}
		}
	}
}
