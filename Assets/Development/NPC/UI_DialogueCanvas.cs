using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UI_DialogueCanvas : MonoBehaviour
{
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image dialogueImage;

    bool skipDialogue;
    public Action SkipLine { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueCanvas = GetComponent<Canvas>();
        //dialogueImage = GetComponent<Image>();
        dialogueCanvas.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
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
       
    }

    public void DestroyDialogue()
    {
        Destroy(this.gameObject);
        SkipLine();
    }

    


}
