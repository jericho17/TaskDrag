using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour 
{	
	public float speed = 5f;	
	private GameObject box;

	void Start () 
	{
		box = GameObject.Find ("Box");
	}

	void Update () 
	{					
		if(Input.GetMouseButton(1))
		{
			transform.RotateAround (box.transform.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
		}
	}
}
