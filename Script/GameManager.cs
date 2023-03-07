using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;
    public static GameManager Instance
    {
        get { return _gameManager; }
        set { _gameManager = value; }
    }

    bool gameOver;
    public GameObject gameWin, gameLose;
    public TextMeshProUGUI BoidsNum, resNum;
    public Image processBar, resBar;
    public float maxProcessValue, processTarget, resTarget;
    private float _maxResValue;
    public float maxResValue
    {
        get
        {
            return _maxResValue;
        }
        set
        {
            _maxResValue = value;
            setResUI();
        }
    }

    public int ControlLimit;

    //-----------------Game Data(player)--------------------
    [Header("Player Data")]
    [SerializeField] private int _resPoint = 0;
    int resPoint
    {
        set
        {
            _resPoint = value;
            setResUI();
        }
        get
        {
            return _resPoint;
        }
    } 


    private void Awake()
    {
        if(_gameManager == null)
            Instance = this;
        gameOver = false;
    }

    void Start()
    {
        Application.targetFrameRate = 80;
    }

    public int followNum {
        set => BoidsNum.text = "" + value;
    }

    public void setResUI()
    {
        resNum.text = "" + resPoint + "/" + maxResValue;
        float value = resPoint / maxResValue;
        if (value > 1)
            resTarget = 1;
        else
            resTarget = value;
    }

    public void setProcessUI(int amount)
    {
        //processBar.fillAmount = (float)amount / maxProcessValue;
        processTarget = (float)amount / maxProcessValue;
        //processBar.fillAmount = Mathf.Lerp(processBar.fillAmount, processTarget, )
    }

    public int getResPoint()
    {
        return resPoint;
    }

    public bool SetResPoint(int num)
    {
        if(resPoint + num < 0)
        {
            return false;
        }
        resPoint += num;
        return true;
    }

    public bool isGameOver()
    {
        return gameOver;
    }

    public void GameOver()
    {
        gameOver = true;
    }

    public void GameWin()
    {
        gameWin.SetActive(true);
    }

    public void GameLose()
    {
        gameLose.SetActive(true);
    }

    public void NewGame()
    {
        gameLose.SetActive(false);
        gameWin.SetActive(false);
    }

    public void Restart()//Test only
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Update()
    {
            resBar.fillAmount = Mathf.Lerp(resBar.fillAmount, resTarget, 3.0f * Time.deltaTime);
            processBar.fillAmount = Mathf.Lerp(processBar.fillAmount, processTarget, 3.0f * Time.deltaTime);
    }
}
