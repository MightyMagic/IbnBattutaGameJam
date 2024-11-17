using UnityEngine;

public class FloatingRb : MonoBehaviour
{
    public Rigidbody rb;           // Reference to the Rigidbody
    public float floatAmplitude = 0.5f; // Vertical floating amplitude
    public float floatFrequency;   // Floating frequency (speed of up/down motion)
    public float zSpeed;          // Speed of forward motion along the z-axis

    private float initialY;            // Store the initial Y position


    float newY;
    float newZ;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        // Store the initial position
        initialY = transform.position.y;
    }

    private void Update()
    {
        FloatAndMove();
    }

    public void FloatAndMove()
    {

       // Calculate vertical (floating) motion
       newY = initialY + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        


      // Calculate forward (z-axis) motion
      //newZ = transform.position.z - zSpeed * Time.fixedDeltaTime;
        

        //rb.MovePosition(new Vector3(transform.position.x, newY, newZ));

        rb.MovePosition(new Vector3(transform.position.x, newY, transform.position.z));
    }
}
