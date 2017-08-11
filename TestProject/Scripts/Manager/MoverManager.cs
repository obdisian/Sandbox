using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverManager : SingletonMonoBehaviour<MoverManager>
{
	public List<Mover> MoverList { get; set; }

	//	初期化処理
	protected override void Init ()
	{
		MoverList = new List<Mover> ();
	}

	//	更新処理
	private void Update ()
	{
		//	リストがないとき通らない
		if (MoverList.Count <= 0)
		{
			return;
		}

		//	動的オブジェクトの更新
		foreach (Mover mover in MoverList)
		{
			mover.BaseMove ();
		}
		//	当たり更新処理
		foreach (Mover mover in MoverList)
		{
			mover.HitMove ();
		}
	}
}
