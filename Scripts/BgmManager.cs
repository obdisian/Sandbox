using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BgmManager : MonoBehaviour
{

	#region Singleton

	private static BgmManager instance;

	public static BgmManager Instance {
		get {
			if (instance == null) {
				instance = (BgmManager)FindObjectOfType (typeof(BgmManager));

				if (instance == null) {
					Debug.LogError (typeof(BgmManager) + "is nothing");
				}
			}

			return instance;
		}
	}

	#endregion Singleton

	public bool DebugMode = true;
	[Range (0f, 1f)]
	public float TargetVolume = 1.0f;
	public float TimeToFade = 2.0f;
	[Range (0f, 1f)]
	public float CrossFadeRatio = 1.0f;
	[NonSerialized]
	public AudioSource CurrentAudioSource = null;

	public AudioSource SubAudioSource {
		get { 
			if (this.AudioSources == null)
				return null;
			foreach (AudioSource s in this.AudioSources) {
				if (s != this.CurrentAudioSource) {
					return s;
				}
			}
			return null;
		}
	}

	private List<AudioSource> AudioSources = null;
	private Dictionary<string,AudioClip> AudioClipDict = null;
	private IEnumerator fadeOutCoroutine;
	private IEnumerator fadeInCoroutine;

	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this.gameObject);
			return;
		}
		DontDestroyOnLoad (this.gameObject);

		this.AudioSources = new List<AudioSource> ();
		this.AudioSources.Add (this.gameObject.AddComponent<AudioSource> ());
		this.AudioSources.Add (this.gameObject.AddComponent<AudioSource> ());
		foreach (AudioSource s in this.AudioSources) {
			s.playOnAwake = false;
			s.volume = 0f;
			s.loop = true;
		}

		this.AudioClipDict = new Dictionary<string, AudioClip> ();
		foreach (AudioClip bgm in Resources.LoadAll<AudioClip>("Audio/BGM")) {
			this.AudioClipDict.Add (bgm.name, bgm);
		}

		if (FindObjectsOfType (typeof(AudioListener)).All (o => !((AudioListener)o).enabled)) {
			this.gameObject.AddComponent<AudioListener> ();
		}
	}

	public void OnGUI ()
	{
		if (this.DebugMode) {
			if (this.AudioClipDict.Count == 0) {
				GUI.Box (new Rect (10, 10, 200, 50), "BGM Manager(Debug Mode)");
				GUI.Label (new Rect (10, 35, 80, 20), "Audio clips not found.");
				return;
			}

			GUI.Box (new Rect (10, 10, 200, 150 + this.AudioClipDict.Count * 25), "BGM Manager(Debug Mode)");
			int i = 0;
			GUI.Label (new Rect (20, 30 + i++ * 20, 180, 20), "Target Volume : " + this.TargetVolume.ToString ("0.00"));
			GUI.Label (new Rect (20, 30 + i++ * 20, 180, 20), "Time to Fade : " + this.TimeToFade.ToString ("0.00"));
			GUI.Label (new Rect (20, 30 + i++ * 20, 180, 20), "Crossfade Ratio : " + this.CrossFadeRatio.ToString ("0.00"));

			i = 0;
			foreach (AudioClip bgm in this.AudioClipDict.Values) {
				bool currentBgm = (this.CurrentAudioSource != null && this.CurrentAudioSource.clip == this.AudioClipDict [bgm.name]);

				if (GUI.Button (new Rect (20, 100 + i * 25, 40, 20), "Play")) {
					this.Play (bgm.name);
				}
				string txt = string.Format ("[{0}] {1}", currentBgm ? "X" : "_", bgm.name);
				GUI.Label (new Rect (70, 100 + i * 25, 1000, 20), txt);

				i++;
			}

			if (GUI.Button (new Rect (20, 100 + i++ * 25, 180, 20), "Stop")) {
				this.Stop ();
			}
			if (GUI.Button (new Rect (20, 100 + i++ * 25, 180, 20), "Stop Immediately")) {
				this.StopImmediately ();
			}
		}
	}

	public void Play (string bgmName)
	{
		if (!this.AudioClipDict.ContainsKey (bgmName)) {
			Debug.LogError (string.Format ("BGM名[{0}]が見つかりません。", bgmName));  
			return;
		}

		if ((this.CurrentAudioSource != null)
			&& (this.CurrentAudioSource.clip == this.AudioClipDict [bgmName])) {
			return;
		}

		if (!TitleSelectProgression.bgmEnable) {
			return;
		}

		stopFadeOut ();
		stopFadeIn ();

		this.Stop ();

		float fadeInStartDelay = this.TimeToFade * (1.0f - this.CrossFadeRatio);

		this.CurrentAudioSource = this.SubAudioSource;
		this.CurrentAudioSource.clip = this.AudioClipDict [bgmName];
		this.fadeInCoroutine = fadeIn (this.CurrentAudioSource, this.TimeToFade, this.CurrentAudioSource.volume, this.TargetVolume, fadeInStartDelay);
		StartCoroutine (this.fadeInCoroutine);
	}

	public void Stop ()
	{
		if (this.CurrentAudioSource != null) {
			this.fadeOutCoroutine = fadeOut (this.CurrentAudioSource, this.TimeToFade, this.CurrentAudioSource.volume, 0f);
			StartCoroutine (this.fadeOutCoroutine);
		}
	}

	public void StopImmediately ()
	{
		this.fadeInCoroutine = null;
		this.fadeOutCoroutine = null;
		foreach (AudioSource s in this.AudioSources) {
			s.Stop ();
		}
		this.CurrentAudioSource = null;
	}

	private IEnumerator fadeIn (AudioSource bgm, float timeToFade, float fromVolume, float toVolume, float delay)
	{
		if (delay > 0) {
			yield return new WaitForSeconds (delay);
		}


		float startTime = Time.time;
		bgm.Play ();
		while (true) {
			float spentTime = Time.time - startTime;
			if (spentTime > timeToFade) {
				bgm.volume = toVolume;
				this.fadeInCoroutine = null;
				break;
			}

			float rate = spentTime / timeToFade;
			float vol = Mathf.Lerp (fromVolume, toVolume, rate);
			bgm.volume = vol;
			yield return null;
		}
	}

	private IEnumerator fadeOut (AudioSource bgm, float timeToFade, float fromVolume, float toVolume)
	{ 
		float startTime = Time.time;
		while (true) {
			float spentTime = Time.time - startTime;
			if (spentTime > timeToFade) {
				bgm.volume = toVolume;
				bgm.Stop ();
				this.fadeOutCoroutine = null;
				break;
			}

			float rate = spentTime / timeToFade;
			float vol = Mathf.Lerp (fromVolume, toVolume, rate);
			bgm.volume = vol;
			yield return null;
		}
	}

	private void stopFadeIn ()
	{
		if (this.fadeInCoroutine != null)
			StopCoroutine (this.fadeInCoroutine);
		this.fadeInCoroutine = null;

	}

	private void stopFadeOut ()
	{
		if (this.fadeOutCoroutine != null)
			StopCoroutine (this.fadeOutCoroutine);
		this.fadeOutCoroutine = null;
	}
}