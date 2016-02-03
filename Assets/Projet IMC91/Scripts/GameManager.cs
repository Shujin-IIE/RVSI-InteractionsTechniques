using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Singleton GameManager
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // how many trial per condition
    protected const int TrialsQuantity = 3;

    // distance from target to cubes
    protected const float objectToTargetDistance = 14f;

    // number of trial being executed
    public int trial;

    //liste des objets à remplir à la main
    public List<GameObject> interactibleObjects;

    // directions for cubes positions
    private List<Vector3> interactibleObjectsDirections;

    //liste des obstacles
    public List<GameObject> obstacleObjects;
	private int index = 0;

	public Timer Timer { get; private set; }
    public Log Log { get; private set; }

    // animated pointing arrow
    public GameObject Arrow;

    // trial panel
    public GameObject TrialPanel;

    protected void Start()
    {
        trial++;

        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Multiple instances of GameManager");

		Timer = GetComponent<Timer>();
        Log = GetComponent<Log>();

        interactibleObjectsDirections = new List<Vector3>() { new Vector3(Mathf.Sqrt(3) / 2, 0, 1f / 2), new Vector3(Mathf.Sqrt(3) / 2, 0, -1f / 2), new Vector3(1, 0, 0) };
        interactibleObjectsDirections.Shuffle();

        foreach (GameObject go in interactibleObjects)
		{
			go.SetActive(false);
		}
        //InstantiateNextObject();
        Arrow.GetComponent<Animation>().Play("ShowArrow");
    }

    protected void Update()
    {
    }

	public void InstantiateNextObject()
	{
        Arrow.SetActive(false);
        if (interactibleObjects.Where(t => t.activeSelf).FirstOrDefault() != null) return;
		if (index < interactibleObjects.Count)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Target");
            if (gos.Length == 0)
            {
                Debug.LogError("No object with tag Target found");
                return;
            }
            var a = gos[0].transform.position;
            Vector3 targetPosition = new Vector3(gos[0].transform.position.x, -19, gos[0].transform.position.z);
            interactibleObjects[index].transform.position = targetPosition + interactibleObjectsDirections[index] * objectToTargetDistance;
            interactibleObjects[index].SetActive(true);
            //Debug.Log((gos[0].transform.position - interactibleObjects[index].transform.position).magnitude);
            index++;
		} else
        {
            interactibleObjectsDirections = new List<Vector3>() { new Vector3(Mathf.Sqrt(3) / 2, 0, 1f / 2), new Vector3(Mathf.Sqrt(3) / 2, 0, -1f / 2), new Vector3(1, 0, 0) };
            interactibleObjectsDirections.Shuffle();

            foreach (GameObject go in interactibleObjects)
            {
                go.SetActive(false);
            }

            index = 0;
            trial++;

            if (trial == TrialsQuantity)
            {
                trial = 1;
                // show menu to click next condition
            }

            Arrow.SetActive(true);
            TrialPanel.GetComponent<Animation>().Play("HideTrials");
            Arrow.GetComponent<Animation>().Play("ShowArrow");
            // reset timmer - And I didnt see how the log is working... but: tell the log to save the trial
        }
    }
}

static class RandomizableList
{
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rnd = new System.Random();
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, rnd.Next(i, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}