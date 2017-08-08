using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

//	基本情報クラス
public static class Info
{
	private static int mapWidth = 100;
	private static int mapHeight = 100;
	public static int MapWidth
	{
		get { return mapWidth; }
		set { mapWidth = value.Clamp (10, 100); }
	}
	public static int MapHeight
	{
		get { return mapHeight; }
		set { mapHeight = value.Clamp (10, 100); }
	}



	//	指定した座標がマス目内であるかどうか
	public static bool IsGridRange (Vector3 gridPos) {

		if (gridPos.x < 0 || gridPos.x >= Info.MapWidth ||
			gridPos.y < 0 || gridPos.y >= Info.MapHeight) {
			return false;
		}
		return true;
	}


	//	Touch情報からマス目の座標を取得する
	public static Vector3 GetGridPos (Touch touch) {

		Vector3 pos = touch.position;
		pos.z = -Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint (pos);

		//	四捨五入してグリッド座標を求める
		Vector3 gridPos = Vector3.zero;
		gridPos.x = Mathf.Round (pos.x);
		gridPos.y = Mathf.Round (pos.y);
		gridPos.z = 0;
		return gridPos;
	}
}
