using UnityEngine;
using System.Collections;

public class ColorChangeFlag : MonoBehaviour {

	public GameProgression.ColorFlag colorFlag;

	void Start () {
		if (colorFlag == GameProgression.ColorFlag.Red) {
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("ColorBlockTex/Color_R_trg");
		}
		else if (colorFlag == GameProgression.ColorFlag.Blue) {
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("ColorBlockTex/Color_B_trg");
		}
		else if (colorFlag == GameProgression.ColorFlag.Green) {
			GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("ColorBlockTex/Color_G_trg");
		}
	}
}
