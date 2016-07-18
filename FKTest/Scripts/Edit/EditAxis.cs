using UnityEngine;
using System.Collections;

public class EditAxis : MonoBehaviour {

	public GameObject AxisX, AxisY, AxisZ;

	public enum Type {
		Position,
		Rotation,
	}
	public Type type;


	public float AxisAngleX { get; set; }
	public float AxisAngleY { get; set; }
	public float AxisAngleZ { get; set; }



	//================================================================================
	//	初期化
	//================================================================================
	void Start () {

		if (type == Type.Position) {
		}
		else if (type == Type.Rotation) {
			
			for (float i = 0; i < 360; i += 360 * 0.05f) {

				float dist = 2.5f;
				GameObject axx = Instantiate (AxisX,
					Quaternion.AngleAxis (i, Vector3.right) * Vector3.forward * dist,
					Quaternion.AngleAxis (i, Vector3.right) * Quaternion.AngleAxis (90, Vector3.forward)) as GameObject;
				GameObject axy = Instantiate (AxisY,
					Quaternion.AngleAxis (i, Vector3.up) * Vector3.right * dist,
					Quaternion.AngleAxis (i, Vector3.up) * Quaternion.AngleAxis (90, Vector3.right)) as GameObject;
				GameObject axz = Instantiate (AxisZ,
					Quaternion.AngleAxis (i, Vector3.forward) * Vector3.up * dist,
					Quaternion.AngleAxis (i, Vector3.forward) * Quaternion.AngleAxis (90, Vector3.up)) as GameObject;

				float height = 0.8f;
				float radius = 0.05f;
				axx.transform.localScale = new Vector3 (height, radius, radius);
				axy.transform.localScale = new Vector3 (radius, height, radius);
				axz.transform.localScale = new Vector3 (radius, radius, height);

				axx.transform.parent = transform;
				axy.transform.parent = transform;
				axz.transform.parent = transform;

				axx.name = "X";
				axy.name = "Y";
				axz.name = "Z";
			}
		}
	}


	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void Update () {

		GameObject hitObj = InputController.MouseHitObject;

		if (!hitObj) {
			return;
		}


		if (hitObj.name == "X") {
			AxisAngleX += Vector3.Distance (Vector3.zero, InputController.MoveVector);
			transform.rotation = Quaternion.AngleAxis (AxisAngleX, transform.right);
		}
		else if (hitObj.name == "Y") {
			AxisAngleY += Vector3.Distance (Vector3.zero, InputController.MoveVector);
			transform.rotation = Quaternion.AngleAxis (AxisAngleY, transform.up);
		}
		else if (hitObj.name == "Z") {
			AxisAngleZ += Vector3.Distance (Vector3.zero, InputController.MoveVector);
			transform.rotation = Quaternion.AngleAxis (AxisAngleZ, transform.forward);
		}
	}
}
