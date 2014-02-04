using UnityEngine;
using System.Collections;

public class BoxChecker : MonoBehaviour 
{
	private GameLogic logic;

	void Start () 
	{
		logic = GameLogic.Inst;
	}

	void OnTriggerEnter(Collider other) 
	{
		logic.EndDrag (other.gameObject);
	}
}
