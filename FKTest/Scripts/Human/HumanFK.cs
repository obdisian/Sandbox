using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanFK : PartsFK {

	public GameObject Parts { get; set; }

	public GameObject parts;

	public List<HumanFK> children = new List<HumanFK> ();

	Vector3 partsPos, partsScale;


	//================================================================================
	//	初期設定
	//================================================================================
	public void Setup (GameObject owner, int createCount, GameObject prefab, Vector3 partsPos, Vector3 partsScale) {
		
		base.Setup (owner);
		this.partsPos = partsPos;
		this.partsScale = partsScale;

		transform.localScale = Vector3.one * 0.5f;


		if (createCount <= 0) {
			return;
		}

		GameObject obj = Instantiate (prefab, partsPos * partsScale.y, Quaternion.identity) as GameObject;
		obj.GetComponent<HumanFK> ().Setup (gameObject, createCount - 1, prefab, partsPos, partsScale);
		children.Add (obj.GetComponent<HumanFK> ());
	}


	//================================================================================
	//	初期化
	//================================================================================
	void Start () {
		//	骨作成
		Parts = Instantiate (parts, partsPos * partsScale.y * 0.5f, Quaternion.identity) as GameObject;
		Parts.GetComponent<PartsFK> ().Setup (gameObject);
		Parts.transform.localScale = partsScale;
	}


	//================================================================================
	//	毎フレーム更新
	//================================================================================
	void Update () {
		UpdateParts ();
	}
}
