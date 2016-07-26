using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FirstClearGraph : MonoBehaviour {

	Image[] graphImage = new Image[15];

	void Start () {
		for (int i = 0; i < 15; i++) {
			graphImage [i] = transform.GetChild (i).GetComponent<Image> ();
			graphImage [i].fillAmount = 0;

			Color c = i == 0 ? new Color (106, 255, 0, 255) / 255 : TitleSelectProgression.sgColor [i - 1];
			c /= 0.1f;
			c.a = 1;
			graphImage [i].color = Color.white;
		}
	}
	
	void Update () {

		for (int i = 0; i < 15; i++) {
			if (SubOptionPanel.openFirstClearPanel) {
				graphImage [i].fillAmount =
					Mathf.Lerp (graphImage [i].fillAmount,
						Mover.RatioMap (SaveData.firstClearCount [i], 0, BoxGraphBasePoint.maxPlayCount, 0.0f, 1.0f), 0.05f);
			} else {
				graphImage [i].fillAmount = Mathf.Lerp (graphImage [i].fillAmount, 0, 0.1f);
			}
		}
	}
}
