using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class StageMap : Mover {

	//	ステージマップ用配列
	public static List<string> map = new List<string> ();

	//	マップの最大上限と下限の座標
	public static float mapTopY;
	public static float mapBottomY;
	public static float mapLengthX;

	public static string[] mapName = {
		"Map00",
		"Map01", "Map02", "Map03", "Map04", "Map05",
		"Map06", "Map07", "Map08", "Map09", "Map10",
		"Map11", "Map12", "Map13", "Map14", "Map15",
		"Map16", "Map17", "Map18", "Map19", "Map20",
		"Map21", "Map22", "Map23", "Map24", "Map25",
		"Map26", "Map27", "Map28", "Map29", "Map30",
	};

	void Awake () {
		map.Clear ();
		TextAsset txtMap = Resources.Load ("StageMapList/"+mapName[(int)Stage.stageMapList], typeof(TextAsset)) as TextAsset;
		char [] lineBreak = { '\n' };

		string[] strs = txtMap.text.Split (lineBreak);
		foreach(string str in strs){
			map.Add(str);
		}

		mapTopY = 1;
		mapBottomY = -map.Count;

		mapLengthX = map [0].Length;
	}
}
