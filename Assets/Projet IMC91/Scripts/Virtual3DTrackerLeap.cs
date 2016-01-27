using Leap;
using System.Collections;
using UnityEngine;

/// <summary>
/// Fournit la position, orientation, translation et rotation de la main (1ère main gauche ou droite reconnue) via le leap motion
/// </summary>
public class Virtual3DTrackerLeap : Virtual3DTracker
{
    //leap motion controller
    [Tooltip("Must be in the scene")]
    public HandController handController;

    //leap hand intermediate attributes
    private Hand hand;

    private HandModel handModel;

    //grabbing attributes
    [Range(0f, 1f)]
    public float minimalGrabbingStrength = 0.5f;

    private float currentGrabbingStrength;
    private float previousGrabbingStrength;

    [SerializeField]
    public bool IsGrabbing
    {
        get { return currentGrabbingStrength > minimalGrabbingStrength; }
    }

    //visible default leap hand model attributes
    public KeyCode visibleHandKey = KeyCode.V;

    public bool defaultVisibleHand = false;
    private bool visibleHand;

    //intermediate positions/orientations
    private Vector3 previousPosition;

    private Vector3 previousOrientation;

    public override void Start()
    {
        base.Start();
        Position = Vector3.zero;
        previousPosition = Vector3.zero;
        visibleHand = defaultVisibleHand;
    }

    protected override void UpdateTracker()
    {
        previousPosition = Position;
        previousOrientation = Orientation;
        previousGrabbingStrength = currentGrabbingStrength;

        //get the rightmost hand in the frame
        if (handController.GetAllGraphicsHands().Length != 0)
        {
            handModel = handController.GetAllGraphicsHands()[0];
            handModel.transform.GetComponentInChildren<SkinnedMeshRenderer>().enabled = visibleHand;
            hand = handModel.GetLeapHand();
            currentGrabbingStrength = lowPassFilter(hand.GrabStrength, previousGrabbingStrength);

            Position = lowPassFilter(handModel.GetPalmPosition(), previousPosition);
            Orientation = lowPassFilter(handModel.GetPalmDirection(), previousOrientation);
        }

        //mask/display the graphical hand on key down
        if (Input.GetKeyDown(visibleHandKey))
        {
            var smr = handModel.transform.GetComponentInChildren<SkinnedMeshRenderer>();
            visibleHand = !visibleHand;
        }

        Translation = Position - previousPosition;
        Rotation = previousOrientation - Orientation;
    }

    protected override void UpdateButtons() //non utilisé
    { }
}