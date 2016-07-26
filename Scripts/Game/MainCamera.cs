using UnityEngine;
using System.Collections;

public class MainCamera : Mover {

	GameObject player;
	Player playerComponent;

	public static Camera mCamera;

	float orthoSize;				//	orthographicSizeの補正前
	float collisionTopBlockY;		//	カメラとの当たり判定の上部分
	float collisionBottomBlockY;	//	カメラとの当たり判定の下部分

	bool isSetupPosition;			//	初期位置の作成

	BoxCollider2D boxCol;

	void Awake () {
		player = GameObject.Find ("Player");
		playerComponent = player.GetComponent<Player> ();

		mCamera = GetComponent<Camera> ();

		boxCol = GetComponent<BoxCollider2D> ();

		isSetupPosition = true;
	}

	void LateUpdate () {
		//	コンテ時の即死を防ぐ
		if (Stage.gameScene == Stage.GameScene.Score) {
			if (playerComponent.isFlag && !Stage.isGoal) {
				playerComponent.ZeroRigid2DVelocityY ();
				player.transform.position = playerComponent.flagPos;
			}
		}

		if (Stage.isTheRePlay) {
			return;
		}

		foreach (GameObject obj in Stage.billboardObject) {
			if (!obj.GetComponent<OnTriggerObject> ().isTrig) {
				obj.transform.rotation = transform.rotation;
			}
		}

		if (isSetupPosition) {
			if (count > 2) {
				isSetupPosition = false;
			} count++;

			orthoSize = Mathf.Abs (collisionTopBlockY - collisionBottomBlockY) / 2;
			mCamera.orthographicSize = Constrain (orthoSize, 3.5f, 5.5f);

			transform.SetPositionX (player.transform.position.x + mCamera.orthographicSize - 1);
			transform.SetPositionY (Constrain (player.transform.position.y, collisionBottomBlockY + mCamera.orthographicSize, collisionTopBlockY - mCamera.orthographicSize));
		}

		if (Stage.gameScene != Stage.GameScene.Score && player) {
			//	transform.SetPositionY (calcPosY + mCamera.orthographicSize);
			//	calcPosY = Mathf.Lerp (calcPosY, collisionBottomBlockY, 0.1f);

			//	ポーズ中のアクション（現状に不満。なのでまた後で直す）
			if (GameProgression.isPause) {
				orthoSize = Mathf.Lerp (mCamera.orthographicSize, Mathf.Abs (collisionTopBlockY - collisionBottomBlockY) / 2, 0.1f);
				mCamera.orthographicSize = Constrain (orthoSize, 3.5f, 5.5f);

				transform.SetPositionX (Mathf.Lerp (transform.position.x, player.transform.position.x + mCamera.orthographicSize - 1, 0.1f));
				transform.SetPositionY (Mathf.Lerp (transform.position.y, Constrain (player.transform.position.y,
					collisionBottomBlockY + mCamera.orthographicSize, collisionTopBlockY - mCamera.orthographicSize), 0.1f));

				transform.Lerp_Rotation (playerComponent.cRotation, 0.1f);

				//	回転の際にコライダーも回転してしまうため、回転時は、コライダーをオフにする処理。オイラー角で表記
				if (Mathf.Abs (transform.localEulerAngles.z - player.transform.localEulerAngles.z) > 10) {
					boxCol.enabled = false;
				} else {
					boxCol.enabled = true;
				}
			}
			//	============================================

			if (GameProgression.isPause) return;

			//	以下、カメラの位置、距離の計算
			orthoSize = Mathf.Lerp (mCamera.orthographicSize, Mathf.Abs (collisionTopBlockY - collisionBottomBlockY) / 2, 0.025f);
			mCamera.orthographicSize = Constrain (orthoSize, 3.5f, 5.5f);

			transform.SetPositionX (player.transform.position.x + mCamera.orthographicSize - 1);
			transform.SetPositionY (Mathf.Lerp (transform.position.y, Constrain (player.transform.position.y,
				collisionBottomBlockY + mCamera.orthographicSize, collisionTopBlockY - mCamera.orthographicSize), 0.1f));

			//	反転の回転はプレイヤー依存
			transform.Lerp_Rotation (playerComponent.cRotation, 0.1f);


			//	回転の際にコライダーも回転してしまうため、回転時は、コライダーをオフにする処理。オイラー角で表記
			if (Mathf.Abs (transform.localEulerAngles.z - player.transform.localEulerAngles.z) > 10) {
				boxCol.enabled = false;
			} else {
				boxCol.enabled = true;
			}

			//	カメラの右限
			transform.SetPositionX (Mathf.Min (StageMap.mapLengthX-5, transform.position.x));
		}
	}

	void OnTriggerEnter2D (Collider2D col) {
		collisionTopBlockY = col.gameObject.tag == "TopBlock" ? col.transform.position.y : collisionTopBlockY;
		collisionBottomBlockY = col.gameObject.tag == "BottomBlock" ? col.transform.position.y : collisionBottomBlockY;
	}
}

