using System;// Date
using System.Collections;
using System.Collections.Generic;
using System.IO;//Input Ouput
using UnityEngine;

/// <summary>
/// Modèle de script pour écrire des données dans un fichier texte fileName :
/// - position de la souris toutes les timeSample secondes
/// - id et position lors d'un clic bouton
/// </summary>
public class Log : MonoBehaviour
{
    public String fileName = "fileName";
    public float timeSample = 0.5f;
    private StreamWriter stream;
	private Timer Timer;
    private CountCollision[] CountCollisions;

    protected void Start()
    {
		String timeStamp = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
		Timer = GetComponent<Timer> ();
        CountCollisions = FindObjectsOfType<CountCollision>();
        if (String.Compare(fileName, "") != 0)
        {
            Debug.Log(String.Format("Creation du fichier {0}. Début ", fileName));
            // Create an instance of StreamWriter to write text to a file.
            stream = new StreamWriter(Application.dataPath + "/" + fileName + timeStamp + ".csv");
            // Add some text to the file.
            stream.Write("Fichier de Log : ");
			stream.WriteLine("id;TimerCube1;TimerCube2;TimerCube3;Timer;DistanceCube1;DistanceCube2;DistanceCube3;WallCollision");
            stream.WriteLine("-------------------");

            //InvokeRepeating("writeLine", 0.1f, timeSample);
        }
        else
        {
            Debug.Log("Le nom du fichier n'est pas valide !");
            this.enabled = false;
        }
    }

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int SumCollisions = 0; 
            foreach (CountCollision cc in CountCollisions)
            {
                SumCollisions += cc.countCollisions;
            }
            stream.WriteLine(Timer.IntermediateTime[0] + ";" + Timer.IntermediateTime[1] + ";" + Timer.IntermediateTime[2] + ";" + Timer.CumulativeTime[2] + ";" + SumCollisions);
        }
    }

    /*protected void writeLine()
    {
        stream.WriteLine(Time.time.ToString("00.00") + ";" + Input.mousePosition.x + ";" + Input.mousePosition.y + ";" + 0); //mouseposition en pixel dans la fenetre Game (0,0) en bas à gauche
    }*/

    protected void OnApplicationQuit()
    {
        stream.Close();
    }
}