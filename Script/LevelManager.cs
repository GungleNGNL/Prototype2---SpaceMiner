using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //[SerializeField] int diffculity;
    [SerializeField] int desDistance;
    [SerializeField] int targetRes;
    [SerializeField] int currentLevel = 0;
    private void Start()
    {
        newLevel();
    }

    public void newLevel()
    {
        currentLevel++;
        GameManager.Instance.maxProcessValue = desDistance;
        GameManager.Instance.maxResValue = targetRes;
    }
    
    public Level getLevel()
    {
        Level current = new Level(desDistance, targetRes);
        GameManager.Instance.maxProcessValue = current.desDistance;
        GameManager.Instance.maxResValue = current.targetRes;
        return current;
    }
}
[CreateAssetMenu(fileName = "Level")]
public class Level : ScriptableObject
{
    public int desDistance;
    public int targetRes;
    public float obSpawnRate;
    public float resSpawnRate;
    public int ResLevel;
    public int ObLevel;

    public Level(int desDistance, int targetRes)
    {
        this.desDistance = desDistance;
        this.targetRes = targetRes;
    }
}
