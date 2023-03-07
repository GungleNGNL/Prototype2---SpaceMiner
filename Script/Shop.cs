using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public void BuyMiner()
    {
        if (!GameManager.Instance.SetResPoint(0))
        {
            return;
        }
        BoidManager.Instance.AddBoid();

    }

    public void SpawnInvObj()
    {
        if (!GameManager.Instance.SetResPoint(0))
        {
            return;
        }
        ResourceSpawner.Instance.spawnItem(0);
    }
}
