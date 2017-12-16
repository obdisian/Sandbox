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

	private Fade m_Fade = Fade.None;
	public Fade FadeState { get { return m_Fade; } }

	private string m_NextSceneName = "";
	private float m_Param = 0.0f;

	[SerializeField]
	private float m_Speed = 0.05f;


	//	フェードイメージ
	private Image m_Image;


	//	シーンが切り替わる前に呼びたい処理を登録する
	public delegate void Delegate ();
	public Delegate CallBackChangeScene = delegate { };


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
		m_Image = fadeObject.AddComponent<Image> ();
		m_Image.rectTransform.offsetMin = Vector2.zero;
		m_Image.rectTransform.offsetMax = Vector2.zero;
		m_Image.rectTransform.anchorMin = Vector2.zero;
		m_Image.rectTransform.anchorMax = Vector2.one;
		m_Image.rectTransform.pivot = Vector2.one * 0.5f;
		m_Image.sprite = Resources.Load<Sprite> ("Sprite/Fade/Image" + Random.Range (0, 3));
		m_Image.material = new Material (Shader.Find ("Custom/Fade"));
		m_Image.raycastTarget = false;
	}

	//	更新処理
	private void Update ()
	{
		switch(m_Fade)
		{
		case Fade.None:
			break;

		case Fade.In:

			m_Param -= m_Speed;
			m_Image.material.SetFloat("_Param", m_Param);

			if (m_Param <= 0.0f) {
				m_Param = 0.0f;
				m_Fade = Fade.None;
			}
			break;

		case Fade.Out:

			m_Param += m_Speed;
			m_Image.material.SetFloat("_Param", m_Param);

			if (m_Param >= 1.0f) {
				m_Param = 1.0f;
				m_Fade = Fade.In;

				CallBackChangeScene ();
				SceneManager.LoadScene (m_NextSceneName);
			}
			break;
		}
	}

	//	シーンの読み込み
	public void LoadScene (string sceneName)
	{
		if (m_Fade == Fade.None) {
			m_Fade = Fade.Out;
			m_NextSceneName = sceneName;
		}
	}
}
