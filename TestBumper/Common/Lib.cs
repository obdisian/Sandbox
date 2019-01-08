using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace Wavy
{
	public static class Lib
	{
		// FPSを設定する
		// UIをアニメーションさせる時はFPSを上げて、それ以外は下げる
		public static void SetFps(int frameRate)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = frameRate;
			//		Time.captureFramerate = frameRate;
		}

		//	uguiに当たっているかどうか
		public static bool IsHitUgui(Vector3 touchPos)
		{
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			pointer.position = touchPos;
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointer, results);
			return (results.Count > 0);
		}
		public static GameObject getHitUgui(Vector3 touchPos)
		{
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			pointer.position = touchPos;
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointer, results);

			foreach (var result in results)
			{
				return result.gameObject;
			}
			return null;
		}
		public static List<GameObject> getHitUguiList(Vector3 touchPos)
		{
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			pointer.position = touchPos;
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointer, results);

			var ret = new List<GameObject>();
			foreach (var result in results)
			{
				ret.Add(result.gameObject);
			}
			return ret;
		}
	}
}
