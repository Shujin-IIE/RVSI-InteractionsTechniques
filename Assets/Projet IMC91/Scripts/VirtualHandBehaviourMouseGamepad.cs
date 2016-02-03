using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main virtuelle
/// selection/manipulation de l'objet (1 seul) en collision
/// - en appuyant sur le bouton manipulationButtonIDOnTracker pour démarrer
/// - en appuyant sur le bouton manipulationButtonIDffTracker pour arrêter
/// coloration en vert de la main si sélection possible
/// coloration en rouge de la main si manipulation en cours
/// cf input manager
/// </summary>

public class VirtualHandBehaviourMouseGamepad : VirtualHandBehaviour
{
    //boutons : (@todo utiliser les données du tracker)
    public string manipulationButtonNameOnTracker = "Fire1";

    public string manipulationButtonNameOffTracker = "Fire1";
    public string manipulationButtonNameKeepOnTracker = "";

    //for debug
    public bool manipOn = false;

    public bool collisionOn = false;

	private bool isCollidingWithTarget;

	protected override void Start()
	{
		base.Start();
		isCollidingWithTarget = false;
	}

    protected override void Update()
    {
        SelectionManipulationManager();
    }

	private void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Target"))
		{
			isCollidingWithTarget = true;
			Debug.Log("(Mouse) Hand<->Target " + isCollidingWithTarget);
		}
	}

    private void OnTriggerStay(Collider col)
    {
        GameObject g = col.gameObject;
        if (IsInteractible(g) && (g == currentManipulatedObject || currentManipulatedObject == null))
        {
            collisionOn = true;
            lastCollidedObject = g;
            ColorizeVirtualHand(Color.green);
            if (manipOn)
            {
                ColorizeVirtualHand(Color.red);
                if (g.GetComponent<Rigidbody>() != null) g.GetComponent<Rigidbody>().isKinematic = true;
                g.transform.parent = this.transform;
                currentManipulatedObject = g;
            }
            else
            {
                lastCollidedObject.transform.parent = null;
                if (lastCollidedObject.GetComponent<Rigidbody>() != null) lastCollidedObject.GetComponent<Rigidbody>().isKinematic = false;

				// If the manipulated object is coliding the target => detroy
				if (currentManipulatedObject != null && currentManipulatedObject.GetComponent<ManipulatedObject>().IsHittingTarget)
				{
					GameManager.instance.Timer.AddIntermediate();
                    Vector3 PositionCube = currentManipulatedObject.transform.position;
                    GameObject Target = GameObject.Find("Target");
                    GameManager.instance.Log.distances.Add(Vector3.Distance(PositionCube, Target.transform.position));
					//Destroy(currentManipulatedObject);
                    currentManipulatedObject.SetActive(false);
                    currentManipulatedObject.GetComponent<ManipulatedObject>().SetInitialPosition();
                    GameManager.instance.InstantiateNextObject();
					ColorizeVirtualHand(Color.gray);
				}

                currentManipulatedObject = null;
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        GameObject g = col.gameObject;
        if (IsInteractible(g))
        {
            collisionOn = false;
            lastCollidedObject = null;
            ColorizeVirtualHand(Color.gray);
        }

		if (col.CompareTag("Target"))
		{
			isCollidingWithTarget = false;
			Debug.Log("(Mouse) Hand</>Target " + isCollidingWithTarget);
		}
    }

    private void SelectionManipulationManager()
    {
        if (manipulationButtonNameKeepOnTracker.Equals("")) //on est en mode on/off par appui simple sur bouton
        {
            if (manipulationButtonNameOffTracker.Equals(manipulationButtonNameOnTracker))
            { //même bouton pour activer/desactiver
                if (Input.GetButtonDown(manipulationButtonNameOnTracker) && collisionOn)
                {
                    manipOn = !manipOn;
                }
            }
            else
            {
                if (Input.GetButtonDown(manipulationButtonNameOnTracker) && collisionOn)
                {
                    manipOn = true;
                }
                if (Input.GetButtonDown(manipulationButtonNameOffTracker))
                {
                    manipOn = false;
                }
            }
        }
        else //on est en appui continu sur bouton
        {
            if (Input.GetButton(manipulationButtonNameKeepOnTracker) && collisionOn)
            {
                manipOn = true;
            }
            else manipOn = false;
        }

		if (isCollidingWithTarget && Input.GetButtonUp(manipulationButtonNameOnTracker))
		{
            GameManager.instance.InstantiateNextObject();
			GameManager.instance.Timer.Run();
		}
    }
}