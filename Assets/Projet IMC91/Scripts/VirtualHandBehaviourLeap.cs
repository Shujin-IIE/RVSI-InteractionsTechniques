using System.Collections;
using UnityEngine;

/// <summary>
/// Main virtuelle
/// selection/manipulation de l'objet (1 seul) en collision
/// - en fermant la main pour saisir
/// - en ouvrant la main pour relacher
/// coloration en vert de la main si sélection possible
/// coloration en rouge de la main si manipulation en cours
/// </summary>
public class VirtualHandBehaviourLeap : VirtualHandBehaviour
{
    private Virtual3DTrackerLeap trackerLeap;

    //for debug
    public bool manipOn = false;

    public bool collisionOn = false;

	private bool isCollidingWithTarget;

    protected override void Start()
    {
        base.Start();
        trackerLeap = (Virtual3DTrackerLeap)tracker;
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
			Debug.Log("(Leap) Hand<->Target");
			isCollidingWithTarget = true;
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
			Debug.Log("(Leap) Hand</>Target");
			isCollidingWithTarget = false;
		}
    }

    private void SelectionManipulationManager()
    {
        if (trackerLeap.IsGrabbing && collisionOn) //on est en mode on/off par fermeture/ouverture de la main
        {
            manipOn = true;
        }
        else
        {
            manipOn = false;
        }

		if (trackerLeap.IsGrabbing && isCollidingWithTarget)
        {
            GameManager.instance.InstantiateNextObject();   // TODO : test in real-time with leap or change by transition
            GameManager.instance.Timer.Run();
		}
    }
}