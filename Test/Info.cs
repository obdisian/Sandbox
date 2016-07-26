using UnityEngine;
using System.Collections;

public class Info : MonoBehaviour {

	public enum BGMName {
		B1, B2, B3,
	}
	public enum SEName {
		Jump, Crush, Frag, Reverse, Force, Fast, Slow, Rejump, ColorBlock, Goal,
	}

	public static void PlayBGM (BGMName keyName) {
		Audio.PlayBgm ("BGM_" + (int)keyName);
	}
	public static void ShotSE (SEName keyName) {
		Audio.PlaySe ("SE_" + (int)keyName);
	}
}
