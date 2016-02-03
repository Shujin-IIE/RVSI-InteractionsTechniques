using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MethodInputField : MonoBehaviour {

	private Text Text;

	// Use this for initialization
	void Start () {
		Text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private int MethodNumber()
	{
		return int.Parse(Text.text);
	}
}
