using UnityEngine;
using System.Collections;

public class RectParticle : MonoBehaviour {

	Vector3 vel;
	Color color;
	SpriteRenderer sRenderer;

	void Start () {
		sRenderer = GetComponent<SpriteRenderer> ();
		color = sRenderer.color;

		if (gameObject.tag == "CrushEffect") {
			transform.localScale = Vector2.one * 0.1f;
		}
		else if (gameObject.tag == "RectEffect") {
			//	速度の設定
			vel = new Vector3 (Random.Range (-1.0f, 1.0f), Random.Range (-1.0f, 1.0f), 0) * 0.3f;

//			sRenderer.color = new Color (Random.value, Random.value, Random.value);
		}
	}
	
	void Update () {
		if (gameObject.tag == "CrushEffect") {
			transform.Lerp_LocalScale (Vector2.one * 2.1f, 0.075f);

			color.a *= 0.925f;
			sRenderer.color = color;

			if (transform.localScale.x > 2) {
				Destroy (gameObject);
			}
		}
		else if (gameObject.tag == "RectEffect") {
			vel *= 0.975f;
			transform.position += vel;
			transform.rotation = Quaternion.AngleAxis (10, Vector3.forward) * transform.rotation;
			transform.localScale *= 0.975f;

			if (transform.localScale.x < 0.01f) {
				Destroy (gameObject);
			}
		}
	}
}
