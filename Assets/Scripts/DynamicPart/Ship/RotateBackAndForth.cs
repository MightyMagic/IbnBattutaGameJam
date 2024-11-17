using UnityEngine;

public class RotateBackAndForth : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float maxAngle = 30f; // Maximum angle to rotate in either direction
    [SerializeField] private float speed = 2f;    // Speed of the rotation

    //[SerializeField] float yCoeff;

    private float currentAngle = 0f; // Tracks the current angle of rotation
    private bool rotatingForward = true; // Tracks rotation direction

    [SerializeField] GameObject modelObject;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float rotationStep = speed * Time.deltaTime;
        if (rotatingForward)
        {
            currentAngle += rotationStep;
            if (currentAngle >= maxAngle)
            {
                currentAngle = maxAngle;
                rotatingForward = false;
            }
        }
        else
        {
            currentAngle -= rotationStep;
            if (currentAngle <= -maxAngle)
            {
                currentAngle = -maxAngle;
                rotatingForward = true;
            }
        }

        modelObject.transform.localRotation = Quaternion.Euler(currentAngle, modelObject.transform.localRotation.eulerAngles.y, modelObject.transform.localRotation.eulerAngles.z);
    }
}
