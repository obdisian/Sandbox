using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class Player : Mover {
    private GameObject stageManager;
    private GameObject mainCamera;
	private GameObject holdPlanet = null;
	public GameObject GetHoldPlanet () { return holdPlanet; }
	
	private Quaternion baseRot = Quaternion.identity;

	private float speed = 40;
	private const float GRAVITY = 0.98f;

	void Start () {
		pos = transform.position;
		velocity = Vector3.zero;
		velocity = new Vector3 (0, 1, 0);
        stageManager = GameObject.Find("StageManager");
		mainCamera = GameObject.Find ("Main Camera");
	}
	
	void Update () {
		count++;
		transform.position = pos;
		pos += velocity;

        foreach (GameObject p in stageManager.GetComponent<StageManager> ().planets) {
			float l = Length (pos - p.transform.position);
			Vector3 n = Normalize (pos - p.transform.position);
			float r = (p.transform.localScale.x + transform.localScale.x) / 2;

			if (l > r) {
				if (holdPlanet == null) {
                    velocity += (p.transform.position - pos) * (/*p.transform.localScale.x * */GRAVITY / stageManager.GetComponent<StageManager> ().planets.Count * 5) / (l * l);
					transform.rotation = Quaternion.LookRotation (pos + velocity - pos, Vector3.up) * RotationZ (count * 0.01f);
					mainCamera.transform.rotation = Quaternion.Lerp (mainCamera.transform.rotation, baseRot * RotationX (0.1f), 0.025f);
				}
			}
			else{
				if (holdPlanet != p) {
					holdPlanet = p;
					velocity = Vector3.zero;
					pos = holdPlanet.transform.position + n * r;
					//pos = holdPlanet.transform.position + baseRot * Vector3.up * r;
					transform.rotation = Quaternion.FromToRotation (AxisY(), n) * transform.rotation;
					baseRot = Quaternion.FromToRotation (AxisY (), n) * transform.rotation;
				}
			}
		}

		if (holdPlanet) {
			velocity = Vector3.zero;
			Vector3 cameraZ = mainCamera.GetComponent<MainCamera> ().AxisZ ();
			float r = (holdPlanet.transform.localScale.x + transform.localScale.x) / 2;

			Vector3 y = Normalize(pos - holdPlanet.transform.position);
			Vector3 x = Vector3.Cross(y, cameraZ);
			Vector3 z = Vector3.Cross(x, y);
            //Vector3 d = Direction () * speed / holdPlanet.transform.localScale.x;
            Vector3 d = !Input.GetMouseButton (0) ? Direction () * speed / holdPlanet.transform.localScale.x :
                VirtualPad.axis * speed / holdPlanet.transform.localScale.x;

			Quaternion rotToAxisY = Quaternion.FromToRotation (Vector3.up, y);
            Quaternion rotToAxisXZ = Quaternion.FromToRotation (rotToAxisY * Vector3.forward, z * d.y + x * d.x);
            Quaternion rot = rotToAxisXZ * rotToAxisY;

			baseRot = (Quaternion.AngleAxis (d.y, x) * Quaternion.AngleAxis (-d.x, z)) * baseRot;
			pos = holdPlanet.transform.position + baseRot * Vector3.up * r;
			mainCamera.transform.rotation = Quaternion.Lerp (mainCamera.transform.rotation, baseRot * RotationX (0.1f), 0.075f);
			if (d.x != 0 || d.y != 0) {
				transform.rotation = Quaternion.Lerp (transform.rotation, rot, 0.1f) * RotationZ (Mathf.Sin (count * 0.1f) * 0.01f);
			}

            if (Input.GetKeyDown (KeyCode.Space) || Input.GetTouch (1).phase == TouchPhase.Began) {
				velocity += AxisY ();
				holdPlanet = null;
			}
		}
	}
}
