using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    //[SerializeField] GameObject gameStarter, gameEnder;
    [SerializeField] float gameStartTimer;
    [SerializeField] int _distance;
    int distance
    {
        get
        {
            return _distance;
        }
        set
        {
            if(value > 0)
            {
                GameManager.Instance.setProcessUI(_distance);
                _distance = value;
            }
        }
    }

    bool over;

    [SerializeField] int des, res;
    private void Start()
    {
        Invoke("GameStart", gameStartTimer);
        over = false;
    }

    void GameStart()
    {
        //gameStarter.GetComponent<Direct>().
        //gameStarter.SetActive(true);
        Level current = GetComponent<LevelManager>().getLevel();
        des = current.desDistance;
        res = current.targetRes;
        StartCoroutine(GameProcessing());
        distance = 0;
    }

    void GameEnd()
    {
        //gameEnder.SetActive(true);
        over = true;
        StopCoroutine(GameProcessing());
        if (res > GameManager.Instance.getResPoint())
        {
            GameManager.Instance.GameLose();
        }
        else
        {
            GameManager.Instance.GameWin();
        }
    }

    IEnumerator GameProcessing()
    {
        while (!over)
        {
            if(distance >= des)
            {
                GameEnd();
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.GameOver();
            }
            yield return new WaitForSeconds(1);
            distance++;
        }     
    }
}
