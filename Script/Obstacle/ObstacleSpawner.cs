using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] Transform objParent;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] Vector2 spawnRange;

    [SerializeField] GameObject[] ObstaclePrefabs;
    [SerializeField] GameObject[] lv2_ObstaclePrefabs;

    [SerializeField] float spawnSeq;

    private void Awake()
    {
        int spawnerNum = transform.childCount;
        spawnPos = new Transform[spawnerNum];
        for (int i = 0; i < spawnerNum; i++)
        {
            spawnPos[i] = transform.GetChild(i);
        }            
    }

    private void Start()
    {
        StartCoroutine(spawnObstacle());
    }

    IEnumerator spawnObstacle()
    {
        while (!GameManager.Instance.isGameOver())
        {
            int spawnerIndex = Random.Range(0, spawnPos.Length - 1);
            Transform targetSpawner = spawnPos[spawnerIndex];
                          
            var obj = Instantiate(ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)], targetSpawner);
            obj.transform.SetParent(objParent);
            float scaler = Random.Range(0.5f, 1.0f);
            obj.transform.position += new Vector3Int(Random.Range((int)-spawnRange.x, (int)spawnRange.x), Random.Range((int)-spawnRange.y, (int)spawnRange.y), 0);
            obj.transform.localScale *= scaler;
            obj.GetComponent<Obstacle>().Initialize(targetSpawner.forward);       
            yield return new WaitForSeconds(spawnSeq);
        }      
    }
}
