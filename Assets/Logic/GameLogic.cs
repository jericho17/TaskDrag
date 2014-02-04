
using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class GameLogic
{
	private float startTime;
	private int ApplesCount = 20;
	private float GameLength = 60;
	private GameObject apple;
	private List<GameObject> Apples;
	private float startDrag;
	private int applesLeft;
	private int score;

	public bool GameStarted;
	public int Score
	{
		get
		{
			return score;
		}
		set
		{
			score = value;
			OnScoreChanged();
		}
	}
	public int ApplesLeft
	{ 
		get
		{
			return applesLeft;
		} 
		set
		{
			applesLeft = value;
			OnApplesCountChanged();
		}
	}
	public float GameTime{get{return Time.time - startTime;}}
	public float TimeLeft{get{return GameLength - GameTime;}}
	public float TotalTime;
	public float TotalScores;
	public float TimeBonus;



	public GameLogic ()
	{
		GameStarted = false;
		Apples = new List<GameObject> ();
	}


	#region Events

	public event EventHandler ScoresChanged;
	public event EventHandler ApplesCountChanged;
	public event EventHandler GameEnded;

	private void OnApplesCountChanged()	
	{
		if (ApplesCountChanged != null) 
			ApplesCountChanged (this, EventArgs.Empty);
	}
	private void OnScoreChanged()
	{
		if (ScoresChanged != null)
			ScoresChanged (this, EventArgs.Empty);
	}
	private void OnGameEnded()
	{
		if (GameEnded != null)
			GameEnded (this, EventArgs.Empty);
	}

	#endregion

	#region Public methods

	public void Start()
	{
		GameStarted = true;
		
		startTime = Time.time;
		startDrag = 0;
		score = 0;
		Score = 0;
		InitApples ();
		
		ApplesLeft = ApplesCount;
		Apples.Clear ();
	}
	
	public void Update()
	{
		if (!GameStarted)
			Start ();

		CheckTime ();
	}
	
	public void StartDrag()
	{
		startDrag = GameTime;
	}
	public void EndDrag (GameObject gameObject)
	{
		if (Apples.Contains (gameObject))
			return;
		Apples.Add (gameObject);
		
		CountScore ();
		ApplesLeft --;
		CheckGame ();
	}


	#endregion

	#region Private methods

	private void InitApples()
	{
		//Возможно, нужно вызывать Destroy(gameObject) но на 100к неактивных объектах не заметил отклонений.
		//Поэтому, счел достаточным деактивировать объекты, чтобы не добавлять ненужное наследование от MonoBehaviour
		var oldApples = GameObject.FindGameObjectsWithTag ("Ball");
		foreach (var item in oldApples) 
		{
			item.SetActive(false);
		}

		apple = Resources.Load ("Ball") as GameObject;
		for (int i = 0; i < ApplesCount; i++) 
		{
			var pos = new Vector3(UnityEngine.Random.Range(-5,25), 4.4858f, UnityEngine.Random.Range(5,25));
			var a = GameObject.Instantiate(apple, pos, new Quaternion()) as GameObject;
		}
	}

	private void CheckGame()
	{
		if (ApplesLeft == 0) 
		{
			TotalTime = TimeLeft;
			TimeBonus = 257 * TotalTime;
			TotalScores = score + TimeBonus;
			OnGameEnded ();
		}
		if (TimeLeft < 1 && TimeLeft > -1) 
		{
			TotalTime = 0;
			TimeBonus = 0;
			TotalScores = score + TimeBonus;
			OnGameEnded ();
		}
	}
	
	private void CountScore()
	{
		Debug.Log("Count score with score: "+Score);
		var time = GameTime - startDrag;
		var score1 = 1.5f - time;
		Score += (int)(score1>0f?score1 * 1000f:200f);	
	}

	private void CheckTime()
	{
		if (TimeLeft > -1 && TimeLeft <1)
			CheckGame ();
	}

	#endregion

	#region Singletone

	protected static GameLogic _inst;
	
	public static GameLogic Inst
	{
		get
		{
			if (_inst == null)
				_inst = new GameLogic();
			
			return _inst;
		}


	#endregion
}
}