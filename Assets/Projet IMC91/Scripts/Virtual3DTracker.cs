using System.Collections;
using UnityEngine;

/// <summary>
/// Abstract base class for all 3D trackers (+ buttons)
/// </summary>
public abstract class Virtual3DTracker : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Low Pass Filtering Constant (0 = no filtering)")]
    [Range(0f, 1f)]
    private float RC = 0f;

    public Vector3 Position
    {
        get;
        protected set;
    }

    public Vector3 Orientation
    {
        get;
        protected set;
    }

    public Vector3 Translation
    {
        get;
        protected set;
    }

    public Vector3 Rotation
    {
        get;
        protected set;
    }

    //buttons
    public bool[] buttonsState;

    public bool[] onoffButtonsState;

    public virtual void Start()
    {
        Position = new Vector3();
        Orientation = new Vector3();
        Translation = new Vector3();
        Rotation = new Vector3();
    }

    public void Update()
    {
        UpdateTracker();
        UpdateButtons();
    }

    protected abstract void UpdateTracker();

    protected abstract void UpdateButtons();

    //Basic Filters
    protected Vector3 lowPassFilter(Vector3 currentRawPosition, Vector3 previousFilteredPosition)
    {
        float a = Time.deltaTime / (RC + Time.deltaTime);
        return ((1 - a) * previousFilteredPosition + a * currentRawPosition);
    }

    protected float lowPassFilter(float currentRawPosition, float previousFilteredPosition)
    {
        float a = Time.deltaTime / (RC + Time.deltaTime);
        return ((1 - a) * previousFilteredPosition + a * currentRawPosition);
    }
}