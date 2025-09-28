using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerWingventory : MonoBehaviour
{
    public int playerTrinketQuantity = 0;
    [SerializeField] private Dictionary<GameObject,int> inventory = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddItemToInv(GameObject item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] += quantity;
        }
        else inventory.Add(item, quantity);
    }

    private void RemoveItemFromInv(GameObject item, int quantity)
    {
        if (inventory.ContainsKey(item))
        {
            inventory[item] -= quantity;
        }
        else inventory.Remove(item);
    }

    private void UpdateInv()
    {


    }

    
}
