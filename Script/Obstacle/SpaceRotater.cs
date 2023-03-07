using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceRotater : MonoBehaviour
{
    float velocity;
    Vector3 rngRotate;
    Rigidbody rb;
    // Update is called once per frame
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        velocity = Random.Range(10, 15);
        rngRotate = Random.onUnitSphere;
    }
    void FixedUpdate()
    {
        rb = GetComponent<Rigidbody>();
        //transform.Rotate(rngRotate, velocity * Time.deltaTime);
        rb.angularVelocity = rngRotate * velocity * Time.fixedDeltaTime;
    }
}
