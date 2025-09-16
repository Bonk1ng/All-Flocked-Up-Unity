using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using Unity.Mathematics;
using System.Threading.Tasks;
using static UnityEditor.Progress;
using UnityEngine.Events;
using UnityEditor.Experimental.GraphView;


public class UI_DialogueCanvas : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image dialogueImage;
    [SerializeField] private DialogueBase dialogueBase;
    [SerializeField] private ScrollRect responseBox;
    [SerializeField] private Button buttonPrefab;
    public int textSpeed = 1;
    public string[] responses;
    public string responseReturnID;
    private bool hasButtons = false;

    bool skipDialogue;
    public Action SkipLine { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueCanvas = GetComponent<Canvas>();
        //dialogueImage = GetComponent<Image>();
        dialogueCanvas.gameObject.SetActive(false);
        dialogueBase = FindFirstObjectByType<DialogueBase>();
        textSpeed=dialogueBase.textSpeed;

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            if (responses != null)
            {
                // ProgressDialogueCanvas();
            }
        }
    }

    public void UpdateDialogueUI(string name,string dialogue, Image image)
    {
        dialogueText.SetText(dialogue);
        nameText.SetText(name);
        dialogueImage.material = image.material;
    }

    public void ClearDialogueCanvas()
    {
        
        dialogueText.SetText("");
        nameText.SetText("");
        DestroyDialogue();
       
    }

    public void DestroyDialogue()
    {
        this.gameObject.SetActive(false);
    }


    public void ProgressDialogueCanvas()
    {
        dialogueBase.ProgressDialogue();
    }

    public async void TypeText()
    {
        foreach (var item in dialogueText.ToString().AsSpan().ToArray())
        {
            Debug.Log(item);
            await Task.Delay(500);
        }

    }

    public void GetResponseOptions()
    {
        responses = new string[dialogueBase.currentResponseOptions.Length];
        dialogueBase.currentResponseOptions.CopyTo(responses.AsSpan());
        Debug.Log("UISpawnResponseButtons");
        float startY = 0f;
        float offset = .2f;
        int index = 0;
        if (responses.Length>3) offset = 0.15f;

        foreach (var item in responses)
        {

            Button response = Instantiate(buttonPrefab,responseBox.transform);
            RectTransform buttonTransform = response.GetComponent<RectTransform>();
            buttonTransform.anchorMin = new Vector2(0.5f, 1);
            buttonTransform.anchorMax = new Vector2(0.5f, 1);
            buttonTransform.pivot = new Vector2(0.5f, 1);
            buttonTransform.anchoredPosition  = new Vector2(0, startY);
            startY -= offset;
            string[] capturedOptions = dialogueBase.currentDialogueLineData.branchID.Split('|');
            string selectedOption = capturedOptions[index];
            response.onClick.AddListener(()=>ResponseClicked(selectedOption));
            index++;
            // response.GetComponent<Text>().text = item.ToString();

            response.GetComponentInChildren<TextMeshProUGUI>().SetText(item);
            Debug.Log(item);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        hasButtons = true;
        
    }

    private void ResponseClicked(string option)
    {
        Debug.Log("responseClicked");
        dialogueBase.responseReturnID = responseReturnID = option;
        Cursor.visible = false;
        DestroyCurrentOptionButtons(); Debug.Log("destroycalled");
        dialogueBase.ProgressDialogue();
        //ProgressDialogueCanvas();

        

    }

    private void DestroyCurrentOptionButtons()
    {
        foreach (Transform child in responseBox.transform)
        {
            Destroy(child.gameObject);
        }
        hasButtons = false;
    }



}
