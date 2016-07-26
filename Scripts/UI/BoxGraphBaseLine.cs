using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BoxGraphBaseLine : MonoBehaviour {

	public static float text100_y;
	public static float text50_y;
	public static float text0_y;

	Text text;

	void Start () {
		text = GetComponent <Text> ();
	}

	void Update () {

		if (gameObject.name == "Text100") {
			text100_y = transform.position.y;
			text.text = "" + BoxGraphBasePoint.maxPlayCount;
		}
		else if (gameObject.name == "Text50") {
			text50_y = transform.position.y;

			text.text = "" + BoxGraphBasePoint.maxPlayCount/2;
		}
		else if (gameObject.name == "Text0") {
			text0_y = transform.position.y;
		}
	}
}
