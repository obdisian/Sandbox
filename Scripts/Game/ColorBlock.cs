
using UnityEngine;
using System.Collections;

public class ColorBlock : MonoBehaviour {

	public GameProgression.ColorFlag colorFlag;

	BoxCollider2D boxCol;
	SpriteRenderer sRenderer;

	Sprite[] tex = new Sprite[2];

	GameProgression.ColorFlag pColorFrag;

	void Start () {
		pColorFrag = GameProgression.ColorFlag.Green;

		boxCol = GetComponent<BoxCollider2D> ();
		sRenderer = GetComponent<SpriteRenderer> ();
		if (colorFlag == GameProgression.ColorFlag.Red) {
			tex [0] = Resources.Load<Sprite> ("ColorBlockTex/Color_R");
			tex [1] = Resources.Load<Sprite> ("ColorBlockTex/Color_R_d");
//			sRenderer.color = new Color (255, 0, 59, 255) / 255;
		}
		else if (colorFlag == GameProgression.ColorFlag.Blue) {
			tex [0] = Resources.Load<Sprite> ("ColorBlockTex/Color_B");
			tex [1] = Resources.Load<Sprite> ("ColorBlockTex/Color_B_d");
//			sRenderer.color = new Color (0, 145, 255, 255) / 255;
		}
		else if (colorFlag == GameProgression.ColorFlag.Green) {
			tex [0] = Resources.Load<Sprite> ("ColorBlockTex/Color_G");
			tex [1] = Resources.Load<Sprite> ("ColorBlockTex/Color_G_d");
//			sRenderer.color = new Color (111, 255, 0, 255) / 255;
		}
	}
	
	void Update () {
		if (GameProgression.colorFlag == pColorFrag) {
			return;
		}
		pColorFrag = GameProgression.colorFlag;

		if (colorFlag != GameProgression.colorFlag) {
			boxCol.enabled = false;
			sRenderer.sprite = tex [1];
			sRenderer.color = new Color (1, 1, 1, 0.4f);
		} else {
			boxCol.enabled = true;
			sRenderer.sprite = tex [0];
			sRenderer.color = Color.white;
		}
	}
}
