using System.Collections;
using UnityEngine;

public class VirtualHandBehaviour : MonoBehaviour
{
    //tracker data
    public Virtual3DTracker tracker;

    //current selectionable object
    public GameObject lastCollidedObject;

    public GameObject currentManipulatedObject;

    protected virtual void Start()
    {
        if (tracker == null)
            Debug.LogError("No Virtual3DTracker found in " + gameObject.name);
        lastCollidedObject = null;
        currentManipulatedObject = null;
    }

    protected virtual void Update()
    {
    }

    protected void ColorizeVirtualHand(Color c)
    {
        this.gameObject.GetComponentInChildren<Renderer>().material.color = c;
    }

    protected bool IsInteractible(GameObject g)
    {
        return (GameManager.instance != null ? GameManager.instance.interactibleObjects.Contains(g) : false);
    }

    protected bool IsObstacle(GameObject g)
    {
        return (GameManager.instance != null ? GameManager.instance.obstacleObjects.Contains(g) : false);
    }
}