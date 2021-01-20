using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int coinsCount;
    public string coinTag = "Coin";

    public static Inventory Instance { get; set; }


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Trying to create multiple instances of a singleton " + transform.gameObject.name);
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(coinTag))
        {
            coinsCount++;
            Destroy(collider.gameObject);
        }
    }
}
