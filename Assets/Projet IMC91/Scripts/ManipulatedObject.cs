using UnityEngine;
using System.Collections;

public class ManipulatedObject : MonoBehaviour
{
	public bool IsHittingTarget { get; private set; }
    public Vector3 position;

    private Vector3 initialPosition;

    public Material materialInCollision;
    private Material myMaterial;

    // Use this for initialization
    void Start()
	{
        initialPosition = this.transform.position;
		IsHittingTarget = false;
        myMaterial = this.GetComponent<Renderer>().material;
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
            this.GetComponent<Renderer>().material = materialInCollision;
            Debug.Log("Target hit");
			IsHittingTarget = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("Collision exit detected");
		if (other.CompareTag("Target"))
		{
            this.GetComponent<Renderer>().material = myMaterial;
            Debug.Log("Target exit");
			IsHittingTarget = false;
		}
	}

    public void SetInitialPosition()
    {
        this.transform.position = initialPosition;
        this.GetComponent<Renderer>().material = myMaterial;
    }
}
