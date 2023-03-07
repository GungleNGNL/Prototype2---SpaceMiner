using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : MonoBehaviour
{
    float speed;
    Vector3 velocity;
    Vector2 spawnDir;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void AddDate(float speed, Vector2 dir)
    {
        spawnDir = dir;
        this.speed = speed;
        //rb.AddForce(rb.velocity);
        rb.velocity = spawnDir * speed;
    }

    private void FixedUpdate()
    {
        if(rb.velocity.magnitude < speed)
        {
            rb.velocity = rb.velocity.normalized * speed; //* 2 * Time.fixedDeltaTime;
        }
        //rb.velocity = spawnDir * speed;
        //transform.position += velocity * Time.fixedDeltaTime;
        //rb.AddForce(rb.velocity * Time.fixedDeltaTime);
    }
}
