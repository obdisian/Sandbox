using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//	シーン遷移管理クラス（キャンバスに付与する）
public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
	public enum Fade
	{
		None,
		In,
		Out,
	}
	private Fade mFade;
	private string mNextSceneName;

	public void LoadScene (string sceneName)
	{
		if (mFade == Fade.None) {
			mFade = Fade.Out;
			mNextSceneName = sceneName;
		}
	}





	private Image image;
	private float param;

	private void Start ()
	{
		image = transform.GetChild (0).GetComponent<Image> ();

		param = 0.0f;
		mFade = Fade.None;
		mNextSceneName = "";
	}
	
	private void Update ()
	{

		switch(mFade)
		{
		case Fade.None:
			break;

		case Fade.In:

			param -= 0.02f;
			image.material.SetFloat("_Param", param);

			if (param <= 0.0f) {
				param = 0.0f;
				mFade = Fade.None;
			}
			break;

		case Fade.Out:

			param += 0.02f;
			image.material.SetFloat("_Param", param);

			if (param >= 1.0f) {
				param = 1.0f;
				SceneManager.LoadScene (mNextSceneName);
				mFade = Fade.In;
			}
			break;
		}
	}
}
