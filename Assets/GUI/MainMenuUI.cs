using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour 
{
	public void StartGame()
	{
		Application.LoadLevel ("MainScene");
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
