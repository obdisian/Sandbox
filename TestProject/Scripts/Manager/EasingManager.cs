using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animate
{
	//	補間用情報クラス
	public abstract class BaseInfo
	{
		public float Speed { get; set; }
		public Easing.Type Type { get; set; }
		public Easing.Ease Ease { get; set; }
		public IEnumerator Enumerator { get; set; }
		protected abstract IEnumerator Calculate ();

		public BaseInfo (float speed, Easing.Type type, Easing.Ease ease)
		{
			Speed = speed;
			Type = type;
			Ease = ease;
			Enumerator = Calculate ();
		}
	}

	//	座標補間クラス
	public class PosInfo : BaseInfo
	{
		public Transform Transform { get; set; }
		public Vector3 StartPos { get; set; }
		public Vector3 EndPos { get; set; }
		public PosInfo (float speed, Easing.Type type, Easing.Ease ease, Transform transform, Vector3 startPos, Vector3 endPos)
			: base (speed, type, ease)
		{
			Transform = transform;
			StartPos = startPos;
			EndPos = endPos;
		}
		protected override IEnumerator Calculate ()
		{
			for (float t = 0; t < 1.0f; t += Speed) {
				t += Speed;
				Transform.position = Easing.Curve (Type, Ease, t, StartPos, EndPos);
				yield return null;
			}
		}
	}

	//	スケール補間クラス
	public class ScaleInfo : BaseInfo
	{
		public Transform Transform { get; set; }
		public Vector3 StartScale { get; set; }
		public Vector3 EndScale { get; set; }
		public ScaleInfo (float speed, Easing.Type type, Easing.Ease ease, Transform transform, Vector3 startScale, Vector3 endScale)
			: base (speed, type, ease)
		{
			Transform = transform;
			StartScale = startScale;
			EndScale = endScale;
		}
		protected override IEnumerator Calculate ()
		{
			for (float t = 0; t < 1.0f; t += Speed) {
				t += Speed;
				Transform.localScale = Easing.Curve (Type, Ease, t, StartScale, EndScale);
				yield return null;
			}
		}
	}
}

//	補間管理クラス
public class EasingManager : SingletonMonoBehaviour<EasingManager>
{
	//	補間させたいクラスのリスト
	public List<Animate.PosInfo> PosInfoList { get; set; }
	public List<Animate.ScaleInfo> ScaleInfoList { get; set; }

	//	初期化
	private void Start ()
	{
		PosInfoList = new List<Animate.PosInfo> ();
		ScaleInfoList = new List<Animate.ScaleInfo> ();
	}

	//	更新
	private void Update ()
	{
		PosCalc ();
		ScaleCalc ();
	}

	//	座標計算
	private void PosCalc ()
	{
		//	要素がないときは通らない
		if (PosInfoList.Count <= 0) { return; }

		//	補間アニメーションの更新処理
		foreach (var unit in PosInfoList) {
			if (!unit.Enumerator.MoveNext ()) {
				unit.Enumerator = null;
			}
		}
		//	Enumeratorがnullのものを全て削除
		PosInfoList.RemoveAll(PosInfoList => PosInfoList.Enumerator == null);
	}

	//	スケール計算
	private void ScaleCalc ()
	{
		//	要素がないときは通らない
		if (ScaleInfoList.Count <= 0) { return; }

		//	補間アニメーションの更新処理
		foreach (var unit in ScaleInfoList) {
			if (!unit.Enumerator.MoveNext ()) {
				unit.Enumerator = null;
			}
		}
		//	Enumeratorがnullのものを全て削除
		ScaleInfoList.RemoveAll(ScaleInfoList => ScaleInfoList.Enumerator == null);
	}
}
