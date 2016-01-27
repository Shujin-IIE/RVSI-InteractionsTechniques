using System.Collections;
using UnityEngine;

/// <summary>
/// Structure pour représenter un mapping clavier/souris
/// utilisé dans Virtual3DTrackerMouse pour les translations/rotations
/// </summary>
[System.Serializable]
public class AxisMapping
{
    //Input Manager Axis
    public string axis;

    //Input Manager Button
    public string modifier;

    //Sign of the translation/rotation
    [Range(-1, 1)]
    public int multiplier = 1;
}