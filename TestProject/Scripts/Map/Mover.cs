using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
	protected Vector3 position;
	protected Vector3 oldPosition;

	protected class Velocity
	{
		public Vector3 vertical = Vector3.zero;
		public Vector3 horizontal = Vector3.zero;
		public void Clear () {
			vertical = Vector3.zero;
			horizontal = Vector3.zero;
		}
	}
	protected Velocity velocity = new Velocity ();


	//	継承先での更新処理
	protected abstract void Move ();
	//	当たり更新処理
	public abstract void HitMove ();


	//	初期化処理
	protected virtual void Start ()
	{
		MoverManager.Instance.MoverList.Add (this);
	}

	//	終了処理
	protected virtual void OnDestroy ()
	{
		MoverManager.Instance.MoverList.Remove (this);
	}

	//	更新処理
	public void BaseMove ()
	{
		position = oldPosition = transform.position;

		Move ();

		position += velocity.vertical;
		position += velocity.horizontal;
		transform.position = position;
	}
}
