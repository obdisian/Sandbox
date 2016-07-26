using UnityEngine;
using System.Collections;

public class GoalEffect : MonoBehaviour {

	int counter = 0;
	bool isTurn = false;

	GameObject player;
	GameObject goal;

	public Shader goalEffectShader;
	Material mat;

	float extend;	//	0.0~0.3

	void Awake () {
		player = GameObject.Find ("Player");
		goal = GameObject.Find ("Goal");
		mat = new Material(goalEffectShader);
	}


	void OnRenderImage (RenderTexture source, RenderTexture destination) {

		if (!isTurn) {
			extend = Mathf.Lerp (extend, 0.3f, 0.1f);
			if (Mathf.Abs (0.3f - extend) < 0.3f*0.2f) {
				counter++;
				if (counter > 30) {
					counter = 0;
					isTurn = true;
				}
			}
		} else {
			extend = Mathf.Lerp (extend, 0.0f, 0.1f);

			if (extend < 0.1f) {
				player.transform.localScale = Vector2.zero;
				goal.transform.localScale = Vector2.zero;
			}
		}

		//	エフェクト発生点
		mat.SetVector ("_Center", MainCamera.mCamera.WorldToScreenPoint (goal.transform.position));

		//	エフェクトの拡張
		mat.SetFloat ("_Extend", extend);

		//	エフェクトの適応
		Graphics.Blit(source, destination, mat);
	}
}
