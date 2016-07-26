using UnityEngine;
using System.Collections;

public class Anime_Tex : MonoBehaviour {

	GameObject player;

	SpriteRenderer sRenderer;
	Sprite [] sprites = new Sprite[9];

	int count = 0;
	int animTimer = 0;

	void Start () {

		sRenderer = GetComponent<SpriteRenderer> ();

		//	スキン1~4まで
		string [,] str = {
			{ "s1_run", "s1_jump" },
			{ "s2_run", "s2_jump" },
			{ "s3_run", "s3_jump" },
			{ "s4_run", "s4_jump" },
		};

		Sprite [] sp = Resources.LoadAll<Sprite> ("PlayerTex/" + str [(int)Player.skinType, 0]);
		for (int i = 0; i < sp.Length; i++) { sprites [i] = sp [i]; }
		sprites [8] = Resources.Load<Sprite> ("PlayerTex/" + str [(int)Player.skinType, 1]);
		sRenderer.sprite = sprites [0];

		player = GameObject.Find ("Player");
	}
	
	void Update () {

		transform.position = player.transform.position;
		transform.localScale = new Vector2 (-player.transform.localScale.x, player.transform.localScale.y)/3;
		transform.rotation = player.transform.rotation;


		//	ポーズ中はアニメーションしない
		if (GameProgression.isPause) {
			return;
		}

		count++;
		if (count % AnimeFrame_Debug.frame == 0) {
			animTimer++;
			if (animTimer >= 8) {
				animTimer = 0;
			}
		}

		if (Player.state == Player.State.Walk) {
			sRenderer.sprite = sprites [animTimer];
		} else {
			sRenderer.sprite = sprites [8];
		}
	}
}
