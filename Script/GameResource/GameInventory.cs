using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInventory : MonoBehaviour
{
    [SerializeField] int _inventoryLimit;
    int inventoryLimit
    {
        get
        {
            return _inventoryLimit;
        }
        set
        {
            if (value <= 0)
            {}
            else
            {
                _inventoryLimit = value;
            }
        }
    }
    int current;
    List<BoidBackpack> res;

    [SerializeField] float speed;
    Vector2 spawnerDir;
    [SerializeField] MoveDirection dirMode;

    private void Awake()
    {
        res = new List<BoidBackpack>();
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
            BoidBackpack boidInv = other.gameObject.GetComponent<BoidBackpack>();
            if(boidInv != null)
            {
                boidInv.StartStore(this);
                res.Add(boidInv);               
            }
        }
    }

    enum MoveDirection
    {
        noDir = 0,
        Direct = 1,
    }

    public bool CanStore(int num)
    {
        if (current + num <= inventoryLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int Store(int num)
    {
        int total = current + num;
        if (total > inventoryLimit)
        {
            GameManager.Instance.SetResPoint(total - current);
            current = inventoryLimit;           
            return total - inventoryLimit;
        }
        else
        {
            current += num;
            GameManager.Instance.SetResPoint(num);
            return 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3) // boids
        {
            BoidBackpack target = other.gameObject.GetComponent<BoidBackpack>();
            if(target != null)
            {
                res.Remove(target);
            }
        }
    }

    public void BoidDestroy(Boid target)
    {       
        res.Remove(target.GetComponent<BoidBackpack>());
    }    
}
