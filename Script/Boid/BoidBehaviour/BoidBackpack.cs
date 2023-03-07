using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBackpack : MonoBehaviour
{
    [SerializeField] int resPoint;
    [SerializeField] int Limit;
    [SerializeField] float storeSpeed = 1;
    GameInventory target;
    float timer = 0;
    float time = 1;
    bool isCount;
    //bool canStore;
    public void AddRes(int num)
    {
        resPoint += num;
    }

    public bool isBackpackFull()
    {
        return resPoint >= Limit;
    }

    public void StartStore(GameInventory target)
    {
        this.target = target;
        if (!(target.CanStore(resPoint) || resPoint > 0))
        {
            EndStore();
            return;
        }
        isCount = true;
    }

    public void EndStore()
    {
        Debug.Log("back");
        GetComponent<Boid>().setTarget(null);
        target = null;
    }

    private void Update()
    {
        if (isCount)
        {
            timer += 1 * Time.deltaTime;
            if (timer > time)
            {
                timer = 0;
                isCount = false;
                Store();
            }
        }
    }

    void Store()
    {
        if (!target.CanStore(resPoint))
        {
            EndStore();
            return;
        }

        resPoint = target.Store(resPoint);
        EndStore();
    }
}
