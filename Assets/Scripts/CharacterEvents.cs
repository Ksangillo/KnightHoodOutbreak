using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
public class CharacterEvents
{
    //character damaged and damage is valued
    public static UnityAction<GameObject, int> damagedCharacter;

    //character healed and healing is valued
    public static UnityAction<GameObject, int> healCharacter;




}
