using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeScreen : MonoBehaviour {

	float speed;
	string sceneName;
	Color color;

	public void Init (string name, float _speed, Color _color) {
		speed = _speed;
		sceneName = name;
		color = _color;
	}

	void Start () {
		//	遷移開始は0〜1、終わりは1〜0
		color.a = SceneTransition.sceneState == SceneTransition.SceneState.StartTransition ? 0 : 1.0f;
		GetComponent<Image> ().color = color;
	}
	
	void Update () {

		//	color.aを、StartTransition時は0〜1に、EndTransition時は1〜0に
		color.a = Mathf.Lerp (color.a, SceneTransition.sceneState == SceneTransition.SceneState.StartTransition ? 1 : 0, 0.1f * speed);

		GetComponent<Image> ().color = color;


		//	殴り書き。そのうち書き直そう
		if (sceneName == "" && color.a <= 0.01f) {
			if (SceneTransition.sceneState == SceneTransition.SceneState.EndTransition) {
				SceneTransition.sceneState = SceneTransition.SceneState.StartTransition;
				Destroy (gameObject);
			}
		}
		else if (color.a >= 0.99f) {

			if (SceneTransition.sceneState == SceneTransition.SceneState.StartTransition) {
				SceneTransition.sceneState = SceneTransition.SceneState.EndTransition;
				SceneManager.LoadScene (sceneName);
			}
		}
	}
}
