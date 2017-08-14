using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	private static readonly string BgmPath = "Audio/BGM/";
	private static readonly string SePath = "Audio/SE/";

	//	BGMを再生するコンポーネント
	AudioSource bgmAudioSource;

	//	SEを再生するコンポーネント
	AudioSource seAudioSource;

	//	音データ
	List<AudioClip> clipList = new List<AudioClip> ();

	//	BGMにアクセスするためのテーブル
	private Dictionary<string, AudioData> poolBgm = new Dictionary<string, AudioData> ();
	//	SEにアクセスするためのテーブル 
	private Dictionary<string, AudioData> poolSe = new Dictionary<string, AudioData> ();



	/// 保持するデータ
	private class AudioData
	{
		public string key;				//	アクセス用のキー
		public string resourcesName;	//	リソース名
		public AudioClip clip;			//	音データ

		public AudioData(string key, string res) {
			this.key = key;
			resourcesName = SePath + res;
			// AudioClipの取得
			clip = Resources.Load(resourcesName) as AudioClip;
		}
	}




	//	初期化処理
	protected override void Init ()
	{
		;
	}

	//	更新処理
	private void Update ()
	{
		
	}
}
