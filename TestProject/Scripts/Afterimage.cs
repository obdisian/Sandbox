using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Animate;

public class Afterimage
{
	private static readonly Color ClearColor = new Color (1.0f, 1.0f, 1.0f, 0.0f);

	private SpriteRenderer ownerObject;
	private SpriteRenderer spriteRenderer;

	private int counter = 0;


	//	コンストラクタ
	public Afterimage (GameObject obj)
	{
		ownerObject = obj.GetComponent<SpriteRenderer> ();
	}

	//	更新処理
	public void Move ()
	{
		if (counter % 20 == 0) {
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
