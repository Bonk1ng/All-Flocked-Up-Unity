using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SaveWindow : MonoBehaviour
{
    [SerializeField] private SaveSlotManager slotManager;
    [SerializeField] private RectTransform saveBox;
    [SerializeField] private GameObject confirmWindow;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private GameObject saveSlotPrefab;
    private UI_SaveSlot pendingSlot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        confirmButton.onClick.AddListener(CloseConfirmWindow);
        cancelButton.onClick.AddListener(CancelConfirmWindow);
        InitSaveBox();
    }

    private void InitSaveBox()
    {
        List<SaveSlotInfo> slots = SaveSlotManager.GetAllSlots();
        foreach (Transform child in saveBox)
            Destroy(child.gameObject);

        // Create a slot for each save file
        for (int i = 0; i < slots.Count; i++)
        {
            var obj = Instantiate(saveSlotPrefab, saveBox);
            var slotUI = obj.GetComponent<UI_SaveSlot>();
            slotUI.Init(slots[i], slotManager, this);
        }
    }


    public void OpenConfirmWindow(UI_SaveSlot slot)
    {
        pendingSlot = slot;
        confirmWindow.SetActive(true);
    }



    public void CloseConfirmWindow()
    {

    }

    public void CancelConfirmWindow()
    {
        confirmWindow.SetActive(false);
    }
}
