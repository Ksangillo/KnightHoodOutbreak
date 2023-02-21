using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthHearts : MonoBehaviour
{
    public Sprite fullHP, halfHP, emptyHP;
    Image heart;
    
    private void Awake()
    {
        heart = GetComponent<Image>();
    }

    public void SetHeart(HeartCondition condition)
    {
        switch (condition)
        {
            case HeartCondition.Empty:
                heart.sprite = emptyHP;
                break;

            case HeartCondition.Half:
                heart.sprite = halfHP;
                break;

            case HeartCondition.Full:
                heart.sprite = fullHP;
                break;

        }
    }
  
}
public enum HeartCondition
{
    Empty = 0,
    Half = 1,
    Full = 2
        //can add quarters set i * 2 to   I * 4       0, 4
}