using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] float bumpForce;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        // Remove from inventory
    //
    //        // Push player opposite direction
    //        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
    //
    //        Vector3 collisionNormal = collision.contacts[0].normal;
    //        collisionNormal = new Vector3(-collisionNormal.x, 0f, 0f);
    //
    //        rb.AddForce(collisionNormal * bumpForce, ForceMode.Impulse);
    //    }
    //}

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
                directionOfBump = - rb.transform.right * bumpForce;
                rb.AddForce(directionOfBump * bumpForce, ForceMode.Impulse);
            }

        }
    }
}
