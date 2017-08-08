using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEasingObject : MonoBehaviour {

	public float speed;

	private Vector2 nextPos;
	private bool isBigSize = false;

	private void Start ()
	{
		nextPos = new Vector2 (5, transform.position.y);
	}
	
	private void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			nextPos.x *= -1;
			Animate.PosInfo info = new Animate.PosInfo (speed, Easing.Type.Quadratic, Easing.Ease.Out, transform, transform.position, nextPos);
			EasingManager.Instance.PosInfoList.Add (info);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			isBigSize = !isBigSize;
			Vector2 size = isBigSize ? Vector2.one * 2 : Vector2.one;
			Animate.ScaleInfo info = new Animate.ScaleInfo (speed, Easing.Type.Quadratic, Easing.Ease.Out, transform, transform.localScale, size);
			EasingManager.Instance.ScaleInfoList.Add (info);
		}
	}
}
