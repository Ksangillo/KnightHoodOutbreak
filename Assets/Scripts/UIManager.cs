using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    public GameObject textDamageprefab;
    public GameObject textHealthprefab;
    public Canvas UICanvas;

    private void Awake()
    {
        //finds the first object that is in the canvas
        UICanvas = FindObjectOfType<Canvas>();
       
    }
    private void OnEnable()//allows to listen when they are called mutiple times
    {
        CharacterEvents.damagedCharacter += CharacterTextDamage;
        CharacterEvents.healCharacter += healingDamage;
    }
    private void OnDisable()
    {
        CharacterEvents.damagedCharacter -= CharacterTextDamage;
        CharacterEvents.healCharacter -= healingDamage;
    }
    public void CharacterTextDamage(GameObject character, int damageTaken)
    {
        //finds the spawn position and Creates the text location for when  damage is Taken
        Vector3 positionSpawn = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpDamageText = Instantiate(textDamageprefab, positionSpawn, Quaternion.identity, UICanvas.transform).GetComponent<TMP_Text>();
        tmpDamageText.text = damageTaken.ToString();
    }

    public void healingDamage(GameObject character, int healTaken)
    {
        //Future Healing mechanics
        Vector3 positionSpawn = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpHealhText = Instantiate(textHealthprefab, positionSpawn, Quaternion.identity, UICanvas.transform).GetComponent<TMP_Text>();
        tmpHealhText.text = healTaken.ToString();
    }
}
