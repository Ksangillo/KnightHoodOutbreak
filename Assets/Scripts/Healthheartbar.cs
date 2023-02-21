using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Healthheartbar : MonoBehaviour
{
    public GameObject heartPrefab;
    public DamageScript playerHealth;
    
    List<HealthHearts> hearts = new List<HealthHearts>();

    private void OnEnable()
    {
        DamageScript.OnDamage += GenerateHearts;
        DamageScript.OnHeal += GenerateHearts;//updates the hearts on screen

    }

    private void OnDisable()
    {
        DamageScript.OnDamage -= GenerateHearts;
        DamageScript.OnHeal -= GenerateHearts;
    }

    public void Awake()
    {
        GenerateHearts();
    }
    

    public void GenerateHearts()
    {
        ClearHearts();//startfresh

        //detemine how many hearts to make from the max HP
        float maxHealthRemain = playerHealth.maxHP % 2;
        int heartMake = (int)((playerHealth.maxHP / 2) + maxHealthRemain);
        for (int i = 0; i <heartMake; i++)
        {
            GenerateEmptyHeart();
        }

        for (int i = 0; i < hearts.Count; i++)//going through each heart
        {
            int heartStatRemain = Mathf.Clamp(playerHealth.HP - (i * 2), 0, 2);//determine the heart status
            hearts[i].SetHeart((HeartCondition)heartStatRemain);
        }
    }

    public void GenerateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
       
        

        HealthHearts heartComp = newHeart.GetComponent<HealthHearts>();
        heartComp.SetHeart(HeartCondition.Empty);
        hearts.Add(heartComp);
    }


    

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHearts>();
    }



}
