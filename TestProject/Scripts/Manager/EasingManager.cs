using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace animate
{
	public enum Target
	{
		Position,
		LocalScale,

		RectPosition,
		RectScale,

		SpriteColor,
	}
	public static class Extension
	{
		//	todo: 初期座標は自身から取得で、スピードは何秒間アニメーションさせるかで決める
		public static BaseInfo SetEasing(this Transform transform, Target target, Easing.Type type, Easing.Ease ease, Vector3 start, Vector3 end, float sec)
		{
			switch (target)
			{
			case Target.Position:
				return new PosInfo (type, ease, transform, start, end, sec);
			case Target.LocalScale:
				return new ScaleInfo (type, ease, transform, start, end, sec);
			default:
				Debug.LogError ("SetEasing [ " + target + " ]");
				break;
			}
			return null;
		}

		//	todo: 初期座標は自身から取得で、スピードは何秒間アニメーションさせるかで決める
		public static BaseInfo SetEasing(this RectTransform transform, Target target, Easing.Type type, Easing.Ease ease, Vector3 start, Vector3 end, float sec)
		{
			switch (target)
			{
			case Target.RectPosition:
				return new PosInfo (type, ease, transform, start, end, sec);
			case Target.RectScale:
				return new ScaleInfo (type, ease, transform, start, end, sec);
			default:
				Debug.LogError ("SetEasing [ " + target + " ]");
				break;
			}
			return null;
		}

		//	スプライトのカラーを操作する
		public static BaseInfo SetEasing(this SpriteRenderer spriteRenderer, Easing.Type type, Easing.Ease ease, Color end, float sec)
		{
			return new SpriteColorInfo (type, ease, spriteRenderer, spriteRenderer.color, end, sec);
		}
	}

	//	補間用情報クラス
	public abstract class BaseInfo
	{
		public float Sec { get; set; }
		public Easing.Type Type { get; set; }
		public Easing.Ease Ease { get; set; }
		public IEnumerator Enumerator { get; set; }
		public Action DeleteAction { get; set; }	//	アニメーション終了時に呼びたい処理

		protected abstract void ApplyEasing(float t);
		protected IEnumerator Calculate()
		{
			float t = 0.0f;
			while (t != 1.0f)
			{
				t += Sec;
				if (t > 1.0f) { t = 1.0f; }
				ApplyEasing(t);
				yield return null;
			}
		}

		public BaseInfo(Easing.Type type, Easing.Ease ease, float sec)
		{
			Type = type;
			Ease = ease;
			Sec = sec;
			Enumerator = Calculate();
			DeleteAction = null;
			EasingManager.Instance.InfoList.Add (this);
		}
	}

	//	三次元ベクトル情報クラス
	public abstract class TransformInfo : BaseInfo
	{
		public Transform Transform { get; set; }
		public Vector3 Start { get; set; }
		public Vector3 End { get; set; }
		public TransformInfo(Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end, float sec)
			: base (type, ease, sec)
		{
			Transform = transform;
			Start = start;
			End = end;
		}
	}

	//	座標補間クラス
	public class PosInfo : TransformInfo
	{
		public PosInfo (Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end, float sec)
			: base (type, ease, transform, start, end, sec) { }
		protected override void ApplyEasing(float t)
		{
			Transform.position = Easing.Curve(Type, Ease, t, Start, End);
		}
	}

	//	スケール補間クラス
	public class ScaleInfo : TransformInfo
	{
		public ScaleInfo(Easing.Type type, Easing.Ease ease, Transform transform, Vector3 start, Vector3 end, float sec)
			: base (type, ease, transform, start, end, sec) { }
		protected override void ApplyEasing(float t)
		{
			Transform.localScale = Easing.Curve(Type, Ease, t, Start, End);
		}
	}

	//	スプライトの色補間クラス
	public class SpriteColorInfo : BaseInfo
	{
		public SpriteRenderer SpriteRenderer { get; set; }
		public Color Start { get; set; }
		public Color End { get; set; }
		public SpriteColorInfo(Easing.Type type, Easing.Ease ease, SpriteRenderer spriteRenderer, Color start, Color end, float sec)
			: base (type, ease, sec)
		{
			SpriteRenderer = spriteRenderer;
			Start = start;
			End = end;
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
	public List<animate.BaseInfo> InfoList { get; set; }

	//	初期化処理
	protected override void Init ()
	{
		InfoList = new List<animate.BaseInfo> ();

		//	シーン切り替えの直前に登録してあるアニメーションを削除する
		FadeManager.Instance.CallBackChangeScene += () => { InfoList.Clear (); };
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
				animate.BaseInfo info = InfoList [i];
				InfoList.Remove (InfoList [i]);
				if (info.DeleteAction != null) { info.DeleteAction (); }
			}
		}

//		//	Enumeratorがnullのものを全て削除
//		InfoList.RemoveAll(InfoList => InfoList.Enumerator == null);
	}
}