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
	private int index = 0;

	public Timer Timer { get; private set; }
    public Log Log { get; private set; }

    protected void Start()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Multiple instances of GameManager");

		Timer = GetComponent<Timer>();
        Log = GetComponent<Log>();

		foreach (GameObject go in interactibleObjects)
		{
			go.SetActive(false);
		}
		InstantiateNextObject();
    }

    protected void Update()
    {
    }

	public void InstantiateNextObject()
	{
		if (interactibleObjects.Count < index)
		{
			interactibleObjects[index].SetActive(true);
			index++;
		}
	}
}