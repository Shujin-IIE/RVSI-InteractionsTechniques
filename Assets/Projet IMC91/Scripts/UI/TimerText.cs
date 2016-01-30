using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerText : MonoBehaviour {
	[SerializeField]
	private Timer _timer;

	private Text _text;

	// Use this for initialization
	void Start ()
	{
		_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		_text.text = Mathf.CeilToInt(_timer.CurrentTime).ToString();
	}
}
