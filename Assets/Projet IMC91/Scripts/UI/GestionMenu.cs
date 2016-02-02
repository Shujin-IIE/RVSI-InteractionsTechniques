using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GestionMenu : MonoBehaviour
{

	public int NumeroTesteur = 0;
	public InputField NumTesteurInput;
	public Button MMBtn;
	public Button MMWallBtn;
	public Button MLBtn;

	public Button MLWallBtn;

	public GameObject MenuPanel;
	public GameObject Menu1;
	private bool esc = false;

	// Use this for initialization
	void Start ()
	{
		MMBtn.interactable = false;
		MMWallBtn.interactable = false;
		MLBtn.interactable = false;

		MLWallBtn.interactable = false;


	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.F1)) {
            
			if (!esc) {
				Menu1.GetComponent<Animation>().Play ("ShowMenu1");
				esc = true;
			}
			else {
				Menu1.GetComponent<Animation>().Play ("HideMenu1");
				esc = false; 
			}
		}

	}

	public void ValidateNumberTesteur ()
	{
		MMBtn.interactable = false;
		MMWallBtn.interactable = false;
		MLBtn.interactable = false;

		MLWallBtn.interactable = false;
		if (NumTesteurInput.text.Length > 0) {
			try {
				NumeroTesteur = int.Parse(NumTesteurInput.text);
			} 
			catch {
				NumeroTesteur = 0;
			}
		}

		if (NumeroTesteur > 0)
		{ // Si le numéro du testeur  est correct 
			print(NumeroTesteur);
			// Play animation of the scenes 
			MMBtn.interactable = true;
			MMWallBtn.interactable = true;
			MLBtn.interactable = true;

			MLWallBtn.interactable = true;
            GameManager.instance.Log.NumTester = NumeroTesteur;

		}

        
	}


	//Choix des tests ( scenes )
	public void ChooseMM()   // Scene Mouse Manipulation
	{
		var anim = MenuPanel.GetComponent<Animation>();
		anim.Play("HideMenu");

		if (Application.loadedLevelName != "Mouse Scene")
		{
			Application.LoadLevel("Mouse Scene");
		}
	}

	public void ChooseMMWall()   // Scene Mouse Manipulation Wall
	{
		var anim = MenuPanel.GetComponent<Animation>();
		anim.Play("HideMenu");

		if (Application.loadedLevelName != "Mouse Wall Scene")
		{
			Application.LoadLevel("Mouse Wall Scene");
		}
	}


	public void ChooseML()   // Scene Leap Manipulation
	{
		var anim = MenuPanel.GetComponent<Animation>();
		anim.Play("HideMenu");

		if (Application.loadedLevelName != "Leap Scene")
		{
			Application.LoadLevel("Leap Scene");
		}
	}

	public void ChooseMLWall ()   // Scene Leap Manipulation Wall
	{
		var anim = MenuPanel.GetComponent<Animation>();
		anim.Play("HideMenu");

		if (Application.loadedLevelName != "Leap Wall Scene")
		{
			Application.LoadLevel("Leap Wall Scene");
		}
	}

	public void NouveauTest()
	{
		if (!esc) {
			Menu1.GetComponent<Animation>().Play ("ShowMenu1");
			esc = true;
		} else {
			Menu1.GetComponent<Animation>().Play ("HideMenu1");
			esc = false;
		}


		MenuPanel.GetComponent<Animation>().Play ("ShowMenu");
	}

	public void Quitter()
	{
		Application.Quit();
	}
}

