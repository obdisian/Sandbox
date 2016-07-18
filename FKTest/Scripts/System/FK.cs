using UnityEngine;
using System.Collections;


//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//
//	FK(フォワードキネマティクス)クラス
//
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
public class FK : MonoBehaviour {


	//++++++++++++++++++++++++++++++++++++++++
	//	親オブジェクト
	//++++++++++++++++++++++++++++++++++++++++
	public GameObject Owner { get; set; }


	//++++++++++++++++++++++++++++++++++++++++
	//	ローカル座標、回転
	//++++++++++++++++++++++++++++++++++++++++
	public Vector3 BasePos { get; set; }
	public Quaternion BaseQuat { get; set; }


	//++++++++++++++++++++++++++++++++++++++++
	//	パラメータ用角度
	//++++++++++++++++++++++++++++++++++++++++
	public Vector3 Angle { get; set; }
	public Vector3 BaseAngle { get; set; }


	//++++++++++++++++++++++++++++++++++++++++
	//	球面線形補間の割合t
	//++++++++++++++++++++++++++++++++++++++++
	public float SlerpT { get; set; }



	//================================================================================
	//	初期設定
	//================================================================================
	public void Setup (GameObject owner) {
		Owner = owner;
		BasePos = transform.position;
		BaseQuat = transform.rotation;
	}


	//================================================================================
	//	FK処理更新
	//================================================================================
	public void UpdateFK () {
		if (!Owner) { return; }

		Angle = Vector3.Lerp (Angle, BaseAngle, SlerpT);
		BaseQuat = xyzAngle(Owner.transform.rotation, Angle.x, Angle.y, Angle.z);

		transform.position = Owner.transform.position + Owner.transform.rotation * BasePos;
		transform.rotation = BaseQuat * Owner.transform.rotation;
	}


	//================================================================================
	//	Edit用FK処理更新
	//================================================================================
	public void EditUpdateFK () {
		if (!Owner) { return; }

		BaseQuat = xyzAngle(Owner.transform.rotation, BaseAngle.x, BaseAngle.y, BaseAngle.z);

		transform.position = Owner.transform.position + Owner.transform.rotation * BasePos;
		transform.rotation = BaseQuat * Owner.transform.rotation;
	}




	//********************************************************************************
	//	以下、計算補助関数
	//********************************************************************************
	public static Quaternion xAngle (Quaternion quat, float angle) {
		return Quaternion.AngleAxis (angle, quat * Vector3.right);
	}
	public static Quaternion yAngle (Quaternion quat, float angle) {
		return Quaternion.AngleAxis (angle, quat * Vector3.up);
	}
	public static Quaternion zAngle (Quaternion quat, float angle) {
		return Quaternion.AngleAxis (angle, quat * Vector3.forward);
	}

	public static Quaternion xyAngle (Quaternion quat, float angleX, float angleY) {
		return xAngle (quat, angleX) * yAngle (quat, angleY);
	}
	public static Quaternion yzAngle (Quaternion quat, float angleY, float angleZ) {
		return yAngle (quat, angleY) * zAngle (quat, angleZ);
	}
	public static Quaternion zxAngle (Quaternion quat, float angleZ, float angleX) {
		return zAngle (quat, angleZ) * xAngle (quat, angleX);
	}

	public static Quaternion xyzAngle (Quaternion quat, float angleX, float angleY, float angleZ) {
		return xAngle (quat, angleX) * yAngle (quat, angleY) * zAngle (quat, angleZ);
	}
}
