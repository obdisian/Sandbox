using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RectMover
{
	[SerializeField, TooltipAttribute("移動速度")]
	protected float moveSpeed = 0.4f;

	[SerializeField, TooltipAttribute("ジャンプ力")]
	protected float jumpPower = 0.75f;


	//	テスト用復帰ポイント
	private Vector3 restartPoint = Vector3.zero;


	//	スプライト描画コンポーネント
	private SpriteRenderer spriteRenderer;


	//	残像作成
	private AfterimageController afterimage;


	//	初期化処理
	protected override void Start ()
	{
		base.Start ();

		restartPoint = transform.position;
		spriteRenderer = GetComponent<SpriteRenderer> ();

		afterimage = new AfterimageController (gameObject);
	}

	//	更新処理
	protected override void Move ()
	{
		afterimage.Move ();

		velocity.vertical -= transform.up * gravityScale;

		//	左右移動
		velocity.horizontal = Vector3.zero;
		if (Input.GetKey (KeyCode.D)) {
			velocity.horizontal += transform.right * moveSpeed;
			spriteRenderer.flipX = true;
		}
		if (Input.GetKey (KeyCode.A)) {
			velocity.horizontal += -transform.right * moveSpeed;
			spriteRenderer.flipX = false;
		}

		//	ジャンプ
		if (Input.GetKeyDown (KeyCode.Space) && isGround) {
			isGround = false;
			velocity.vertical = transform.up * jumpPower;
		}

		//	方向転換
		if (Input.GetKeyDown (KeyCode.Z)) {
			transform.rotation = Quaternion.AngleAxis (90, Vector3.forward) * transform.rotation;
		}


		if (Vector3.Distance (restartPoint, transform.position) > 50.0f) {
			transform.position = restartPoint;
			velocity.Clear ();
		}


		//	カメラ操作
		MoveCamera ();
	}


	//	カメラ操作（デバッグ用）
	private void MoveCamera ()
	{
		Vector3 pos = transform.position;
		pos.z = -10;
		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, pos, 0.1f);
		Camera.main.transform.rotation = Quaternion.Lerp (Camera.main.transform.rotation, transform.rotation, 0.1f);
	}
}
