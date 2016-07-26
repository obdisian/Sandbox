using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	//	セーブ関係
	public enum SaveKey {
		MyRanking,
	}
	static string[] saveKey = {
		"Ranking",
	};
	public static string SaveData_Key (SaveKey sk) {
		return saveKey [(int)sk];
	}
	public static void SaveData_Save (SaveKey sk, int num) {
		PlayerPrefs.SetInt (saveKey [(int)sk], num);
	}
	//	初回時、0を代入
	public static int SaveData_Load (SaveKey sk) {
		return PlayerPrefs.GetInt (saveKey [(int)sk], 0);
	}



	public int hp = 1;
	public int count = 0, frame = 0;
	public Vector3 pos, prevPos, velocity, size;
	public Quaternion rotation;
	public GameObject owner = null;

	//	上、真ん中、下、左、右
	public enum UiBasePos { Top, Middle, Bottom, Left, Right }
	public static readonly Vector2 uiBasePanelScale = new Vector2 (800, 450);
	static readonly Vector2[] uiBasePos = {
		new Vector2 (Screen.width / 2, Screen.height / 2 * 3),
		new Vector2 (Screen.width / 2, Screen.height / 2),
		new Vector2 (Screen.width / 2, -Screen.height / 2),
		new Vector2 (-Screen.width / 2, Screen.height / 2),
		new Vector2 (Screen.width / 2 * 3, Screen.height / 2),
	};
	public static Vector2 UBPosition (UiBasePos ubp)
	{
		return uiBasePos [(int)ubp];
	}

	public static void FrameRate_Control (int frameRate) {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = frameRate;
		Time.captureFramerate = frameRate;
	}


	public float Radians (float angle) {
		return angle * Mathf.PI / 180.0f;
	}
	public float Length (Vector3 v) {
		return Mathf.Sqrt (v.x * v.x + v.y * v.y + v.z * v.z);
	}
	public static float RatioMap (float value, float start1, float end1, float start2, float end2) {
		return value / (start1 - end1) * (start2 - end2) + start2;
	}
	public static float Constrain (float value, float min, float max) {
		return Mathf.Max (min, Mathf.Min (value, max));
	}
	public static int Constrain (int value, int min, int max) {
		return Mathf.Max (min, Mathf.Min (value, max));
	}
	public Vector3 Normalize (Vector3 v) {
		float leng = Length(v);
		float num = 1 / leng;
		v *= num;
		return v;
	}
	public Vector3 Vertical (Vector3 origin, Vector3 v)
	{
		Vector3 unitV = Normalize(v);
		float vLength = Vector3.Dot(origin, unitV);
		unitV *= vLength;
		return origin - unitV;
	}

	public static Color HSV (float h, float s, float v) {
		float r = v, g = v, b = v;
		if (s > 0.0f) {
			h *= 6.0f;
			int i = (int)h;
			float f = h - (float)i;
			switch (i) {
			default:
			case 0: g *= 1 - s * (1 - f); b *= 1 - s; break;
			case 1: r *= 1 - s * f; b *= 1 - s; break;
			case 2: r *= 1 - s; b *= 1 - s * (1 - f); break;
			case 3: r *= 1 - s; g *= 1 - s * f; break;
			case 4: r *= 1 - s * (1 - f); g *= 1 - s; break;
			case 5: g *= 1 - s; b *= 1 - s * f; break;
			}
		}
		return new Color (r, g, b);
	}

	public Vector3 AxisX () { return transform.rotation * Vector3.right; }
	public Vector3 AxisY () { return transform.rotation * Vector3.up; }
	public Vector3 AxisZ () { return transform.rotation * Vector3.forward; }

	public Vector3 Direction () {
		Vector3 v = Vector3.zero;
		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) { v.y++; }
		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) { v.y--; }
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) { v.x--; }
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) { v.x++; }
		return v = v != Vector3.zero ? Normalize (v) : Vector3.zero;
	}
	public Vector2 BezierCurve2D (int count, int f, Vector2 s, Vector2 c1, Vector2 c2, Vector2 e) {
		float t = (float)count / (float)f;
		return (1 - t) * (1 - t) * (1 - t) * s + 3 * (1 - t) * (1 - t) * t * c1 + 3 * (1 - t) * t * t * c2 + t * t * t * e;
	}
	public Vector3 BezierCurve (int count, int f, Vector3 s, Vector3 c1, Vector3 c2, Vector3 e) {
		float t = (float)count / (float)f;
		return (1 - t) * (1 - t) * (1 - t) * s + 3 * (1 - t) * (1 - t) * t * c1 + 3 * (1 - t) * t * t * c2 + t * t * t * e;
	}


	public Quaternion RotationX (float angle) {
		return Quaternion.AngleAxis (angle * 360, Vector3.right);
	}
	public Quaternion RotationY (float angle) {
		return Quaternion.AngleAxis (angle * 360, Vector3.up);
	}
	public Quaternion RotationZ (float angle) {
		return Quaternion.AngleAxis (angle * 360, Vector3.forward);
	}
	public Quaternion RotationXYZ (float angle) {
		return RotationX(angle) * RotationY(angle) * RotationZ(angle);
	}

	public void IsDelete () {
		Destroy (gameObject);
	}
}
