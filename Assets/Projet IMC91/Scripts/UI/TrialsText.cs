using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrialsText : MonoBehaviour
{
    private Text _text;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "Essai " + GameManager.instance.trial.ToString();
    }
}
