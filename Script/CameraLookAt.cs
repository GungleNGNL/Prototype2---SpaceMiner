using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    Transform player;
    [SerializeField] Vector3 offset;
    Vector3 velocity = Vector3.zero;
    [SerializeField] float smoothTime = 0.25f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void FixedUpdate()
    {
        Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
        transform.LookAt(player);
    }
}
