using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;

// データアクセスクラス
public static class Data {

	private static readonly string ShiftJis = "Shift_JIS";

	// データ保存可能パス
	public static string LocalPath {
		get {
#if UNITY_EDITOR
			return Application.dataPath + "/StreamingAssets/AppData/";
#elif UNITY_IOS || UNITY_ANDROID
			return Application.persistentDataPath + "/AppData/";
#endif
		}
	}

	// 指定のディレクトリが無い場合、作成する
	public static bool Check (string path) {
		if (!Directory.Exists (path)) {
			Directory.CreateDirectory(path);
			return false;
		}
		return true;
	}

	// 指定したパスの直下にあるファイル名を取得
	public static string[] GetFileNames (string path) {
		FileInfo[] fileInfos = new DirectoryInfo (path).GetFiles ();
		string[] ret = new string[fileInfos.Length];
		for (int i = 0; i < fileInfos.Length; i++) {
			ret [i] = fileInfos [i].Name;
		}
		return ret;
	}

	// 読み込み処理
	public static string Load (string path) {
		string ret = "";
		if (Check (LocalPath)) {
			return ret;
		}
		using (StreamReader streamReader = new StreamReader (path, Encoding.GetEncoding(ShiftJis))) {
			ret = streamReader.ReadToEnd ();
		}
		return ret;
//		return File.ReadAllText (path);
	}

	// 書き込み処理
	public static void Save (string path, string text) {
		Check (LocalPath);
		using(StreamWriter streamWriter = new StreamWriter(path, false, Encoding.GetEncoding(ShiftJis))) {
			streamWriter.Write (text);
		}
	}
}
