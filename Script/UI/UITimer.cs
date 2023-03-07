using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    float current = 0;
    float end;
    bool haveTime;
    Image image;
    Button button;

    public void SetTimer(float time)
    {
        if (time <= 0) return;
        current = 0;
        end = time;
        haveTime = true;
        button.interactable = false;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }


    void Update()
    {
        if (haveTime)
        {
            current += 1 * Time.deltaTime;
            image.fillAmount = current / end;
            if(current > end)
            {
                haveTime = false;
                image.fillAmount = 1;
                current = 0;
                button.interactable = true;
            }
        }
    }
}
