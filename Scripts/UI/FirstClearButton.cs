using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FirstClearButton : MonoBehaviour {

	Image image;
	Image childImage;
	Text childText;

	void Start () {
		image = GetComponent<Image> ();
		childImage = transform.GetChild (0).GetComponent<Image> ();
		childText = transform.GetChild (1).GetComponent<Text> ();

		childImage.color = Color.clear;
	}
	
	void Update () {
		if (SubOptionPanel.openFirstClearPanel) {
			image.color = Color.Lerp (image.color, Color.black, 0.1f);
			childImage.color = Color.Lerp (childImage.color, Color.yellow, 0.1f);
			childText.color = Color.Lerp (childText.color, Color.black, 0.1f);
		} else {
			image.color = Color.Lerp (image.color, Color.white, 0.1f);
			childImage.color = Color.Lerp (childImage.color, Color.clear, 0.1f);
			childText.color = Color.Lerp (childText.color, Color.white, 0.1f);
		}
	}

	public void OnFirstClearButton () {
		SubOptionPanel.openFirstClearPanel = !SubOptionPanel.openFirstClearPanel;
	}
}
