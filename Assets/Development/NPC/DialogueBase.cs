using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using JetBrains.Annotations;

public class DialogueBase : MonoBehaviour
{
    [SerializeField] private int currentDialogueIndex=1;
    [SerializeField] private int startDialogueIndex=1;
    [SerializeField] private string currentDialogueLineID;

    [SerializeField] private string currentDialogueName;
    [SerializeField] private string currentDialogueText;
    [SerializeField] private Image currentDialogueImage;
    [SerializeField] private UI_CanvasController canvasController;

    public string DIALOGUEFILENAME = "DialogueSpreadsheet.csv";
    public List<DialogueLineData> dialogueList = new List<DialogueLineData>();
    public DialogueLineData currentDialogueLineData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadDialogueSheet();
        
        //PrintDialogue();
    }

    public void LoadDialogueSheet()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "DialogueSpreadsheet.csv");
        if (!File.Exists(filePath)) { Debug.Log("FileNotFound"); return; }

        string[] importedLines = File.ReadAllLines(filePath);
        Debug.Log(importedLines.Length);
        //does print correct number of lines but doesnt fill []

        for (int i = currentDialogueIndex; i < importedLines.Length; i++)
        {
            string line = importedLines[i].Trim();

            if (string.IsNullOrEmpty(line)) continue;

            string[] lineData = line.Split(',');

            DialogueLineData dialogueLine = new DialogueLineData
            {
                dialogueID = lineData[0],
                dialogueSpeaker = lineData[1],
                dialogueText = lineData[2],
                dialogueImage = lineData[3],
                dialogueCondition = lineData[4],
                nextID = lineData[5]
            };
            dialogueList.Add(dialogueLine);
            currentDialogueLineData = dialogueLine;
            currentDialogueName = dialogueLine.dialogueSpeaker;
            currentDialogueText = dialogueLine.dialogueText;
            currentDialogueIndex = i;
        }
    }

    public DialogueLineData GetDialogueLineByID(string id)
    {
        return dialogueList.Find(line => line.dialogueID == id);
    }

    public void SetCurrentDialogue(DialogueLineData dialogueLine, string id)
    {
        if (dialogueLine.dialogueID == id)
        {
            currentDialogueName = dialogueLine.dialogueSpeaker;
            currentDialogueText = dialogueLine.dialogueText;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void PrintDialogue(string currentDialogueLineID)
    {
        SetCurrentDialogue(currentDialogueLineData,currentDialogueLineID);
        canvasController.dialogueCanvas.UpdateDialogueUI(currentDialogueName, currentDialogueText, currentDialogueImage);
    }
    public void ProgressDialogue()
    {
        ClearDialogue();
        canvasController.dialogueCanvas.UpdateDialogueUI(currentDialogueName, currentDialogueText, currentDialogueImage);
    }

    public void ClearDialogue()
    {
        canvasController.dialogueCanvas.ClearDialogueCanvas();
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
