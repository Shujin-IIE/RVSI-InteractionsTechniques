using UnityEngine;
using System.Collections;

public class CountCollision : MonoBehaviour {
	
	public int countCollisions = 0;
	bool colourChangeCollision = false;
	public float colourChangeDelay = 0.5f;
	float currentDelay = 0f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("im=" + this.name + " col=" + countCollisions);
		checkColourChange();
	}



	void OnCollisionEnter(Collision hit)
	{
		if (hit.gameObject.name == "Cube" ){
			//Debug.Log("one");
			countCollisions++;
			colourChangeCollision = true;
			currentDelay = Time.time + colourChangeDelay;
		}
	}
	
	void checkColourChange()
	{        
		if(colourChangeCollision)
		{
			transform.GetComponent<Renderer>().material.color = Color.red;
			if(Time.time > currentDelay)
			{
				transform.GetComponent<Renderer>().material.color = Color.white;
				colourChangeCollision = false;
			}
		}
	}

    public void ResetCollision()
    {
        countCollisions = 0;
    }
}
	