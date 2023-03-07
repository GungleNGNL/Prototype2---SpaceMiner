using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]float speed;
    Vector2 spawnerDir;
    [SerializeField] MoveDirection dirMode;

    public void Initialize(Vector2 spawnerDir)
    {
        this.spawnerDir = spawnerDir.normalized;
        switch (dirMode)
        {
            case MoveDirection.noDir:
                break;
            case MoveDirection.Direct:
                gameObject.GetComponent<Direct>().AddDate(speed, spawnerDir);
                break;
        }
    }

    enum MoveDirection
    {
        noDir = 0,
        Direct = 1,
    }
}
