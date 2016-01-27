using System.Collections;
using UnityEngine;

/// <summary>
/// Gestion des commandes globales de l'application (autres que main virtuelle)
/// cf. input manager
/// </summary>
public class ApplicationControl : MonoBehaviour
{
    protected void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}