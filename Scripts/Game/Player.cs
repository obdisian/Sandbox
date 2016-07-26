using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public enum State {
		Walk, Jump, Roll,
	}
	public static State state;

	bool isTrigger;
	bool isCollision;
	GameObject triggerOther;
	GameObject collisionOther;

	bool isGround;		//	接地フラグ
	public bool isDead;		//	死亡フラグ

	public bool isTimeOfDeath;	//	死亡した瞬間のフラグ

	//	FixedUpdate用フラグ
	bool isFixedJumpAction;	//	ジャンプの入力のフラグ
	bool isFixedJumpCancel;	//	ジャンプキャンセルの入力のフラグ

	bool isSky;				//	空中にいるかどうかの確認フラグ
	bool isSpin;
	bool isForceJump;
	bool isForceJumpNow;	//	強制ジャンプ中はtrueでいるフラグ

	int skyCounter = 0;
	bool isSkyCheck = false;
	bool isTrueSky = false;

	int intReverse = 1;		//	反転用係数
	int flagReverse = 1;	//	復帰時の反転係数

	public int lifePoint = 1;		//	残機

	int speedAssist = 2;	//	スピード用の係数
	int flagSpeedAssist = 2;//	復帰時のスピード係数
	float[] speed = { 4.0f, 5.5f, 7.0f, 8.5f, 10.0f, 11.5f };
	const float jumpPower = 22.0f/1.5f;
	GameProgression.ColorFlag flagColorFlag = GameProgression.ColorFlag.Red;	//	復帰時のカラーの記憶

	public Vector2 flagPos;
	readonly Vector2 gravity = Vector2.down * 0.75f;

	public Quaternion cRotation;

	//	動画広告用フラグ
	public bool isFlag;

	Rigidbody2D rigid;

	List<GameObject> colObject = new List<GameObject> ();

