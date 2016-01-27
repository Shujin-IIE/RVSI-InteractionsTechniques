using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
	public bool IsRunning { get; private set; }

	public float CurrentTime { get; private set; }

	public List<float> IntermediateTime { get; private set; }
	public List<float> CumulativeTime { get; private set; }

	void Start()
	{
		InitTimer();
	}

	void Update()
	{
		if (IsRunning)
		{
			CurrentTime += Time.deltaTime;
		}
	}

	/// Init the timer and stop it if running
	public void InitTimer(float start = 0.0f)
	{
		CurrentTime = start;
		IntermediateTime = new List<float>();
		CumulativeTime = new List<float>();
		Stop();
	}

	public void Run()
	{
		IsRunning = true;
	}

	public void Stop()
	{
		IsRunning = false;
	}

	public void AddIntermediate()
	{
		if (IntermediateTime.Count == 0)
		{
			IntermediateTime.Add(CurrentTime);
		}
		else
		{
			IntermediateTime.Add(CurrentTime - CumulativeTime[CumulativeTime.Count - 1]);	
		}

		CumulativeTime.Add(CurrentTime);
	}
}
