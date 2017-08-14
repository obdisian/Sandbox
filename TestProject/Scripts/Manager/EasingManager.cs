using System;
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
		SpriteColor,
	}
	public static class Extension
	{
		//	todo: 初期座標は自身から取得で、スピードは何秒間アニメーションさせるかで決める
		public static BaseInfo SetEasing(this Transform transform, Target target, float speed, Easing.Type type, Easing.Ease ease, Vector3 start, Vector3 end)
		{
			switch (target)
			{
			case Target.Position:
				return new PosInfo (speed, type, ease, transform, start, end);
			case Target.LocalScale:
				return new ScaleInfo (speed, type, ease, transform, start, end);
			default:
				Debug.LogError ("SetEasing [ " + target + " ]");
				break;
			}
			return null;
		}

		//	スプライトのカラーを操作する
		public static BaseInfo SetEasing(this SpriteRenderer spriteRenderer, Easing.Type type, Easing.Ease ease, Color end, float speed)
		{
			return new SpriteColorInfo (speed, type, ease, spriteRenderer, spriteRenderer.color, end);
		}
	}

	//	補間用情報クラス
	public abstract class BaseInfo
	{
		public float Speed { get; set; }
		public Easing.Type Type { get; set; }
		public Easing.Ease Ease { get; set; }
		public IEnumerator Enumerator { get; set; }
		public Action DeleteAction { get; set; }	//	削除されるときに呼びたい処理

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
			DeleteAction = null;
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
			: base (speed, type, ease, transform, start, end)
		{
			EasingManager.Instance.InfoList.Add (this);
		}
	}

	//	スケール補間クラス
	public class ScaleInfo : Vector3Info
	{
		protected override void ApplyEasing(float t)
		{
			Transform.localScale = Easing.Curve(Type, Ease, t, Start, End);
		}
		public ScaleInfo(float speed, Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end)
			: base (speed, type, ease, transform, start, end)
		{
			EasingManager.Instance.InfoList.Add (this);
		}
	}

	//	スプライトのカラーフェード
	public class SpriteColorInfo : BaseInfo
	{
		public SpriteRenderer SpriteRenderer { get; set; }
		public Color Start { get; set; }
		public Color End { get; set; }
		public SpriteColorInfo(float speed, Easing.Type type, Easing.Ease ease, SpriteRenderer spriteRenderer, Color start, Color end)
			: base (speed, type, ease)
		{
			SpriteRenderer = spriteRenderer;
			Start = start;
			End = end;
			EasingManager.Instance.InfoList.Add (this);
		}
		protected override void ApplyEasing(float t)
		{
			SpriteRenderer.color = Easing.Curve(Type, Ease, t, Start, End);
		}
	}
}

//	補間管理クラス
public class EasingManager : SingletonMonoBehaviour<EasingManager>
{
	public List<Animate.BaseInfo> InfoList { get; set; }

	//	初期化処理
	protected override void Init ()
	{
		InfoList = new List<Animate.BaseInfo> ();
	}

	//	更新処理
	private void Update ()
	{
		//	要素がないときは通らない
		if (InfoList.Count <= 0) { return; }

		//	補間アニメーションの更新処理
		foreach (var unit in InfoList)
		{
			if (!unit.Enumerator.MoveNext ())
			{
				unit.Enumerator = null;
			}
		}

		//	処理が終了したものを削除する
		for (int i = InfoList.Count - 1; i >= 0; i--)
		{
			if (InfoList [i].Enumerator == null) {
				Animate.BaseInfo info = InfoList [i];
				InfoList.Remove (InfoList [i]);
				if (info.DeleteAction != null) { info.DeleteAction (); }
			}
		}

//		//	Enumeratorがnullのものを全て削除
//		InfoList.RemoveAll(InfoList => InfoList.Enumerator == null);
	}
}