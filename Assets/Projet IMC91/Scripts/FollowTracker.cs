using System.Collections;
using UnityEngine;

/// <summary>
/// Associe le transform de l'objet courant aux données de Position/Rotation d'un Virtual3DTracker
/// </summary>
public class FollowTracker : MonoBehaviour
{
    //tracker data
    public Virtual3DTracker tracker;

    //3d scale vector
    public Vector3 translationScale = Vector3.one;

    //3d scale vector
    public Vector3 rotationScale = Vector3.one;

    //movement mode
    public enum movementModeEnum { Position, Translation, Speed, Acceleration } ;

    public movementModeEnum movementMode = movementModeEnum.Position;

    public bool freezeRotations = false;

    private void Start()
    {
        if (tracker == null)
            this.enabled = false;
    }

    private void Update()
    {
        switch (movementMode)
        {
            case movementModeEnum.Position:
                this.transform.position = new Vector3(tracker.Position.x * translationScale.x, tracker.Position.y * translationScale.y, tracker.Position.z * translationScale.z);
                if (!freezeRotations) this.transform.rotation = Quaternion.Euler(tracker.Orientation.x * rotationScale.x, tracker.Orientation.y * rotationScale.y, tracker.Orientation.z * rotationScale.z);
                break;

            case movementModeEnum.Translation:
                this.transform.Translate(new Vector3(tracker.Translation.x * translationScale.x, tracker.Translation.y * translationScale.y, tracker.Translation.z * translationScale.z), Space.World);
                if (!freezeRotations) this.transform.Rotate(tracker.Rotation.x * rotationScale.x, tracker.Rotation.y * rotationScale.y, tracker.Rotation.z * rotationScale.z, Space.World);
                break;

            default: break;
        }
    }
}