//	public GameObject deadEffect;

	public enum SkinType {
		Skin_0, Skin_1, Skin_2, Skin_3,
	}
	public static SkinType skinType;

	//	SE
	#if UNITY_EDITOR || UNITY_IOS
	#elif UNITY_ANDROID
	int[] soundKeyNumber = new int [10];
	#endif

	void SeShot (Sound.SeName keyName) {
		#if UNITY_EDITOR || UNITY_IOS
		Sound.PlaySe (Sound.seKeyName [(int)keyName]);
		#elif UNITY_ANDROID
		AndroidNativeAudio.play(soundKeyNumber [(int)keyName]);
		#endif
	}

	void Start () {

		state = State.Walk;

		rigid = GetComponent<Rigidbody2D> ();

		//	初期位置を復帰位置にする
		flagPos = transform.position;
		isGround = true;

		//	Android用に別途SE登録
		AndroidNativeAudio.makePool(1);
		#if UNITY_EDITOR || UNITY_IOS
		#elif UNITY_ANDROID
		for (int i = 0; i < 10; i++) {
			soundKeyNumber [i] = AndroidNativeAudio.load("Android Native Audio/SND_SE0" + i + ".wav");
		}
		#endif
	}
	
	void LateUpdate () {

		SkinOption ();

		TimeOfDeath ();
		GraphicRotation ();
		DeathDecision_PositionY ();
		AfterDeath ();
		InputJumpTrigger ();
		JumpNaturalCancel ();

		BlockLeftCollision ();

		OnColliderObject_Update ();


		if (isTrueSky && isSpin) {
			state = State.Roll;
		} else if (!isGround && isSky || isForceJumpNow) {
			state = State.Jump;
		} else {
			state = State.Walk;
		}

		if (isSkyCheck) {
			skyCounter++;
			if (skyCounter > 5) {
				isTrueSky = true;
			}
		} else {
			skyCounter = 0;
			isTrueSky = false;
		}
	}

	void FixedUpdate () {

		Gravity ();
		HorizontalMove ();
		JumpAction ();
		JumpCancel ();
		ForceJumpAction ();
	}

	void OnTriggerEnter2D (Collider2D col) {

		OnGoal (col.gameObject);
		OnReverse (col.gameObject);
		OnNeedle (col.gameObject);
		OnStop (col.gameObject);
		OnSpeedUp (col.gameObject);
		OnSpeedDown (col.gameObject);
		OnFlag (col.gameObject);
		OnBarBlock (col.gameObject);
		OnReJump (col.gameObject);
		OnForceJump (col.gameObject);
		OnColorChangeFlag (col.gameObject);
		OnColorBlock (col.gameObject);

		OnBlockLeft (col.gameObject);
	}

	void OnCollisionEnter2D (Collision2D col) {
		GroundEnter (col.gameObject, "Block");
		GroundEnter (col.gameObject, "TopBlock");
		GroundEnter (col.gameObject, "BottomBlock");
		GroundEnter (col.gameObject, "ColorBlock");
	}
	void OnCollisionStay2D (Collision2D col) {

		GroundCheck (col.gameObject, "Block");
		GroundCheck (col.gameObject, "TopBlock");
		GroundCheck (col.gameObject, "BottomBlock");
		GroundCheck (col.gameObject, "ColorBlock");
	}
	void OnCollisionExit2D (Collision2D col) {
		SkyCheck (col.gameObject, "Block");
		SkyCheck (col.gameObject, "TopBlock");
		SkyCheck (col.gameObject, "BottomBlock");
		SkyCheck (col.gameObject, "ColorBlock");
	}


	//	=================忘れそうだからコメントを無駄でもいいから残しとこう宣言=================


	//	MainCameraから呼び出す用(コンテ時、まれに空中にいることがあるため)
	public void ZeroRigid2DVelocityY () {
		rigid.SetRigid2DVelocityY (0);
	}

	//	=================以下Update用=================

	//	死亡時に呼ぶ
	void TimeOfDeath () {
		if (isTimeOfDeath) {
			lifePoint--;
			//	後半追加
			if (lifePoint <= 0)
			{ isDead = true; Stage.gameScene = Stage.GameScene.Score; Stage.RunPercent (); return; }
			transform.position = flagPos; 	//	位置の移動
			intReverse = flagReverse;		//	反転係数の入れ替え
			speedAssist = flagSpeedAssist;	//	スピード係数の入れ替え
			GameProgression.colorFlag = flagColorFlag;	//	色の入れ替え
			isTimeOfDeath = false;
			GameProgression.isPause = true;

			//	後から追加、ジャンプがキャンセルされてないと復帰後即ジャンプに行こうしているため
			rigid.SetRigid2DVelocityY (0);
			isGround = true;
			isSky = false;
			isFixedJumpAction = false;
			isFixedJumpCancel = false;

//			OnColliderObject_Setup ();
		}
	}

	//	y座標での死亡判定
	void DeathDecision_PositionY () {
		if (transform.position.y < StageMap.mapBottomY || transform.position.y > StageMap.mapTopY) {
			isTimeOfDeath = true;
		}
	}

	//	死後の処理
	void AfterDeath () {
		if (isDead) {
			//	クラッシュエフェクト
//			Instantiate (deadEffect, transform.position, cRotation);
			GameProgression.CreateCrushEffect (transform.position);
			SeShot (Sound.SeName.Crush);

			transform.position = flagPos; 	//	位置の移動
			intReverse = flagReverse;		//	反転係数の入れ替え
			speedAssist = flagSpeedAssist;	//	スピード係数の入れ替え
			GameProgression.colorFlag = flagColorFlag;	//	色の入れ替え

			//	グラフィック上の回転処理
			transform.SetLocalScaleX (transform.localScale.y * intReverse);
			cRotation = Quaternion.AngleAxis (intReverse > 0 ? 0 : 180, Vector3.forward);
			transform.rotation = cRotation;

			Stage.gameScene = Stage.GameScene.Score;
			GameProgression.isPause = true;

			//	後から追加、ジャンプがキャンセルされてないと復帰後即ジャンプに行こうしているため
			rigid.SetRigid2DVelocityY (0);
			isGround = true;
			isSky = false;
			isFixedJumpAction = false;
			isFixedJumpCancel = false;

			//	AdsButtonで書いている
//			OnColliderObject_Setup ();
		}
	}

	//	ジャンプの入力用
	void InputJumpTrigger () {
		if (Input.GetMouseButtonDown (0) && isGround) { isFixedJumpAction = true; if (isTrueSky) { isSpin = true;} }
		else if (Input.GetMouseButtonUp (0)) { isFixedJumpCancel = true;/* isGround = false;*/ }

		//	2016/1/6
		//	データ上は当たってないが、当たってると思って反応しないと思う人多数、なのでこういう修正入れる
		//	とか思ったが一時停止時からの復帰が辛いのでとりあえず消す
//		if (Input.GetMouseButton (0) && isGround) {
//			isFixedJumpAction = true;
//			if (isSky) {
//				isSpin = true;
//			}
//		}

		if (isForceJumpNow) {
			isFixedJumpCancel = false;
		}
	}

	//	ジャンプキャンセルフラグの自然キャンセル
	void JumpNaturalCancel () {
		if (isFixedJumpCancel && intReverse > 0 && rigid.velocity.y < 0 ||
		    isFixedJumpCancel && intReverse < 0 && rigid.velocity.y > 0) {
			isFixedJumpCancel = false;
//			isGround = false;
		}
	}

	//	グラフィック上の回転処理
	void GraphicRotation () {
		//	textureは正方形かつtransform.localScale.yは不変のもののため、このような書き方にした
		transform.SetLocalScaleX (transform.localScale.y * intReverse);
		cRotation = Quaternion.AngleAxis (intReverse > 0 ? 0 : 180, Vector3.forward);

		//	スピンの判定を優先することによって上部ブロックとの当たり判定を無効にする
		if (isTrueSky && isSpin) {
			transform.rotation = Quaternion.AngleAxis (20 * -intReverse, Vector3.forward) * transform.rotation;
		} else if (isTrueSky || isGround) {
			transform.rotation = cRotation;
		}
	}

	void BlockLeftCollision () {
		if (!isCollision && isTrigger) {
			if (collisionOther && triggerOther && collisionOther != triggerOther) {
				isTimeOfDeath = true;
			}
		}

		if (isTrigger) {
			if (Mathf.Abs (triggerOther.transform.position.x - transform.position.x) >
				Mathf.Abs (triggerOther.transform.position.y - transform.position.y)) {
				isTimeOfDeath = true;
			}
		}
		isCollision = false;
		isTrigger = false;
	}

	//	スキンオプション
	void SkinOption () {
//		if (skinType == SkinType.Skin_3) {
//			Afterimage.CreateAfterimage (gameObject, 10);
//		}
	}

	//	=================以下FixedUpdate用=================

	//	横移動
	void HorizontalMove () {
		rigid.SetRigid2DVelocityX (speed[speedAssist] * Time.deltaTime * 60);
	}

	//	クリックしたらジャンプする
	void JumpAction () {
		if (isFixedJumpAction) {
			rigid.SetRigid2DVelocityY (jumpPower * Time.deltaTime * 60 * intReverse);
			isFixedJumpAction = false;
			isGround = false;
			isSky = true;

			SeShot (Sound.SeName.Jump);
		}
	}

	//	タップリリースしたら降下する
	void JumpCancel () {
		if (isFixedJumpCancel && intReverse > 0 && rigid.velocity.y > 0 ||
			isFixedJumpCancel && intReverse < 0 && rigid.velocity.y < 0) {
			rigid.SetRigid2DVelocityY (0);
			isFixedJumpCancel = false;
		}
	}

	void ForceJumpAction () {
		if (isForceJump) {
			isForceJump = false;
			isForceJumpNow = true;
			isGround = false;
			rigid.SetRigid2DVelocityY (jumpPower*1.225f * Time.deltaTime * 60 * intReverse);
		}
	}

	//	重力処理
	void Gravity () {
		rigid.velocity += gravity * Time.deltaTime * 60 * intReverse;
	}


	//	=================以下Collider用=================


	void GroundEnter (GameObject obj, string tagName) {
		if (obj.tag == tagName) {
			isCollision = true;
			collisionOther = obj;

			if (obj.transform.position.y < intReverse * transform.position.y) {
				isGround = true;
				isForceJumpNow = false;
			}
			//	ブロックの下側かそうでないか
			if (obj.transform.position.y < intReverse * transform.position.y) {
				isSky = false;
				isSpin = false;
				isSkyCheck = false;
			}
		}
	}
	//	地面に触れている時（「触れた」から「触れている」に修正。Enter -> Stay）
	void GroundCheck (GameObject obj, string tagName) {
		if (obj.tag == tagName) {
			//	感度を快適にする
			isFixedJumpCancel = false;
			//	ブロックの下側かそうでないか
			if (obj.transform.position.y < intReverse * transform.position.y) {
				isSky = false;
				isSpin = false;
				isSkyCheck = false;
			}
		}
	}
	//	地面を離れた時
	void SkyCheck (GameObject obj, string tagName) {
		if (obj.tag == tagName) {
			if (obj.transform.position.y > intReverse * transform.position.y) {
				isSky = true;
			}
			isSkyCheck = true;
		}
	}

	//	反転オブジェにぶつかった時。一度y軸の速度ベクトルを消す
	void OnReverse (GameObject obj) {
		if (obj.tag == "Reverse") {
			rigid.SetRigid2DVelocityY (0);
			intReverse *= -1;

			OnColliderObject_Add (obj);
			SeShot (Sound.SeName.Reverse);
		}
	}

	//	ゴールとの接触
	void OnGoal (GameObject obj) {
		if (obj.name == "Goal") {
			//	とりあえずは直スコアへ
			Stage.gameScene = Stage.GameScene.Score;
			//	ここに書くのはよくないかもだけどポーズ
			GameProgression.isPause = true;

			//	先にゴールフラグを立てる
			Stage.isGoal = true;
			Stage.RunPercent ();
			SeShot (Sound.SeName.Goal);
		}
	}

	//	トゲに当たったら
	void OnNeedle (GameObject obj) {
		if (obj.tag == "Needle") {
			isTimeOfDeath = true;
		}
	}

	//	ストップオブジェに当たったら
	void OnStop (GameObject obj) {
		if (obj.tag == "Stop") {
			GameProgression.isPause = true;

			OnColliderObject_Add (obj);
		}
	}

	//	フラッグに当たったら
	void OnFlag (GameObject obj) {
		if (obj.tag == "Flag") {
			isFlag = true;
			flagPos = obj.transform.position;
			obj.GetComponent<Flag> ().isFlag = true;
			flagReverse = intReverse;
			flagSpeedAssist = speedAssist;
			flagColorFlag = GameProgression.colorFlag;

			//	死亡時に爆発エフェクトとともに流れるのを阻止
			if (Stage.gameScene == Stage.GameScene.Game) {
				SeShot (Sound.SeName.Frag);
			}
		}
	}

	//	バーブロックに当たったら
	void OnBarBlock (GameObject obj) {
		if (obj.tag == "BarBlock") {
			isTimeOfDeath = true;
		}
	}

	//	ブロックの側面
	void OnBlockLeft (GameObject obj) {
		if (obj.tag == "Block") {
			//	Updateメソッドに対応処理を書いたのでコメント
//			isTimeOfDeath = true;
			isTrigger = true;
			triggerOther = obj;
		}
	}

	void OnSpeedUp (GameObject obj) {
		if (obj.tag == "SpeedUp") {
			speedAssist = Mathf.Min (speedAssist + 1, speed.Length-1);
			OnColliderObject_Add (obj);
			GameProgression.CreateRingEffect (gameObject, new Color (255, 102, 0, 255)/255);

			SeShot (Sound.SeName.Fast);
		}
	}

	void OnSpeedDown (GameObject obj) {
		if (obj.tag == "SpeedDown") {
			speedAssist = Mathf.Max (speedAssist - 1, 0);
			OnColliderObject_Add (obj);
			GameProgression.CreateRingEffect (gameObject, new Color (51, 255, 0, 255)/255);

			SeShot (Sound.SeName.Slow);
		}
	}

	void OnReJump (GameObject obj) {
		if (obj.tag == "ReJump") {
			OnColliderObject_Add (obj);
			isGround = true;
			isForceJumpNow = false;
			GameProgression.CreateRingEffect (gameObject, new Color (255, 51, 255, 255)/255);

			SeShot (Sound.SeName.Rejump);
		}
	}

	void OnForceJump (GameObject obj) {
		if (obj.tag == "ForceJump") {
			OnColliderObject_Add (obj);
			isForceJump = true;
			GameProgression.CreateRingEffect (gameObject, new Color (102, 0, 153, 255)/255);

			SeShot (Sound.SeName.Force);
		}
	}

	void OnColorChangeFlag (GameObject obj) {
		if (obj.tag == "ColorChangeFlag") {
			OnColliderObject_Add (obj);
			GameProgression.colorFlag = obj.GetComponent<ColorChangeFlag> ().colorFlag;

			SeShot (Sound.SeName.ColorBlock);
		}
	}
	void OnColorBlock (GameObject obj) {
		if (obj.tag == "ColorBlock") {
			isTimeOfDeath = true;
		}
	}




	//	=================以下、その他=================


	//	当たったオブジェクトを、ライフ復帰時に正常復帰させる為の処理
	//	OnTriggerObjectを必ず追加する

	//	当たった奴を追加
	void OnColliderObject_Add (GameObject obj) {
		bool colObjEnable = false;
		foreach (var cObj in colObject) {
			if (obj == cObj) { colObjEnable = true; }
		}
		obj.GetComponent<OnTriggerObject> ().isTrig = true;

		if (!colObjEnable) { colObject.Add (obj); }
		obj.GetComponent<CircleCollider2D> ().enabled = false;
	}
	//	当たった奴のアップデート
	void OnColliderObject_Update () {
		foreach (var cObj in colObject) {
			if (!cObj.GetComponent<CircleCollider2D> ().enabled) {
				cObj.transform.localScale = Vector2.Lerp (cObj.transform.localScale, Vector2.zero, 0.075f);
				cObj.transform.rotation = Quaternion.Lerp (cObj.transform.rotation, Quaternion.AngleAxis (-180*intReverse, Vector3.forward), 0.1f);
			}
		}
	}
	//	当たった奴のセットアップ
	public void OnColliderObject_Setup () {
		foreach (var cObj in colObject) {
			cObj.GetComponent<CircleCollider2D> ().enabled = true;
			cObj.GetComponent<OnTriggerObject> ().isTrig = false;
			cObj.transform.localScale = cObj.GetComponent<OnTriggerObject> ().size;
			cObj.transform.rotation = Quaternion.AngleAxis (0, Vector3.forward);
		}
	}
}
