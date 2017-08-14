using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RectMover : Mover
{
	#if UNITY_EDITOR
	public bool isGizeos = true;
	#endif


	//	当たり情報
	public enum Hit
	{
		Up,
		Down,
		Right,
		Left,
	}

	[SerializeField]
	protected Vector2 rectScale = new Vector2 (0.5f, 0.75f);

	[SerializeField]
	protected float gravityScale = 0.098f;

	protected bool isGround = false;



	public override void HitMove ()
	{
		isGround = false;

		HitHorizontal (1);
		HitHorizontal (-1);
		HitVertical (1);
		HitVertical (-1);
	}



	//	壁補正
	private void HitHorizontal (int dir)
	{
		RaycastHit2D hit = Physics2D.BoxCast (transform.position, rectScale, transform.rotation.eulerAngles.z, transform.right * dir);
		if (hit && hit.distance < rectScale.x) {
			transform.position += -transform.right * (rectScale.x - hit.distance) * dir;
			float velDist = Vector3.Dot (transform.right * dir, velocity.horizontal);
			if (velDist > 0) {
				velocity.horizontal = Vector3.zero;
			}
		}
	}

	//	地面補正
	private void HitVertical (int dir)
	{
		RaycastHit2D hit = Physics2D.BoxCast (transform.position, rectScale, transform.rotation.eulerAngles.z, transform.up * dir);
		if (hit && hit.distance < rectScale.y) {
#if false
			//	下から通り抜けれるオブジェクト
			if (dir == 1) {
				return;
			}
#endif
			transform.position += -transform.up * (rectScale.y - hit.distance) * dir;
			float velDist = Vector3.Dot (transform.up * dir, velocity.vertical);
			if (velDist > 0) {
				velocity.vertical = Vector3.zero;

				if (dir == -1) {
					isGround = true;
				}
			} else {

				//	上の判定が下にあるときは、補正座標を修正する
				if (dir == 1) {
					transform.position += transform.up * (rectScale.y - hit.distance) * dir;
				}
			}
		}
	}



#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Action <Vector3, Vector3> DrawWireRect = (pos, scale) =>
		{
			Vector3[] points = new Vector3[]
			{
				pos + transform.right * scale.x / 2 + transform.up * scale.y / 2,	//	上右
				pos - transform.right * scale.x / 2 + transform.up * scale.y / 2,	//	上右
				pos - transform.right * scale.x / 2 - transform.up * scale.y / 2,	//	上右
				pos + transform.right * scale.x / 2 - transform.up * scale.y / 2,	//	上右
				pos + transform.right * scale.x / 2 + transform.up * scale.y / 2,	//	楽に回すため追加
			};
			for (int i = 0; i < points.Length - 1; i++) {
				Gizmos.DrawLine(points[i], points[i + 1]);
			}
		};
		Action<Vector2, float> DrawWireHitRect = (dir, scale) =>
		{
			RaycastHit2D hit = Physics2D.BoxCast (transform.position, rectScale, transform.rotation.eulerAngles.z, dir);
			if (hit) {
				Vector2 point = hit.point - dir * scale;
				Gizmos.DrawLine (transform.position, point);
				DrawWireRect (point, rectScale * 2.0f);
			}
		};

		//	rectScale
		Gizmos.color = Color.green;
		DrawWireRect (transform.position, rectScale * 2.0f);

		if (!isGizeos) { return; }

		//	当たり判定描画
		Gizmos.color = Color.blue;
		DrawWireHitRect (transform.up, rectScale.y);		//	上
		DrawWireHitRect (-transform.up, rectScale.y);		//	下
		Gizmos.color = Color.red;
		DrawWireHitRect (transform.right, rectScale.x);		//	右
		DrawWireHitRect (-transform.right, rectScale.x);	//	左
	}
#endif
}
