using System.Collections;
using UnityEngine;

/// <summary>
/// Update the Position, Translation and Buttons state of the virtual tracker
/// based on the axis mapping chosen in the inspector
/// NB : probleme qd memes axes et pas de modifier pour translations
/// NB : pas de solution satisfaisante pour les rotations
/// </summary>
public class Virtual3DTrackerMouse : Virtual3DTracker
{
    //Axis mapping for 3 translations
    [Tooltip("Caution : Add a no-key/no-effect axis in input manager")]
    public AxisMapping[] TranslationAxisMapping = new AxisMapping[3];

    private float[] axis = new float[3];

    public override void Start()
    {
        base.Start();
        buttonsState = new bool[3];
        onoffButtonsState = new bool[3];
    }

    protected override void UpdateTracker()
    {
        for (int i = 0; i < TranslationAxisMapping.Length; i++)
        {
            if (TranslationAxisMapping[i].modifier == "" || Input.GetButton(TranslationAxisMapping[i].modifier))
                axis[i] = Input.GetAxis(TranslationAxisMapping[i].axis) * TranslationAxisMapping[i].multiplier;
        }

        Position = new Vector3(Position.x + axis[0], Position.y + axis[1], Position.z + axis[2]);
        Translation = new Vector3(axis[0], axis[1], axis[2]);

        for (int i = 0; i < axis.Length; i++)
            axis[i] = 0;
    }

    protected override void UpdateButtons()
    {
        buttonsState[0] = Input.GetButton("Fire1");
        buttonsState[1] = Input.GetButton("Fire2");
        buttonsState[2] = Input.GetButton("Fire3");
    }
}