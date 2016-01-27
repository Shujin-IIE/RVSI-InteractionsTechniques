using System;// Date
using System.Collections;
using System.IO;//Input Ouput
using UnityEngine;

/// <summary>
/// Modèle de script pour écrire des données dans un fichier texte fileName :
/// - position de la souris toutes les timeSample secondes
/// - id et position lors d'un clic bouton
/// </summary>
public class Log : MonoBehaviour
{
    public String fileName = "fileName.csv";
    public float timeSample = 0.5f;
    private StreamWriter stream;

    protected void Start()
    {
        if (String.Compare(fileName, "") != 0)
        {
            Debug.Log(String.Format("Creation du fichier {0}. Début ", fileName));
            // Create an instance of StreamWriter to write text to a file.
            stream = new StreamWriter(Application.dataPath + "/" + fileName);
            // Add some text to the file.
            stream.Write("Fichier de Log : ");
            stream.WriteLine("time;mouseX;mouseY;button");
            stream.WriteLine("-------------------");

            InvokeRepeating("writeLine", 0.1f, timeSample);
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
            stream.WriteLine(Time.time.ToString("00.00") + ";" + Input.mousePosition.x + ";" + Input.mousePosition.y + ";" + 1);
        if (Input.GetMouseButtonDown(1))
            stream.WriteLine(Time.time.ToString("00.00") + ";" + Input.mousePosition.x + ";" + Input.mousePosition.y + ";" + 2);
    }

    protected void writeLine()
    {
        stream.WriteLine(Time.time.ToString("00.00") + ";" + Input.mousePosition.x + ";" + Input.mousePosition.y + ";" + 0); //mouseposition en pixel dans la fenetre Game (0,0) en bas à gauche
    }

    protected void OnApplicationQuit()
    {
        stream.Close();
    }
}