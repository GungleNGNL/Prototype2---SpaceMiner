using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResource : MonoBehaviour
{    
    [SerializeField]Transform player;
    [SerializeField] int _resPoint;
     int resPoint
    {
        get
        {
            return _resPoint;
        }
        set
        {
            if (value <= 0)
            {
                _resPoint = value;
                selfDestroy();
            }
            else
            {
                _resPoint = value;
            }
        }
    }
    [SerializeField] int originalRes;
    List<Mining> miner;
    [SerializeField] Vector3 originalScale;

    [SerializeField] float speed;
    Vector2 spawnerDir;
    [SerializeField] MoveDirection dirMode;
    BoxCollider col;

    private void Awake()
    {
        miner = new List<Mining>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        col = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        originalScale = transform.localScale;
        originalRes = resPoint;
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3) // boids
        {
            Mining miningboid = other.gameObject.GetComponent<Mining>();
            if(miningboid != null)
            {
                miner.Add(miningboid);
                miningboid.StartMine(this);
            }
        }
    }

    enum MoveDirection
    {
        noDir = 0,
        Direct = 1,
    }

    public bool canMine(bool mine)
    {
        if (resPoint > 0)
        {
            if(mine)
            resPoint--;
            //rescale();
            return true;
        }
        else
        {
            return false;
        }
    }

    void rescale()
    {
        float targetScale = (float)resPoint / originalRes;
        if (targetScale < 0.4f)
            targetScale = 0.4f;
        transform.localScale = targetScale * originalScale;
        col.size = new Vector3(2, 2, 2) * (1 / targetScale);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3) // boids
        {
            Mining miningboid = other.gameObject.GetComponent<Mining>();
            if(miningboid != null)
            {
                miningboid.stopMine();
                miner.Remove(miningboid);
            }
        }
    }

    private void selfDestroy()
    {
        foreach(Mining m in miner)
        {
            m.stopMine();
        }
        Destroy(gameObject);
    }

    public void BoidDestroy(Boid target)
    {       
        miner.Remove(target.GetComponent<Mining>());
    }
    
}
