using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using animate;

//	残像クラス
public class Afterimage : MonoBehaviour
{
	//	終了処理
	protected virtual void OnDestroy ()
	{
//		EasingManager.Instance.InfoList.Remove (this);
	}
}

//	残像操作クラス
public class AfterimageController
{
	private static readonly Color ClearColor = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	private SpriteRenderer ownerObject;
	private SpriteRenderer spriteRenderer;

	private int counter = 0;


	//	コンストラクタ
	public AfterimageController (GameObject obj)
	{
		ownerObject = obj.GetComponent<SpriteRenderer> ();
	}

	//	更新処理
	public void Move ()
	{
		if (counter % 60 == 0) {
			CreateEffect ();
		}
	}

	//	残像を作る
	public void CreateEffect ()
	{
		SpriteRenderer sr = new GameObject ("Afterimage").AddComponent<SpriteRenderer> ();
		sr.transform.position = ownerObject.transform.position;
		sr.transform.localScale = ownerObject.transform.localScale;
		sr.transform.rotation = ownerObject.transform.rotation;
		sr.sprite = ownerObject.sprite;
		var easing = sr.SetEasing (Easing.Type.Linear, Easing.Ease.In, ClearColor, 0.05f);
		easing.DeleteAction = () => { GameObject.Destroy (sr.gameObject); };
	}
}
