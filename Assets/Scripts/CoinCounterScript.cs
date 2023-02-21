using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounterScript : MonoBehaviour
{
    public static CoinCounterScript instance;

    public TMP_Text coinText;
    public int currentCoins = 0;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "SCORE: " + currentCoins.ToString();
    }

   public void IncreaseCoins(int v)
    {
        currentCoins += v;
        coinText.text = "SCORE: " + currentCoins.ToString();
    }
}
