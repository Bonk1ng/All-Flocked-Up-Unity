using UnityEngine;
using System.Collections.Generic;

public class NPCBase: MonoBehaviour, I_NPCInterface
{
    //on load
    public void Awake()
    {
        Debug.Log("Loading");
    }
    //on start
    public void Start()
    {
        Debug.Log("NPC LOADED");
    }

    public void Update()
    {
        
    }    
    public void LookAtNPC()
    {

    }

    public void InteractWithNPCDialogue()
    {

    }


}
