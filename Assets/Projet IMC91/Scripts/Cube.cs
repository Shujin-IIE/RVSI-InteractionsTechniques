using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	private void OnTriggerEnter (Collider other)
	{
		Debug.Log("Collision detected");
		if (other.CompareTag("Target"))
		{
			Debug.Log("Target hit");
		}
	}
}
