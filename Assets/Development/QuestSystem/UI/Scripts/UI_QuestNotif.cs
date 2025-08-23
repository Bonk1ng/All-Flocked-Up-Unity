using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestNotif : MonoBehaviour
{

    [SerializeField] private GameObject questNotifCanvas;
    [SerializeField] private TextMeshProUGUI questNotifText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questNotifCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private async void ShowQuestNotif()
    {
        questNotifCanvas.SetActive(true);
        await Task.Delay(3000);
        questNotifCanvas.SetActive(false);
    }
}
