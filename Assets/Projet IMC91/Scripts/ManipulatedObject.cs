using UnityEngine;
using System.Collections;

public class ManipulatedObject : MonoBehaviour
{
	public bool IsHittingTarget { get; private set; }

	// Use this for initialization
	void Start()
	{
		IsHittingTarget = false;
	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Collision enter detected");
		if (other.CompareTag("Target"))
		{
			Debug.Log("Target hit");
			IsHittingTarget = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Collision exit detected");
		if (other.CompareTag("Target"))
		{
			Debug.Log("Target exit");
			IsHittingTarget = false;
		}
	}
}
