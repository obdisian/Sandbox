using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//
//	データ管理クラス
//	使用するデータ
//	MotionListフォルダ以下、各種モーション
//
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
public class Data : MonoBehaviour {


	//================================================================================
	//	初期設定
	//================================================================================
	public static void Setup () {
		DirectoryCheck (DirectoryPath () + "/MotionList");
	}


	//================================================================================
	//	データの読み込み
	//================================================================================
	public static MotionData Load (string fileName) {
		string path = DirectoryPath () + "/MotionList/" + fileName + ".txt";
		string str = File.ReadAllText (path);
		return StringToMotionData (str);
	}


	//================================================================================
	//	データの保存
	//================================================================================
	public static void Save (string fileName, MotionData mData) {
		string path = DirectoryPath () + "/MotionList/" + fileName + ".txt";
		StreamWriter fileWriter = new StreamWriter (path);
		fileWriter.WriteLine(MotionDataToString (mData));
		fileWriter.Close ();
	}


	//================================================================================
	//	ディレクトリがあるかどうかのチェック、なければ作成する
	//================================================================================
	static void DirectoryCheck (string path) {
		
		if (Directory.Exists (path)) {
			
		} else {
			Directory.CreateDirectory(path);
		}
	}


	//================================================================================
	//	ディレクトリパス
	//================================================================================
	static string DirectoryPath () {

		#if UNITY_EDITOR
		return Application.dataPath + "/StreamingAssets";

		#elif UNITY_IOS || UNITY_ANDROID
		string path = Application.persistentDataPath;
		if (!File.Exists (path)) {
		StreamWriter fileWriter = File.CreateText (path);
		fileWriter.Close ();
		}
		return path;
		#endif
	}




	//================================================================================
	//	モーションデータを文字列に変換
	//================================================================================
	static string MotionDataToString (MotionData mData) {

		string str =
			(mData.IsLoop ? 1 : 0) + "/" +
			mData.MaxFrame + "/";

		for (int i = 0; i < mData.KeyFrame.Length; i++) {
			str += mData.KeyFrame [i] + (i == mData.KeyFrame.Length - 1 ? "" : ",");
		}
		str += "/";
		for (int i = 0; i < mData.SlerpT.Length; i++) {
			str += mData.SlerpT [i] + (i == mData.SlerpT.Length - 1 ? "" : ",");
		}
		str += "/";
		for (int i = 0; i < mData.Pos.Length; i++) {
			str += mData.Pos [i].x + "=";
			str += mData.Pos [i].y + "=";
			str += mData.Pos [i].z + (i == mData.Pos.Length - 1 ? "" : ",");
		}
		str += "/";
		for (int i = 0; i < mData.Angles.Length; i++) {
			for (int j = 0; j < mData.Angles [0].Length; j++) {
				str += mData.Angles [i][j].x + "+";
				str += mData.Angles [i][j].y + "+";
				str += mData.Angles [i][j].z + (j == mData.Angles [0].Length - 1 ? "" : "=");
			}
			str += i == mData.Angles.Length - 1 ? "" : ",";
		}

		return str;
	}


	//================================================================================
	//	文字列をモーションデータに変換
	//================================================================================
	static MotionData StringToMotionData (string strData) {

		MotionData motData = new MotionData ();
		string[] motStr = strData.Split ('/');

		motData.IsLoop = int.Parse (motStr [0]) == 0 ? false : true;
		motData.MaxFrame = int.Parse (motStr [1]);

		string[] keyStr = motStr [2].Split (',');
		motData.KeyFrame = new int [keyStr.Length];
		for (int i = 0; i < keyStr.Length; i++) {
			motData.KeyFrame [i] = int.Parse (keyStr [i]);
		}

		string[] slerpStr = motStr [3].Split (',');
		motData.SlerpT = new float [slerpStr.Length];
		for (int i = 0; i < slerpStr.Length; i++) {
			motData.SlerpT [i] = float.Parse (slerpStr [i]);
		}

		string[] posStr = motStr [4].Split (',');
		motData.Pos = new Vector3 [posStr.Length];
		for (int i = 0; i < posStr.Length; i++) {
			string[] xyzStr = posStr [i].Split ('=');
			motData.Pos [i] = new Vector3 (float.Parse (xyzStr [0]), float.Parse (xyzStr [1]), float.Parse (xyzStr [2]));
		}

		string[] angleStr = motStr [5].Split (',');
		motData.Angles = new Vector3[angleStr.Length][];
		for (int i = 0; i < angleStr.Length; i++) {
			string[] partsStr = angleStr [i].Split ('=');
			motData.Angles [i] = new Vector3 [partsStr.Length];
			for (int j = 0; j < partsStr.Length; j++) {
				string[] xyzStr = partsStr [j].Split ('+');
				motData.Angles [i][j] = new Vector3 (float.Parse (xyzStr [0]), float.Parse (xyzStr [1]), float.Parse (xyzStr [2]));
			}
		}

		return motData;
	}
}
