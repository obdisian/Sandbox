using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scene
{
	//	シーン名の定義
	public enum Name {
		Title,
		Game,
		Result,
	}

	//	拡張関数
	public static string ToString (this Name name) {
		return "" + name;
	}

	//	シーンの呼び出し
	public static void Load (Name name)
	{
		FadeManager.Instance.LoadScene (name.ToString ());
	}
}
