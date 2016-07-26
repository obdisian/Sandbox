using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Help_Jump : MonoBehaviour {

	Transform owner;
	Image childTapImage;
	Image[] childImage = new Image[2];
	Color wClear = new Color (1,1,1,0);

	void Start () {
		owner = transform;

		childTapImage = transform.GetChild (0).GetComponent<Image> ();

		childImage [0] = transform.GetChild (1).GetComponent<Image> ();
		childImage [1] = transform.GetChild (2).GetComponent<Image> ();

//		childImage [0].transform.localScale = Vector2.zero;
//		childImage [1].transform.localScale = Vector2.zero;
//		childImage [0].transform.rotation = Quaternion.AngleAxis (181, Vector3.forward);
//		childImage [1].transform.rotation = Quaternion.AngleAxis (179, Vector3.forward);

		childImage [0].color = Color.clear;
		childImage [1].color = Color.clear;
	}
	
	void LateUpdate () {
		if (TitleSelectProgression.helpEnable && HelpPanel.isOnhOpenFrag) {
			if (HelpPanel.hOpenFrag == HelpPanel.HelpOpenFrag.Help_1) {

				childTapImage.transform.Lerp_Position (owner.position + Vector3.left * Screen.width/5, 0.2f);

				childImage [0].transform.position = owner.position;
				childImage [1].transform.position = owner.position + Vector3.right * Screen.width/5;
				childImage [0].color = Color.Lerp (childImage [0].color, Color.white, 0.1f);
				childImage [1].color = Color.Lerp (childImage [1].color, Color.white, 0.1f);

//				childImage [0].transform.Lerp_LocalScale (Vector2.one, 0.1f);
//				childImage [1].transform.Lerp_LocalScale (Vector2.one, 0.1f);
//				childImage [0].transform.Lerp_Rotation (Quaternion.AngleAxis (0, Vector3.forward), 0.1f);
//				childImage [1].transform.Lerp_Rotation (Quaternion.AngleAxis (0, Vector3.forward), 0.1f);
				} else {

				childTapImage.transform.Lerp_Position (owner.position, 0.2f);

				childImage [0].transform.position = owner.position;
				childImage [1].transform.position = owner.position;

//				childImage [0].transform.localScale = Vector2.zero;
//				childImage [1].transform.localScale = Vector2.zero;
//				childImage [0].transform.rotation = Quaternion.AngleAxis (181, Vector3.forward);
//				childImage [1].transform.rotation = Quaternion.AngleAxis (179, Vector3.forward);

				childImage [0].color = wClear;
				childImage [1].color = wClear;
			}
		}
	}
}
