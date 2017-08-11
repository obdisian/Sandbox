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

	private Fade fade = Fade.None;
	private string nextSceneName = "";
	private float param = 0.0f;

	[SerializeField]
	private float speed = 0.05f;


	//	フェードイメージ
	private Image image;



	//	初期化処理
	protected override void Init ()
	{
		//	キャンバスを設定
		Canvas canvas = gameObject.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvas.sortingOrder = 999;

		//	キャンバスのスケール設定
		CanvasScaler scaler = gameObject.AddComponent<CanvasScaler> ();
		scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;


		//	フェードイメージの作成
		GameObject fadeObject = new GameObject ("Image");
		fadeObject.transform.parent = transform;

		//	イメージコンポーネント設定
		image = fadeObject.AddComponent<Image> ();
		image.rectTransform.offsetMin = Vector2.zero;
		image.rectTransform.offsetMax = Vector2.zero;
		image.rectTransform.anchorMin = Vector2.zero;
		image.rectTransform.anchorMax = Vector2.one;
		image.rectTransform.pivot = Vector2.one * 0.5f;
		image.sprite = Resources.Load<Sprite> ("Sprite/Fade/Image" + Random.Range (0, 3));
		image.material = new Material (Shader.Find ("Custom/Fade"));
		image.raycastTarget = false;
	}

	//	更新処理
	private void Update ()
	{
		switch(fade)
		{
		case Fade.None:
			break;

		case Fade.In:

			param -= speed;
			image.material.SetFloat("_Param", param);

			if (param <= 0.0f) {
				param = 0.0f;
				fade = Fade.None;
			}
			break;

		case Fade.Out:

			param += speed;
			image.material.SetFloat("_Param", param);

			if (param >= 1.0f) {
				param = 1.0f;
				SceneManager.LoadScene (nextSceneName);
				fade = Fade.In;
			}
			break;
		}
	}

	//	シーンの読み込み
	public void LoadScene (string sceneName)
	{
		if (fade == Fade.None) {
			fade = Fade.Out;
			nextSceneName = sceneName;
		}
	}
}
