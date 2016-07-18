using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {


	//	押してるかどうか
	public static bool IsHoldDown { get; set; }

	//	スワイプの移動量
	public static Vector3 SwipeVector { get; set; }

	//	前フレームとの差分
	public static Vector3 MoveVector { get; set; }

	//	タッチしたオブジェクトを取得(離すまで)
	public static GameObject MouseHitObject { get; set; }

	//	タッチしたオブジェクトとの位置を取得
	public static Vector3 MouseHitPoint { get; set; }



	//	過去の位置
	Vector3 prevPos;

	//	押した瞬間の位置
	Vector3 downPos;




	void Update () {

		Vector3 mousePos = Input.mousePosition;

		if (Input.GetMouseButtonDown (0)) {
			IsHoldDown = true;

			downPos = prevPos = mousePos;

			Ray ray = Camera.main.ScreenPointToRay (mousePos);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				MouseHitObject = hit.collider.gameObject;
				MouseHitPoint = hit.point;
			}
		}
		else if (Input.GetMouseButtonUp (0)) {
			IsHoldDown = false;
			MouseHitObject = null;
		}

		if (IsHoldDown) {
			if (mousePos != prevPos) {
				MoveVector = mousePos - prevPos;
				prevPos = mousePos;
			}
			else {
				MoveVector = Vector3.zero;
			}
			SwipeVector = mousePos - downPos + Vector3.up * 0.001f;
		}
		else {
			prevPos = SwipeVector = MoveVector = Vector3.zero;
		}
	}
}
