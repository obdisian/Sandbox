using UnityEngine;
using System.Collections;

public class SelectOptionPanelButton : MonoBehaviour {

	int subOpenPanelloseCount = 2;
	public void OpenSubOptionPanel () {
		SubOptionPanel.openSubOptionPanel = true;
	}
	public void CloseSubOptionPanel () {
		subOpenPanelloseCount--;
		SubOptionPanel.openFirstClearPanel = !SubOptionPanel.openFirstClearPanel;
		if (subOpenPanelloseCount <= 0) {
			subOpenPanelloseCount = 2;
			SubOptionPanel.openSubOptionPanel = false;
		}
	}



	public void BGM_OnOrOff () {
		TitleSelectProgression.bgmEnable = !TitleSelectProgression.bgmEnable;

		if (!TitleSelectProgression.bgmEnable) {
			BgmManager.Instance.StopImmediately ();
		}
	}

	public void SE_OnOrOff () {
		TitleSelectProgression.seEnable = !TitleSelectProgression.seEnable;
	}

	//	スキンを選択する用
	public void SkinChange () {
		TitleSelectProgression.skinEnable = true;
	}

	//	スキンパネルを引っ込める関数
	public void SkinPanelBacker () {
		TitleSelectProgression.skinEnable = false;
	}

	//	ヘルプ展開
	public void Open_HelpPanel () {
		TitleSelectProgression.helpEnable = true;
	}

	//	ヘルプクローズ
	public void Close_HelpPanel () {
		if (HelpPanel.hOpenFrag != HelpPanel.HelpOpenFrag.None) {
			HelpPanel.hOpenFrag = HelpPanel.HelpOpenFrag.None;
		} else {
			HelpPanel.isOnhOpenFrag = false;
			TitleSelectProgression.helpEnable = false;
		}
	}

	public void Open_HelpTexture (int i) {
		HelpPanel.isOnhOpenFrag = true;
		if (HelpPanel.hOpenFrag != HelpPanel.HelpOpenFrag.None) {
			HelpPanel.hOpenFrag = HelpPanel.HelpOpenFrag.None;
		} else {
			HelpPanel.hOpenFrag = (HelpPanel.HelpOpenFrag)i;
		}
	}

	//	作者欄
	public void Open_CreatorPanel () {
		TitleSelectProgression.creatorEnable = true;
	}

	public void Open_GameCenter () {
		RankingUtility.ShowLeaderboardUI();
	}

	public void Open_Language () {
		LanguagePanel.isOpenLanguagePanel = true;
	}


	public void SkinDecision (int i) {
		if (StageClearCount (i)) {
			Player.skinType = (Player.SkinType)i;
		} else {
			SkinButton.isOpenDescriptionPanel = true;
			SkinButton.selectedSkin = i;
		}
	}

	//	trueが解放されていると言う返答
	bool StageClearCount (int integer) {
		int _1_5 = 0, _6_10 = 0, _11_15 = 0;
		for (int i = 0; i < 15; i++) {
			if (100 == Score.mapScore [i]) {
				if (i < 5) _1_5++;
				else if (i < 10) _6_10++;
				else if (i < 15) _11_15++;
			}
		}
		if (integer == 0 ||
			integer == 1 && _1_5 == 5 ||
			integer == 2 && _6_10 == 5 ||
			integer == 3 && _11_15 == 5) {
			return true;
		} else {
			return false;
		}
	}
}
