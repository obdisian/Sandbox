using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public static class Lib {

	// FPSを設定する
	public static void SetFps (int frameRate) {
		QualitySettings.vSyncCount = 1;
		Application.targetFrameRate = frameRate;
//		Time.captureFramerate = frameRate;
	}

	//	uguiに当たっているかどうか
	public static bool IsHitUgui (Vector3 touchPos) {
		PointerEventData pointer = new PointerEventData (EventSystem.current);
		pointer.position = touchPos;
		List<RaycastResult> result = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (pointer, result);
		return (result.Count > 0);
	}
}
