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

    protected override void Start()
    {
        base.Start();
        trackerLeap = (Virtual3DTrackerLeap)tracker;
    }

    protected override void Update()
    {
        SelectionManipulationManager();
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
    }
}