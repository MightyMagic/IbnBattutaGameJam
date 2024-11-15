using UnityEngine;

public class InputCapture: MonoBehaviour
{
    public float Steer;
    public bool BoostPressed;
    public bool BoostHeld;
    public bool BoostReleased;


    public void GatherInput()
    {
        Steer = Input.GetAxis("Horizontal");
        BoostPressed = Input.GetButtonDown("Jump");
        BoostHeld = Input.GetButton("Jump");
        BoostReleased = Input.GetButtonUp("Jump");

    }
}
