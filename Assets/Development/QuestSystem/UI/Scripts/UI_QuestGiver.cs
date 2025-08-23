using UnityEngine;
using UnityEngine.UI;

public class UI_QuestGiver : MonoBehaviour
{

    [SerializeField] GameObject questGiverCanvas;
    [SerializeField] QuestGiver questGiver;
    public QuestLog questLog;

    [SerializeField] Button acceptQuestButton;
    [SerializeField] Button cancelButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questGiverCanvas.SetActive(false);
        acceptQuestButton.onClick.AddListener(AddQuestToLog);
    }


    public void OpenQuestGiverUI()
    {
        questGiverCanvas.SetActive(true);
        
    }

    public void CloseQuestGiverUI()
    {
        questGiverCanvas.SetActive(false);
    }

    private void AddQuestToLog()
    {
      questGiver.InteractWithNPC(questLog);
        CloseQuestGiverUI();
    }
}
