using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBase : MonoBehaviour
{
    [SerializeField] private int currentDialogueIndex;
    [SerializeField] private int startDialogueIndex;

    [SerializeField] private string currentDialogueName;
    [SerializeField] private string currentDialogueText;
    [SerializeField] private Image currentDialogueImage;
    [SerializeField] private UI_CanvasController canvasController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PrintDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PrintDialogue()
    {
        canvasController.activeDialogueInstance.UpdateDialogueUI(currentDialogueName, currentDialogueText, currentDialogueImage);
    }
    public void ProgressDialogue()
    {
        ClearDialogue();
        canvasController.activeDialogueInstance.UpdateDialogueUI(currentDialogueName, currentDialogueText, currentDialogueImage);
    }

    public void ClearDialogue()
    {
        canvasController.activeDialogueInstance.ClearDialogueCanvas();
    }

    public bool SkipDialogue(Action SkipLine)
    { 
        return true;
    }

    public void SetStartDialogueLine()
    {

    }

    public void SetNextDialogueLine()
    {

    }
}
