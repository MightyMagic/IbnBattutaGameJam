using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    [SerializeField] InputCapture inputCapture;
    [SerializeField] GameObject modelObject;

    [Header("Movement values")]
    [SerializeField] float maxAngle;
    [SerializeField] float sideSpeed;
    [SerializeField] float sideAcceleration;
    [SerializeField] float angularSpeed;

    

    void Start()
    {
        
    }

    void Update()
    {
        inputCapture.GatherInput();
        SteerShip(inputCapture.Steer);
        RotateModel(inputCapture.Steer);
        
    }

    void SteerShip(float steer)
    {
        //rb.linearVelocity = new Vector3(steer * sideSpeed, rb.linearVelocity.y, rb.linearVelocity.z);

        if(Mathf.Abs(rb.linearVelocity.magnitude) < sideSpeed)
        {
            rb.linearVelocity += new Vector3(steer * sideAcceleration * Time.deltaTime, rb.linearVelocity.y, rb.linearVelocity.z);

        }
    }

    void RotateModel(float steer)
    {
        // Get the current Y-axis rotation in degrees
        float currentYRotation = modelObject.transform.localRotation.eulerAngles.y;

        // Convert to a range of -180 to 180 for better handling
        if (currentYRotation > 180f)
            currentYRotation -= 360f;

        // Determine the target rotation based on steer input
        float targetRotation = currentYRotation + steer * angularSpeed * Time.deltaTime;

        // Clamp the rotation to the maximum allowed angle
        targetRotation = Mathf.Clamp(targetRotation, -maxAngle, maxAngle);

        // If steer is low, rotate back towards zero (center position)
        //if (Mathf.Abs(steer) < 0.1f)
        //{
        //    targetRotation = Mathf.MoveTowards(currentYRotation, 0f, angularSpeed * Time.deltaTime);
        //}

        // Apply the calculated rotation
        modelObject.transform.localRotation = Quaternion.Euler(modelObject.transform.localRotation.eulerAngles.x, targetRotation, modelObject.transform.localRotation.eulerAngles.z);
    }

    
}
