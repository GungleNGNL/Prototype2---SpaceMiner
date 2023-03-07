using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    private static ResourceSpawner _resourceSpawner;
    public static ResourceSpawner Instance
    {
        get
        {
            return _resourceSpawner;
        }
    }
    [SerializeField] Transform objParent;
    [SerializeField] Transform[] spawnPos;
    [SerializeField] Vector2 spawnRange;

    [SerializeField] GameObject[] ResPrefabs;
    [SerializeField] GameObject[] itemPrefabs;

    [SerializeField] float spawnSeq;
    [SerializeField] bool canSpawn;
    private void Awake()
    {
        if(Instance == null)
        {
            _resourceSpawner = this;
        }
        int spawnerNum = transform.childCount;
        spawnPos = new Transform[spawnerNum];
        for (int i = 0; i < spawnerNum; i++)
        {
            spawnPos[i] = transform.GetChild(i);
        }            
    }

    private void Start()
    {
        canSpawn = true;
        StartCoroutine(spawnRes());
    }

    IEnumerator spawnRes()
    {
        while (!GameManager.Instance.isGameOver())
        {
            int spawnerIndex = Random.Range(0, spawnPos.Length - 1);
            Transform targetSpawner = spawnPos[spawnerIndex];
                          
            var obj = Instantiate(ResPrefabs[Random.Range(0, ResPrefabs.Length)], targetSpawner);
            obj.transform.SetParent(objParent);
            obj.transform.position += new Vector3Int(Random.Range((int)-spawnRange.x, (int)spawnRange.x), Random.Range((int)-spawnRange.y, (int)spawnRange.y), 0);
            if (obj.GetComponent<GameResource>() != null)
                obj.GetComponent<GameResource>().Initialize(targetSpawner.forward);
            else
                obj.GetComponent<GameInventory>().Initialize(targetSpawner.forward);
            yield return new WaitForSeconds(spawnSeq);
        }      
    }

    public void spawnItem(int index)
    {
        int spawnerIndex = Random.Range(0, spawnPos.Length - 1);
        Transform targetSpawner = spawnPos[spawnerIndex];

        var obj = Instantiate(itemPrefabs[index], targetSpawner);
        obj.transform.position += new Vector3Int(Random.Range((int)-spawnRange.x, (int)spawnRange.x), Random.Range((int)-spawnRange.y, (int)spawnRange.y), 0);
        obj.transform.SetParent(objParent);
        obj.GetComponent<GameInventory>().Initialize(targetSpawner.forward);
        canSpawn = false;
        Invoke("spawnCD", 2.0f);
    }

    void SpawnCD()
    {
        canSpawn = true;
    }
}
