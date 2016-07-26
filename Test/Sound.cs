using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Audio {
	
	const int SE_CHANNEL = 4;

	enum Type {
		Bgm,
		Se,
	}

	static Audio _singleton = null;
	public static Audio GetInstance()
	{
		return _singleton ?? (_singleton = new Audio());
	}

	GameObject _object = null;
	AudioSource _sourceBgm = null;
	AudioSource _sourceSeDefault = null;
	AudioSource[] _sourceSeArray;
	Dictionary<string, _Data> _poolBgm = new Dictionary<string, _Data>();
	Dictionary<string, _Data> _poolSe = new Dictionary<string, _Data>();

	class _Data {
		public string Key;
		public string ResName;
		public AudioClip Clip;

		public _Data(string key, string res) {
			Key = key;
			ResName = "Audio/" + res;
			Clip = Resources.Load(ResName) as AudioClip;
		}
	}

	public Audio() {
		_sourceSeArray = new AudioSource[SE_CHANNEL];
	}

	AudioSource _GetAudioSource(Type type, int channel=-1) {
		if(_object == null) {
			_object = new GameObject("Audio");
			GameObject.DontDestroyOnLoad(_object);
			_sourceBgm = _object.AddComponent<AudioSource>();
			_sourceSeDefault = _object.AddComponent<AudioSource>();
			for (int i = 0; i < SE_CHANNEL; i++)
			{
				_sourceSeArray[i] = _object.AddComponent<AudioSource>();
			}
		}

		if(type == Type.Bgm) {
			return _sourceBgm;
		}
		else {
			if (0 <= channel && channel < SE_CHANNEL)
			{
				return _sourceSeArray[channel];
			}
			else
			{
				return _sourceSeDefault;
			}
		}
	}

	public static void LoadBgm(string key, string resName) {
		GetInstance()._LoadBgm(key, resName);
	}
	public static void LoadSe(string key, string resName) {
		GetInstance()._LoadSe(key, resName);
	}
	void _LoadBgm(string key, string resName) {
		if (_poolBgm.ContainsKey(key))
		{
			_poolBgm.Remove(key);
		}
		_poolBgm.Add(key, new _Data(key, resName));
	}
	void _LoadSe(string key, string resName) {
		if (_poolSe.ContainsKey(key))
		{
			_poolSe.Remove(key);
		}
		_poolSe.Add(key, new _Data(key, resName));
	}

	public static bool PlayBgm(string key) {
		return GetInstance()._PlayBgm(key);
	}
	bool _PlayBgm(string key) {
		if(_poolBgm.ContainsKey(key) == false) {
			return false;
		}

		_StopBgm();

		_Data _data = _poolBgm[key];

		AudioSource source = _GetAudioSource(Type.Bgm);
		source.loop = true;
		source.clip = _data.Clip;
		source.Play();

		return true;
	}
	public static bool StopBgm() {
		return GetInstance()._StopBgm();
	}
	bool _StopBgm() {
		_GetAudioSource(Type.Bgm).Stop();

		return true;
	}

	public static bool PlaySe(string key, int channel=-1) {
		return GetInstance()._PlaySe(key, channel);
	}
	bool _PlaySe(string key, int channel=-1) {
		if(_poolSe.ContainsKey(key) == false) {
			return false;
		}

		_Data _data = _poolSe[key];

		if (0 <= channel && channel < SE_CHANNEL)
		{
			AudioSource source = _GetAudioSource(Type.Se, channel);
			source.clip = _data.Clip;
			source.Play();
		}
		else
		{
			AudioSource source = _GetAudioSource(Type.Se);
			source.PlayOneShot(_data.Clip);
		}

		return true;
	}
}