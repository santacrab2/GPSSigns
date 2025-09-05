using UnityEngine;

public class mover : MonoBehaviour
{
    [Tooltip("The speed at which the object moves.")]
    public float moveSpeed = 1.5f;

    [Tooltip("The maximum distance from the starting position the object will fly.")]
    public float movementRadius = 3.0f;

    [Tooltip("The time in seconds to wait before choosing a new random destination.")]
    public float newDestinationTime = 3.0f;

    private Vector3 initialPosition;
    private Vector3 currentTargetPosition;
    private float timeToNewDestination;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Store the starting position relative to its parent (the AR space).
        initialPosition = transform.localPosition;
        timeToNewDestination = newDestinationTime;
        SetNewRandomPosition();

    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the target position.
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentTargetPosition, moveSpeed * Time.deltaTime);

        // If close to the target, choose a new one.
        if (Vector3.Distance(transform.localPosition, currentTargetPosition) < 0.1f)
        {
            timeToNewDestination -= Time.deltaTime;
            if (timeToNewDestination <= 0)
            {
                SetNewRandomPosition();
                timeToNewDestination = newDestinationTime;
            }
        }
    }
    void SetNewRandomPosition()
    {
        // Generate a random position within a sphere around the initial position.
        Vector3 randomDirection = Random.insideUnitSphere * movementRadius;
        currentTargetPosition = initialPosition + randomDirection;
    }
}
