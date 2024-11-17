using System.Collections.Generic;
using UnityEngine;

public class BorderMovement : MonoBehaviour
{
    [SerializeField] List<GameObject> mountains;

    [SerializeField] float speedOfMountains;

    [SerializeField] Transform furtherPosition;
    [SerializeField] Transform markToSwap;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SwapMountains();
    }

    private void Move()
    {
        for(int i = 0; i < mountains.Count; i++)
        {
            mountains[i].transform.position -= new Vector3(0f, 0f, speedOfMountains * Time.deltaTime);
        }
    }

    void SwapMountains()
    {
        for (int i = 0; i < mountains.Count; i++)
        {
            if(mountains[i].transform.position.z < markToSwap.position.z)
            {
                mountains[i].transform.position = furtherPosition.position;
            }
        }
    }
}
