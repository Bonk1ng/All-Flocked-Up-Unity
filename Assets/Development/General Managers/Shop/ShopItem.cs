using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class ShopItem : MonoBehaviour
{
    public GameObject item;
    public int cost;

    void Awake()
    {
        item = this.gameObject;
    }
}
