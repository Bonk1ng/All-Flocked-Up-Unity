using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SaveSlot : MonoBehaviour
{
    [SerializeField] private SaveSlotInfo data;
    public SaveData saveData;
    private SaveSlotManager slotManager;
    public Image saveImage;
    public TextMeshProUGUI playerNameText;
    public TextMeshProUGUI lastSaveText;
    public Button saveLoadButton;

    private SaveSlotInfo slotInfo;
    private UI_SaveWindow saveWindow;
    public bool isSaving;

    private void CheckSaveLoad()
    {
        if (!isSaving)
        {
            CallLoad();
        }else { CallSave(); }
    }

    public void Init(SaveSlotInfo info, SaveSlotManager manager, UI_SaveWindow window, bool saving = true)
    {
        slotInfo = info;
        slotManager = manager;
        saveWindow = window;
        isSaving = saving;

        UpdateSlotUI();

        saveLoadButton.onClick.AddListener(() =>
        {
            if (isSaving && slotInfo.exists)
            {
                // Confirm overwrite
                saveWindow.OpenConfirmWindow(this);
            }
            else
            {
                CheckSaveLoad();
            }
        });
    }
    private void GetSlot()
    {

    }

    private void CallSave()
    {
        SaveData data = FindFirstObjectByType<PlayerSaveLoadHandler>().saveData;
        SaveSlotManager.SaveToSlot(slotInfo.slotIndex, data, true);
        UpdateSlotUI();
    }

    private void CallLoad()
    {
        SaveData loadedData = SaveSlotManager.LoadFromSlot(slotInfo.slotIndex, true);
    }

    private void UpdateSlotUI()
    {
        playerNameText.text = slotInfo.exists ? slotInfo.playerName : "Empty Slot";
        lastSaveText.text = slotInfo.exists ? slotInfo.lastSaved.ToString() : "--/--/--";
    }

}
