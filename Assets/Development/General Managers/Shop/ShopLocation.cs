using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class ShopLocation : ShopManager
{
    [SerializeField] protected Dictionary<GameObject, int> currentItemList = new();
    [SerializeField] protected List<ShopItem> items = new();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        SpawnItemsInSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if(resetFlag)UpdateItemInventory();
    }
    protected void SpawnItemsInSlots()
    {
        foreach (var slot in slots)
        {
            var randomIndex = Random.Range(0, base.itemDictionary.Count);
            var item = base.itemDictionary.ElementAt(randomIndex);
            var spawnedItem = Instantiate(item.Key).GetComponent<ShopItem>();
            currentItemList.Add(item.Key,item.Value);
            items.Add(spawnedItem);
            spawnedItem.transform.position = slot.transform.position;
            Debug.Log("ItemSpawned:" + item.Key);
        }
    }


    protected void MovePlayerToNextSlot()
    {

    }

    protected void BuyAndRemoveItem()
    {

    }


    protected void UpdateItemInventory()
    {
        foreach (var item in items)
        {
            if (item != null) continue;
            var randomIndex = Random.Range(0, base.itemDictionary.Count);
            var newItem = base.itemDictionary.ElementAt(randomIndex);
            var spawnedItem = Instantiate(newItem.Key);
            spawnedItem.transform.position = item.transform.position;


        }
    }


}
