using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public GameObject item;
    public string itemName;
    public string itemDescription;
    public int cost;

    void Awake()
    {
        item = this.gameObject;
    }
}
