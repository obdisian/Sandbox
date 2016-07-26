using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkinButton : MonoBehaviour {

	public static bool isOpenDescriptionPanel;
	public static int selectedSkin;
	GameObject descriptionPanel;
	Text descriptionText;

	Image lockImage;
	Color lockColor = new Color (1, 0, 0, 0.8f);

	Image[] image = new Image[4];

	void Start () {
		image [0] = gameObject.transform.GetChild (0).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		image [1] = gameObject.transform.GetChild (1).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		image [2] = gameObject.transform.GetChild (2).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();
		image [3] = gameObject.transform.GetChild (3).gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ();

		//	説明パネル
		descriptionPanel = gameObject.transform.GetChild (5).gameObject;
		Image descriptionImage = descriptionPanel.GetComponent<Image> ();
		Image descriptionFlame = descriptionPanel.transform.GetChild (0).GetComponent<Image> ();
		descriptionText = descriptionPanel.transform.GetChild (1).GetComponent<Text> ();
		selectedSkin = 0;

		//	自動伸縮
		RectTransform[] rect = new RectTransform[3];
		rect [0] = descriptionPanel.GetComponent<RectTransform> ();
		rect [1] = descriptionFlame.GetComponent<RectTransform> ();
		rect [2] = descriptionText.GetComponent<RectTransform> ();
		int adder = Info_StringText.textLanguage == 1 ? 1 : 2;	//	日本語の時は全角な為、幅を倍にする
		Vector2 whSize = new Vector2 (Info_StringText.skinDescriptionText_3 [Info_StringText.textLanguage].Length * (9.5f*adder), rect [0].sizeDelta.y);
		rect [0].sizeDelta = whSize;
		rect [1].sizeDelta = new Vector2 (whSize.x - 10, whSize.y - 10);
		rect [2].sizeDelta = whSize;

		//	ロックエフェクト
		lockImage = gameObject.transform.GetChild (6).GetComponent<Image> ();
	}
	
	void Update () {

		for (int i = 0; i < 4; i++) {
			if ((int)Player.skinType == i) {
				image [i].color = Color.Lerp (image [i].color, Color.gray, 0.1f);
			} else {
				image [i].color = Color.Lerp (image [i].color, Color.black, 0.1f);
			}
		}

		if (isOpenDescriptionPanel) {
			if (Input.GetMouseButton (0)) {
				isOpenDescriptionPanel = false;
			}
			descriptionPanel.transform.localScale = Vector2.one;

			lockImage.transform.localScale = Vector2.one;
			lockImage.color = Color.Lerp (lockImage.color, Color.clear, 0.025f);

			if (selectedSkin == 1) {
				descriptionText.text = Info_StringText.skinDescriptionText_1 [Info_StringText.textLanguage];
				lockImage.transform.position = image [1].transform.position;
			}else if (selectedSkin == 2) {
				descriptionText.text = Info_StringText.skinDescriptionText_2 [Info_StringText.textLanguage];
				lockImage.transform.position = image [2].transform.position;
			} else if (selectedSkin == 3) {
				descriptionText.text = Info_StringText.skinDescriptionText_3 [Info_StringText.textLanguage];
				lockImage.transform.position = image [3].transform.position;
			}
		} else {
			descriptionPanel.transform.localScale = Vector2.zero;
			lockImage.transform.localScale = Vector2.zero;
			lockImage.color = lockColor;
		}
	}
}
