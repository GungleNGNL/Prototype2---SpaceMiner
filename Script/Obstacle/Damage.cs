using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            var target = other.transform.root.gameObject;
            target.SendMessage("Destroy");
        }
        if(other.tag == "Player")
        {
            Debug.Log("hit player");
        }
    }
}
