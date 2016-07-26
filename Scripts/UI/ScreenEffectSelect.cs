using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenEffectSelect : MonoBehaviour {

	public static bool isOpenScreenEffectPanel = false;

	public void Button_PanelOpen () {
		int count = 0;
		for (int i = 0; i < 15; i++) {
			if (100 == Score.mapScore [i]) {
				count++;
			}
		}
		if (count == 15) {
			isOpenScreenEffectPanel = true;
		} else {
			LockScreenEffectPanel.isOpen_LockScreenEffectPanel = true;
		}
	}

	public void Button_PanelClose () {
		isOpenScreenEffectPanel = false;
	}

	public void Button_SelectScreenEffect (int n) {
		selectShaderNum = n;
		mat.shader = sampleShaders [selectShaderNum];
	}


	public static int selectShaderNum = 0;

	Vector2 size;
	Slider slider, slider_2;
	static float sliderValue = 0.5f;
	static float sliderValue_2 = 0.5f;
	public Material mat;
	public Shader[] sampleShaders;

	Image[] selectImage = new Image[4];
	Text[] selectText = new Text[4];

	void Start () {
		//	シェーダーのセット
		mat.shader = sampleShaders [selectShaderNum];

		isOpenScreenEffectPanel = false;

		size = transform.localScale;
		slider = gameObject.transform.GetChild (1).gameObject.GetComponent<Slider> ();
		slider_2 = gameObject.transform.GetChild (2).gameObject.GetComponent<Slider> ();

		slider.value = sliderValue;
		slider_2.value = sliderValue_2;

		selectImage [0] = gameObject.transform.GetChild (3).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		selectImage [1] = gameObject.transform.GetChild (4).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		selectImage [2] = gameObject.transform.GetChild (5).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		selectImage [3] = gameObject.transform.GetChild (6).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();

		selectText [0] = gameObject.transform.GetChild (3).gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();
		selectText [1] = gameObject.transform.GetChild (4).gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();
		selectText [2] = gameObject.transform.GetChild (5).gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();
		selectText [3] = gameObject.transform.GetChild (6).gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();

		selectText [0].text = Info_StringText.effectText [0, Info_StringText.textLanguage];
		selectText [1].text = Info_StringText.effectText [1, Info_StringText.textLanguage];
		selectText [2].text = Info_StringText.effectText [2, Info_StringText.textLanguage];
		selectText [3].text = Info_StringText.effectText [3, Info_StringText.textLanguage];

		if (selectShaderNum != 0) {
			mat.SetFloat ("_Extend", 1.0f);
		}
	}

	void Update () {

		if (isOpenScreenEffectPanel) {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Middle), 0.1f);
		} else {
			transform.Lerp_Position (Mover.UBPosition (Mover.UiBasePos.Right), 0.1f);
		}

		if (selectShaderNum == 0 || selectShaderNum == 1) {
			slider.transform.localScale = Vector2.zero;
			slider_2.transform.localScale = Vector2.zero;
		} else {
			slider.transform.localScale = size * 1.5f;
			slider_2.transform.localScale = size * 1.5f;
		}

		for (int i = 0; i < 4; i++) {
			if (i == selectShaderNum) {
				selectImage [i].color = Color.Lerp (selectImage [i].color, Color.gray, 0.1f);
				selectText [i].color = Color.Lerp (selectText [i].color, Color.black, 0.1f);
			} else {
				selectImage [i].color = Color.Lerp (selectImage [i].color, Color.black, 0.1f);
				selectText [i].color = Color.Lerp (selectText [i].color, Color.white, 0.1f);
			}
		}

		sliderValue = slider.value;
		sliderValue_2 = slider_2.value;

		//	mosaic
		if (selectShaderNum == 2) {
			mat.SetFloat ("_Size_x", CustomImageEffect.shaderSetFloat_Size_x = Mover.RatioMap (slider.value, 0.0f, 1.0f, CustomImageEffect.min_Size, CustomImageEffect.max_Size));
			mat.SetFloat ("_Size_y", CustomImageEffect.shaderSetFloat_Size_y = Mover.RatioMap (slider_2.value, 0.0f, 1.0f, CustomImageEffect.min_Size, CustomImageEffect.max_Size));
		}

		//	wave
		else if (selectShaderNum == 3) {
			mat.SetFloat ("_Speed", CustomImageEffect.shaderSetFloat_Speed = Mover.RatioMap (slider.value, 0.0f, 1.0f, CustomImageEffect.min_Speed, CustomImageEffect.max_Speed));
			mat.SetFloat ("_Pow", CustomImageEffect.shaderSetFloat_Pow = Mover.RatioMap (slider_2.value, 0.0f, 1.0f, CustomImageEffect.min_Pow, CustomImageEffect.max_Pow));
		}
	}
}
