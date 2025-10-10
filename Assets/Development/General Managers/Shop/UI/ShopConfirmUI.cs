using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopConfirmUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    public ShopLocation shopLocation;
    public ShopItem currentItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nameText.SetText(currentItem.itemName);
        descriptionText.SetText(currentItem.itemDescription);
        costText.SetText(currentItem.cost.ToString());
        confirmButton.onClick.AddListener(BuyItem);
        cancelButton.onClick.AddListener(CloseWindow);
        leftButton.onClick.AddListener(shopLocation.MovePlayerToNextSlotLeft);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BuyItem()
    {

    }

    private void CloseWindow()
    {

    }
}
