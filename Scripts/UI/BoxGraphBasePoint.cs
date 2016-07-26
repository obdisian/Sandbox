using UnityEngine;
using System.Collections;
using System.Linq;

public class BoxGraphBasePoint : MonoBehaviour {

	public static int maxPlayCount;

	void Start () {
		maxPlayCount = PlayCount.playCount.Max ();

		if (maxPlayCount < 10) {
			maxPlayCount = 10;
			return;
		}


		int maxfor = maxPlayCount > 1000 ? 1000 : maxPlayCount > 10000 ? 10000 : 100;

		int tempMaxPlayCount = 0;
		for (int i = 1; i <= maxfor; i++) {
			if (maxPlayCount >= 10 * i) {
				tempMaxPlayCount = 10 * i + 10;
			}
		}
		maxPlayCount = Mathf.Min (tempMaxPlayCount, 1000);
	}
}
