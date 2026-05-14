using UnityEngine;

public class CarMovementReverse : MonoBehaviour
{
    [Header("Speed Settings")]
    public float normalSpeed = 8f;
    public float slowSpeed   = 2f;
    public float creepSpeed  = 1f;

    [Header("Loop Settings")]
    public float roadStartX = -100f; // left side spawn
    public float roadEndX   =  120f; // right side loop back

    [Header("Stop Point")]
    public Transform stopPoint;
    public float stopDistance = 10f;

    [Header("Car Ahead")]
    public Transform carAhead;
    public float safeFollowDistance = 8f;

    private float currentSpeed;
    private bool isStopped  = false;
    private bool isPastStop = false;
    private float fixedY;
    private float fixedZ;

    void Start()
    {
        currentSpeed = normalSpeed;
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void Update()
    {
        string light = TrafficLight.CurrentState;

        // Lock Y and Z always
        transform.position = new Vector3(
            transform.position.x, fixedY, fixedZ);

        // Distance to stop point
        float distToStop = 999f;
        if (stopPoint != null)
            distToStop = Vector3.Distance(
                transform.position, stopPoint.position);

        // Distance to car ahead
        float distToCarAhead = 999f;
        if (carAhead != null)
            distToCarAhead = Vector3.Distance(
                transform.position, carAhead.position);

        // Car crossed stop line
        if (distToStop < 1f)
            isPastStop = true;

        // GREEN - full speed
        if (light == "Green")
        {
            isStopped  = false;
            isPastStop = false;

            if (distToCarAhead > safeFollowDistance)
                currentSpeed = Mathf.MoveTowards(
                    currentSpeed, normalSpeed,
                    Time.deltaTime * 4f);
            else
                currentSpeed = Mathf.MoveTowards(
                    currentSpeed, 0f,
                    Time.deltaTime * 6f);
        }

        // YELLOW after GREEN - slow down
        else if (light == "Yellow" && !isPastStop && !isStopped)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed, slowSpeed,
                Time.deltaTime * 3f);

            if (distToStop <= stopDistance)
            {
                isStopped    = true;
                currentSpeed = 0f;
            }
        }

        // RED - stop
        else if (light == "Red" && !isPastStop)
        {
            if (distToStop <= stopDistance * 4f)
                currentSpeed = Mathf.MoveTowards(
                    currentSpeed, 0f,
                    Time.deltaTime * 5f);

            if (distToStop <= stopDistance)
            {
                isStopped    = true;
                currentSpeed = 0f;
            }
        }

        // YELLOW after RED - creep forward
        if (light == "Yellow" && isStopped)
        {
            isStopped    = false;
            currentSpeed = creepSpeed;
        }

        // Avoid crashing into car ahead
        if (distToCarAhead <= safeFollowDistance && !isStopped)
        {
            currentSpeed = Mathf.MoveTowards(
                currentSpeed, 0f,
                Time.deltaTime * 8f);

            if (distToCarAhead <= safeFollowDistance * 0.4f)
            {
                isStopped    = true;
                currentSpeed = 0f;
            }
        }

        // Move car in POSITIVE X direction (opposite lane)
        if (!isStopped)
            transform.position = new Vector3(
                transform.position.x + (currentSpeed * Time.deltaTime),
                fixedY, fixedZ);

        // Loop back to start
        if (transform.position.x >= roadEndX)
        {
            transform.position = new Vector3(
                roadStartX, fixedY, fixedZ);
            currentSpeed = normalSpeed;
            isStopped    = false;
            isPastStop   = false;
        }
    }
}