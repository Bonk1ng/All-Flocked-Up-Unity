using UnityEngine;

[System.Serializable]
public struct StageDetails
{
    //Store the Stage variables and refs here.

    public string stageName;
    [TextArea] public string stageDescription;

    public ObjectiveDetails[] objectivesToComplete;

    public int expReward;
    public int trinketReward;
}
