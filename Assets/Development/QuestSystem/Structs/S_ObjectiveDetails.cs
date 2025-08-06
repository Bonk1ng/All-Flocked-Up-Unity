using UnityEngine;

[System.Serializable]
public struct ObjectiveDetails
{
    //Store all the Objective variables & refs here.

    public string objectiveName;
    [TextArea] public string objectiveDescription;
    public string objectiveType;
    public string objectiveID;
    public int quantityToComplete;
    public bool isOptional;
    public int bonusEXP;
}

