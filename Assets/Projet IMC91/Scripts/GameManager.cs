using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton GameManager
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //liste des objets à remplir à la main
    public List<GameObject> interactibleObjects;

    //liste des obstacles
    public List<GameObject> obstacleObjects;

    protected void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Multiple instances of GameManager");

		// TODO: Remove the automatic start when the start feature is ready
		GetComponent<Timer>().Run();
    }

    protected void Update()
    {
    }
}