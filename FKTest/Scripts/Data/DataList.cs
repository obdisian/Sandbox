﻿using UnityEngine;
using System.Collections;


//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//
//	モデルデータクラス
//
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
public class ModelData {

	//	最大関節数
	public int PartsLength { get; set; }

	//	各パーツごとの位置
	public Vector3[] Pos { get; set; }

	//	各パーツのプリミティブの種類

	//	各パーツごとの親子関係

	//	あああ
}



//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//
//	モーションデータクラス
//
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
public class MotionData {

	//	ループフラグ (擬似Bool型 0か1で判断)
	public bool IsLoop { get; set; }

	//	最大フレーム数
	public int MaxFrame { get; set; }

	//	各モーションのキーフレーム
	public int[] KeyFrame { get; set; }

	//	補間割合
	public float[] SlerpT { get; set; }

	//	各キーごとの位置
	public Vector3[] Pos { get; set; }

	//	各キーごとの角度
	public Vector3[][] Angles { get; set; }
}

