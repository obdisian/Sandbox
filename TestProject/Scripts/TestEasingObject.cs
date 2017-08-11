using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animate;

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
			transform.SetEasing (Target.Position, speed, Easing.Type.Quadratic, Easing.Ease.Out, transform.position, nextPos);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			isBigSize = !isBigSize;
			Vector2 size = isBigSize ? Vector2.one * 2 : Vector2.one;
			transform.SetEasing (Target.LocalScale, speed, Easing.Type.Quadratic, Easing.Ease.Out, transform.localScale, size);
		}
	}
}
