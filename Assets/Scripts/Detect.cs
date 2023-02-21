using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    //detects a collider and appears in the list
    public List<Collider2D> detectColliders = new List<Collider2D>();
    Collider2D col2D;
   
   private void Awake()
    {
        col2D = GetComponent<Collider2D>();
    }
    //keeps track of the detect box's zone
    private void OnTriggerEnter2D(Collider2D col)
    {
        detectColliders.Add(col);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        detectColliders.Remove(col);
    }
}
