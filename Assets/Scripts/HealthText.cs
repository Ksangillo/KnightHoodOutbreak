using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HealthText : MonoBehaviour
{
    public Vector3 textSpeed = new Vector3(0, 75, 0);
    public float fadeTime = 1f;
    private float elapsedTime;
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    private void Update()
    {
        textTransform.position += textSpeed * Time.deltaTime;


        elapsedTime += Time.deltaTime;
        //simliar to fade away behaviour sript used here to avoid adding more animations and controllers
        float fadeAlpha = startColor.a * (1 - (elapsedTime / fadeTime));
        if (elapsedTime < fadeTime)
        {
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
