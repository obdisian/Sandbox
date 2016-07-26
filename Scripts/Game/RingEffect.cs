using UnityEngine;
using System.Collections;

public class RingEffect : MonoBehaviour {

	SpriteRenderer sRenderer;
	Color color;
	int count = 0;

	public void Init (Color color) {
		sRenderer = GetComponent<SpriteRenderer> ();
		sRenderer.color = this.color = color;
	}

	void Start () {
//		transform.localScale = color == Color.green || color == Color.red ? Vector2.zero : Vector2.one;
		transform.localScale = Vector2.zero;
	}
	
	void Update () {
//		if (color == Color.green || color == Color.red) {
//			transform.Lerp_LocalScale (Vector3.one, 0.1f);
//			sRenderer.color = new Color (color.r, color.g, color.b, 1.05f - transform.localScale.x);
//		} else {
//			transform.Lerp_LocalScale (Vector3.zero, 0.1f);
//			sRenderer.color = new Color (color.r, color.g, color.b, 1.05f - transform.localScale.x);
//		}
		transform.Lerp_LocalScale (Vector3.one, 0.15f);
		sRenderer.color = new Color (color.r, color.g, color.b, 1.05f - transform.localScale.x);



		if (Mathf.Abs (transform.localScale.x - Vector3.one.x) < 0.01f) {
			Destroy (gameObject);
		}

		count++;
		if (count > 180) {
			Destroy (gameObject);
		}
	}
}
