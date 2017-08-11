using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Animate
{
	public enum Target
	{
		Position,
		LocalScale,
	}
	public static class Extension
	{
		//	todo: 初期座標は自身から取得で、スピードは何秒間アニメーションさせるかで決める
		public static void SetEasing(this Transform transform, Target target, float speed, Easing.Type type, Easing.Ease ease, Vector3 start, Vector3 end)
		{
			switch (target)
			{
			case Target.Position:
				{
					PosInfo info = new PosInfo(speed, type, ease, transform, start, end);
					EasingManager.Instance.PosInfoList.Add(info);
				}
				break;
			case Target.LocalScale:
				{
					ScaleInfo info = new ScaleInfo(speed, type, ease, transform, start, end);
					EasingManager.Instance.ScaleInfoList.Add(info);
				}
				break;
			default:
				Debug.LogError("SetEasing [ " + target + " ]");
				break;
			}
		}
	}

	//	補間用情報クラス
	public abstract class BaseInfo
	{
		public float Speed { get; set; }
		public Easing.Type Type { get; set; }
		public Easing.Ease Ease { get; set; }
		public IEnumerator Enumerator { get; set; }

		protected abstract void ApplyEasing(float t);
		protected IEnumerator Calculate()
		{
			float t = 0.0f;
			while (t != 1.0f)
			{
				t += Speed;
				if (t > 1.0f) { t = 1.0f; }
				ApplyEasing(t);
				yield return null;
			}
		}

		public BaseInfo(float speed, Easing.Type type, Easing.Ease ease)
		{
			Speed = speed;
			Type = type;
			Ease = ease;
			Enumerator = Calculate();
		}
	}

	//	三次元ベクトル情報クラス
	public abstract class Vector3Info : BaseInfo
	{
		public Transform Transform { get; set; }
		public Vector3 Start { get; set; }
		public Vector3 End { get; set; }
		public Vector3Info(float speed, Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end)
			: base (speed, type, ease)
		{
			Transform = transform;
			Start = start;
			End = end;
		}
	}

	//	座標補間クラス
	public class PosInfo : Vector3Info
	{
		protected override void ApplyEasing(float t)
		{
			Transform.position = Easing.Curve(Type, Ease, t, Start, End);
		}
		public PosInfo (float speed, Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end)
			: base (speed, type, ease, transform, start, end) { }
	}

	//	スケール補間クラス
	public class ScaleInfo : Vector3Info
	{
		protected override void ApplyEasing(float t)
		{
			Transform.localScale = Easing.Curve(Type, Ease, t, Start, End);
		}
		public ScaleInfo(float speed, Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end)
			: base (speed, type, ease, transform, start, end) { }
	}
}

//	補間管理クラス
public class EasingManager : SingletonMonoBehaviour<EasingManager>
{
	public List<Animate.PosInfo> PosInfoList { get; set; }
	public List<Animate.ScaleInfo> ScaleInfoList { get; set; }

	//	初期化
	protected override void Init ()
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