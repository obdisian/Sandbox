using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using animate;
public class Countdown : MonoBehaviour
{
	IEnumerator Start ()
	{
		var text = GetComponent<Text> ();
		text.text = "3";
		transform.SetEasing (Target.LocalScale, Easing.Type.Cubic, Easing.Ease.In, Vector3.one * 5, Vector3.zero, 0.04f);
		yield return new WaitForSeconds (1.0f);

		text.text = "2";
		transform.SetEasing (Target.LocalScale, Easing.Type.Cubic, Easing.Ease.In, Vector3.one * 5, Vector3.zero, 0.04f);
		yield return new WaitForSeconds (1.0f);

		text.text = "1";
		transform.SetEasing (Target.LocalScale, Easing.Type.Cubic, Easing.Ease.In, Vector3.one * 5, Vector3.zero, 0.04f);
		yield return new WaitForSeconds (1.0f);

		text.text = "Start!!!";
		var easing = transform.SetEasing (Target.LocalScale, Easing.Type.Cubic, Easing.Ease.Out, transform.localScale, Vector3.one * 5, 0.05f);

		easing.DeleteAction = () => { Destroy(gameObject); };
	}
}
