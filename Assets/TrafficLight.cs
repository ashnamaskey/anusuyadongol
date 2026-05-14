using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [Header("This light's colored parts")]
    public Renderer redPart;
    public Renderer yellowPart;
    public Renderer greenPart;

    [Header("Is this the MASTER light?")]
    public bool isMaster = false;

    [Header("Durations (only master uses these)")]
    public float greenDuration  = 8f;
    public float yellowDuration = 2f;
    public float redDuration    = 8f;

    // Shared across ALL traffic lights
    public static string CurrentState = "Green";

    private float timer = 0f;

    // These are the actual material colors
    // of your traffic light model
    private Color redOn    = new Color(1f,   0.1f, 0f);
    private Color yellowOn = new Color(1f,   0.8f, 0f);
    private Color greenOn  = new Color(0f,   1f,   0f);
    private Color lightOff = new Color(0.1f, 0.1f, 0.1f);

    void Start()
    {
        ApplyColor(CurrentState);
    }

    void Update()
    {
        if (isMaster)
        {
            timer += Time.deltaTime;

            if (CurrentState == "Green" && timer >= greenDuration)
                ChangeState("Yellow");

            else if (CurrentState == "Yellow" && timer >= yellowDuration)
                ChangeState("Red");

            else if (CurrentState == "Red" && timer >= redDuration)
                ChangeState("Green");
        }

        // Follower always copies master every frame
        ApplyColor(CurrentState);
    }

    void ChangeState(string newState)
    {
        CurrentState = newState;
        timer        = 0f;
    }

    void ApplyColor(string state)
    {
        // First turn everything off
        SetColor(redPart,    lightOff);
        SetColor(yellowPart, lightOff);
        SetColor(greenPart,  lightOff);

        // Then turn on only the active one
        if (state == "Red")    SetColor(redPart,    redOn);
        if (state == "Yellow") SetColor(yellowPart, yellowOn);
        if (state == "Green")  SetColor(greenPart,  greenOn);
    }

    void SetColor(Renderer part, Color color)
    {
        if (part == null) return;

        // This changes ALL materials on the part
        // in case it has more than one material
        Material[] mats = part.materials;
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].color = color;
        }
        part.materials = mats;
    }
}