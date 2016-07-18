using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EditUI : MonoBehaviour {

	//++++++++++++++++++++++++++++++++++++++++
	//	キープレハブ
	//++++++++++++++++++++++++++++++++++++++++
	public GameObject keyPrefab;
	public GameObject addKeyPrefab;

	//	生成するキーの親オブジェ
	public Transform keyBar;
	//	生成するキーの位置情報
	public Transform keyZero, keyMax;







	//++++++++++++++++++++++++++++++++++++++++
	//	UI
	//++++++++++++++++++++++++++++++++++++++++

	//	再生中に動くキー
	public Transform moveKey;

	//	マックスキー入力フィールド
	public InputField maxInputField;

	//	カレントキー入力フィールド
	public InputField currentInputField;

	//	ループフラグ(トグル)
	public Toggle loopToggle;

	//	イージングドロップダウン
	public Dropdown easingDropdown;

	//	モーション位置テキスト (整数にするか、切り捨てにするか)
	public Text[] posTextXYZ;

	//	モーション回転テキスト (整数にするか、切り捨てにするか)
	public Text[] angleTextXYZ;






	int currentTime = 0;

	List<Button> createKeyButton = new List<Button> ();

	//++++++++++++++++++++++++++++++++++++++++
	//	初期値
	//++++++++++++++++++++++++++++++++++++++++
	const bool first_IsLoop = true;
	const int first_MaxFrame = 60;
	const int first_KeyFrame = 0;
	const float first_SlerpT = 0.1f;
	readonly Vector3 first_Pos = Vector3.zero;
	readonly Vector3[] first_Angles = new Vector3[]
	{
		Vector3.zero, Vector3.zero,
		new Vector3 (0, 0, 90), new Vector3 (0, 0, -90),
		Vector3.zero, Vector3.zero, Vector3.zero,
		Vector3.zero, Vector3.zero, Vector3.zero,
	};
	//++++++++++++++++++++++++++++++++++++++++



	EditManager edMng;



	//================================================================================
	//	初期化
	//================================================================================
	void Awake () {

		edMng = GetComponent<EditManager> ();

		//	キーのマス目作成
		int count = 3;
		for (int i = 0; i < count; i++) {
			float x = Info.RatioMap (i, 0, count-1, keyZero.transform.position.x, keyMax.transform.position.x);
			GameObject obj = Instantiate (keyPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			obj.transform.SetParent (keyBar);
			obj.transform.position = Vector3.up * keyBar.position.y + new Vector3 (x, 0, 0);
			obj.transform.localScale = Vector3.one;
		}

		//++++++++++++++++++++++++++++++++++++++++
		//	データを初期化
		edMng.edit_IsLoop = first_IsLoop;
		edMng.edit_MaxFrame = first_MaxFrame;

		maxInputField.text = "" + first_MaxFrame;

		//	新規データの追加
		AddNewData ();
		//++++++++++++++++++++++++++++++++++++++++
	}


	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void Update () {
		
		if (edMng.isPlay) {
			MotionPlay ();
		}
	}




	//================================================================================
	//	モーション再生
	//================================================================================
	void MotionPlay () {
		currentTime++;
		if (!edMng.edit_IsLoop) {
			if (currentTime >= edMng.edit_MaxFrame - 1) {
				currentTime = edMng.edit_MaxFrame - 1;
			}
		}
		float x = Info.RatioMap (currentTime % edMng.edit_MaxFrame, 0, edMng.edit_MaxFrame, keyZero.transform.position.x, keyMax.transform.position.x);
		moveKey.transform.position = Vector3.up * keyBar.position.y + new Vector3 (x, -10, 0);
	}


	//================================================================================
	//	新規データの追加
	//================================================================================
	void AddNewData () {

		//	各パラメータ初期化
//		edit_IsLoop = first_IsLoop;
//		edit_MaxFrame = first_MaxFrame;
		edMng.edit_KeyFrame.Add (first_KeyFrame);
		edMng.edit_SlerpT.Add (first_SlerpT);
		edMng.edit_Pos.Add (first_Pos);
		edMng.edit_Angles.Add (first_Angles);

		GameObject obj = Instantiate (addKeyPrefab, Vector3.up * keyBar.position.y + new Vector3 (keyZero.transform.position.x, 0, 0), Quaternion.identity) as GameObject;
		obj.transform.SetParent (keyBar);
		obj.transform.localScale = Vector3.one;
		obj.transform.rotation = Quaternion.AngleAxis (45, Vector3.forward);
		createKeyButton.Add (obj.GetComponent<Button> ());

		//	編集中キーを最新にする
		edMng.nowEditNum = edMng.edit_KeyFrame.Count - 1;

//		maxInputField.text = "" + first_MaxFrame;
		currentInputField.text = "0";

		//	クリックアクションの追加
		int clickNum = edMng.nowEditNum;
		createKeyButton [edMng.nowEditNum].onClick.AddListener (() => OnNowKeyChange (clickNum));
	}





	//================================================================================
	//	モーション再生ボタン
	//================================================================================
	public void OnPlayButton () {
		currentTime = 0;
		edMng.isPlay = !edMng.isPlay;
		moveKey.transform.position = Vector3.up * keyBar.position.y + new Vector3 (keyZero.transform.position.x, -10, 0);
	}


	//================================================================================
	//	キー追加ボタン
	//================================================================================
	public void OnKeyAdd () {
		AddNewData ();
	}


	//================================================================================
	//	現在のキーを変更ボタン
	//================================================================================
	public void OnCurrentKeyChange () {
//		int num = 0;
//		if (int.TryParse (currentInputField.text, out num)) {
//			edit_KeyFrame [nowEditNum] = num;
//
//			float x = Info.RatioMap (num, 0, edit_MaxFrame, keyZero.transform.position.x, keyMax.transform.position.x);
//			createKeyButton [nowEditNum].transform.position = Vector3.up * keyBar.position.y + new Vector3 (x, 0, 0);
//		}
//		else {
//		}
	}


	//================================================================================
	//	上に配置されているキーを選択した場合、フォーカスするキーを変更する
	//================================================================================
	public void OnNowKeyChange (int keyNumbar) {
		
		edMng.nowEditNum = keyNumbar;
		currentInputField.text = "" + edMng.edit_KeyFrame [edMng.nowEditNum];

		Debug.Log ("キー " + keyNumbar);
	}



	//================================================================================
	//	現在のキーを編集終了後
	//================================================================================
	public void EndField_CurrentKeyFrame () {
		int num = 0;
		if (int.TryParse (currentInputField.text, out num)) {
			edMng.edit_KeyFrame [edMng.nowEditNum] = num;

			float x = Info.RatioMap (num, 0, edMng.edit_MaxFrame, keyZero.transform.position.x, keyMax.transform.position.x);
			createKeyButton [edMng.nowEditNum].transform.position = Vector3.up * keyBar.position.y + new Vector3 (x, 0, 0);
		}
		else {
			currentInputField.text = "" + edMng.edit_KeyFrame [edMng.nowEditNum];
		}
	}


	//================================================================================
	//	最大フレーム数の編集終了後
	//================================================================================
	public void EndField_MaxKeyFrame () {
		int num = 0;
		if (int.TryParse (maxInputField.text, out num)) {
			edMng.edit_MaxFrame = num;

			//	設置した全てのキーの位置を修正
			for (int i = 0; i < edMng.edit_KeyFrame.Count; i++) {
				float x = Info.RatioMap (edMng.edit_KeyFrame [i], 0, edMng.edit_MaxFrame, keyZero.transform.position.x, keyMax.transform.position.x);
				createKeyButton [i].transform.position = Vector3.up * keyBar.position.y + new Vector3 (x, 0, 0);
			}
		}
		else {
			maxInputField.text = "" + edMng.edit_MaxFrame;
		}
	}


	//================================================================================
	//	ループするかどうかのチェックボックス
	//================================================================================
	public void CheckBox_Loop () {
		edMng.edit_IsLoop = loopToggle.isOn;
	}


	//================================================================================
	//	補間の割合のドロップダウン
	//================================================================================
	public void Dropdown_Slerp () {
		float[] slerpT = {
			0.02f, 0.025f, 0.05f, 0.075f, 0.1f, 0.125f, 0.15f, 0.2f, 0.25f, 0.3f, 0.4f, 0.5f, 1.0f,
		};
		edMng.edit_SlerpT [edMng.nowEditNum] = slerpT [easingDropdown.value];
	}
}
