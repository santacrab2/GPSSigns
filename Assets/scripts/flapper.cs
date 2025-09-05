using UnityEngine;

public class flapper : MonoBehaviour
{
    public Transform pivotObject; // Assign an empty GameObject or another object as the pivot
    public float rotationSpeed = 50f; // Adjust rotation speed as needed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         if (pivotObject != null)
            {
                // Rotate the wing around the pivotObject's position along the Y-axis
                transform.RotateAround(pivotObject.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
    }
}
