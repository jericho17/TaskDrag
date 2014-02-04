using System;
using UnityEngine;


public class GameInterface : MonoBehaviour
{
	private GameLogic logic;
	private GUIStyle guiStyle;

	private UILabel scores;
	private UILabel time;
	private UILabel apples;

	private GameObject gTime;
	private GameObject gscoresE;
	private GameObject gtimeBonusE;
	private GameObject gtotalScoresE;
	private GameObject gmenuBg;

	private UILabel scoresE;
	private UILabel timeBonusE;
	private UILabel totalScoresE;
	private UISprite menuBg;

	private UIButton retry;
	private int timeLeft;

	void Start()
	{
		logic = GameLogic.Inst;		
		logic.ApplesCountChanged += (sender, e) => OnApplesCountChanged();
		logic.ScoresChanged += (sender, e) => OnScoresChanged();
		logic.GameEnded += (sender, e) => OnGameEnded();

		time = GameObject.Find ("lTime").GetComponent<UILabel>();

		apples = GameObject.Find ("lApplesCount").GetComponent<UILabel>();
		scores = GameObject.Find ("lScores").GetComponent<UILabel>();

		scoresE = GameObject.Find ("lendScores").GetComponent<UILabel>();
		timeBonusE = GameObject.Find ("lendTimeLeft").GetComponent<UILabel>();
		totalScoresE = GameObject.Find ("lendTotalScores").GetComponent<UILabel>();
		menuBg = GameObject.Find ("sMenuBg").GetComponent<UISprite>();

		gTime = GameObject.Find ("lTime");
		gscoresE = GameObject.Find ("lendScores");
		gtimeBonusE = GameObject.Find ("lendTimeLeft");
		gtotalScoresE = GameObject.Find ("lendTotalScores");
		gmenuBg = GameObject.Find ("sMenuBg");

		retry = GameObject.Find ("bRetry").GetComponent<UIButton>();

		DisableScores ();
	}

	void Update()
	{
		logic.Update();		
	}
	
	void OnGUI()
	{
		if (timeLeft != logic.TimeLeft) 
		{
			timeLeft = (int)logic.TimeLeft;
			time.text = timeLeft+"";
		}
	}


	void OnScoresChanged()
	{
		scores.text = "Scores: " + logic.Score;
	}
	void OnApplesCountChanged()
	{
		apples.text = "Apples left: " + logic.ApplesLeft;
	}
	void OnGameEnded()
	{
		UpdateScores ();
		EnableScores ();
	}

	void UpdateScores()
	{
		scoresE.text = "Scores: "+logic.Score;
		timeBonusE.text = "Bonus: "+(int)logic.TimeBonus;
		totalScoresE.text = "Total: "+(int)logic.TotalScores;
	}

	void EnableScores()
	{
		ChangeScoresEnabled (true);
	}
	void DisableScores()
	{
		ChangeScoresEnabled (false);
	}
	void ChangeScoresEnabled(bool enabledState)
	{
		NGUITools.SetActive (gscoresE, enabledState);
		NGUITools.SetActive (gtimeBonusE, enabledState);
		NGUITools.SetActive (gtotalScoresE, enabledState);
		NGUITools.SetActive (gmenuBg, enabledState);
		NGUITools.SetActive (gTime, !enabledState);
	}


	public void Retry()
	{
		DisableScores ();
		logic.Start();
	}
	public void Quit()
	{
		Application.Quit();
	}
}
