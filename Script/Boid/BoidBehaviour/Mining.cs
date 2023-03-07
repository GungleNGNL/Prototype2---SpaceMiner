using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoidBackpack))]
public class Mining : MonoBehaviour
{
    [SerializeField] float mineSpeed = 1;
    BoidBackpack backpack;
    GameResource target;

    private void Awake()
    {
        backpack = GetComponent<BoidBackpack>();
    }

    public void StartMine(GameResource target)
    {
        if (!target.canMine(false))
        {
            goBack();
            return;
        }
        this.target = target;
        InvokeRepeating("Mine", 1 / mineSpeed, 1 / mineSpeed);
        Debug.Log("started mining");
    }

    public void Mine()
    {
        if (backpack.isBackpackFull())
        {
            goBack();
            return;
        }
        if(target.canMine(true))
            backpack.AddRes(1);
        else
            goBack();
    }

    public void stopMine()
    {
        if (this == null) return;
        CancelInvoke("Mine");
        goBack();
    }

    void goBack()
    {
        Debug.Log("back");
        GetComponent<Boid>().setTarget(null);
        target = null;
        CancelInvoke("Mine");
    }
}
