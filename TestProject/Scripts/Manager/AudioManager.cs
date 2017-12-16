using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//  サウンド管理
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
	//  種類
	private enum Type
	{
		Bgm,
		Se,
	}

	//  保持するデータ
	private class Data
	{
		public string Key;
		public string ResourceName;
		public AudioClip Clip;
		public Data(string key, string res)
		{
			Key = key;
			ResourceName = res;
			Clip = Resources.Load(ResourceName) as AudioClip;
		}
	}

	// サウンドリソース
	private AudioSource m_SourceBgm = null;
	private AudioSource m_SourceSe = null;
	private Dictionary<string, Data> m_PoolBgm = new Dictionary<string, Data>();
	private Dictionary<string, Data> m_PoolSe = new Dictionary<string, Data>();

	//	初期化
	protected override void Init ()
	{
		m_SourceBgm = gameObject.AddComponent<AudioSource>();
		m_SourceSe = gameObject.AddComponent<AudioSource>();
	}

	//  AudioSourceを取得する
	private AudioSource UseAudioSource(Type type)
	{
		if(type == Type.Bgm)
		{
			return m_SourceBgm;
		}
		else
		{
			return m_SourceSe;
		}
	}

	// BGMの読み込み
	public void LoadBgm(string key, string resourceName)
	{
		if (m_PoolBgm.ContainsKey(key))
		{
			m_PoolBgm.Remove(key);
		}
		m_PoolBgm.Add(key, new Data(key, resourceName));
	}

	// SEの読み込み
	public void LoadSe(string key, string resourceName)
	{
		if (m_PoolSe.ContainsKey(key))
		{
			m_PoolSe.Remove(key);
		}
		m_PoolSe.Add(key, new Data(key, resourceName));
	}

	//  BGMの再生
	public bool PlayBgm(string key) {
		if(m_PoolBgm.ContainsKey(key) == false) { return false; }
		StopBgm();
		AudioSource source = UseAudioSource(Type.Bgm);
		source.loop = true;
		source.clip = m_PoolBgm[key].Clip;
		source.Play();
		return true;
	}

	//  SEの再生
	public bool PlaySe(string key)
	{
		if(m_PoolSe.ContainsKey(key) == false) { return false; }
		AudioSource source = UseAudioSource(Type.Se);
		source.PlayOneShot(m_PoolSe[key].Clip);
		return true;
	}

	//  BGMの停止
	public bool StopBgm()
	{
		UseAudioSource(Type.Bgm).Stop();
		return true;
	}
}