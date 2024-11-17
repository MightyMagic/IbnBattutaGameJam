using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] float bumpForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            Debug.LogError("Colliding with player!");

            // Remove from inventory

            // Push player opposite direction
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            Vector3 directionOfBump;

            if (other.transform.position.x > this.transform.position.x)
            {
                directionOfBump = rb.transform.right * bumpForce;
                rb.AddForce(directionOfBump * bumpForce, ForceMode.Impulse);

            }
            else
            {
                directionOfBump = -rb.transform.right * bumpForce;
                rb.AddForce(directionOfBump * bumpForce, ForceMode.Impulse);
            }

        }
    }
}
