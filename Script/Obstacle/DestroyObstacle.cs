using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6 || other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
    }
}
