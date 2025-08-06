using UnityEngine;
//Interface for Quest functions.
public interface QuestInteraction
{
    public void InteractWithNPC(QuestLog playerQuestLog);
    public void LookAtNPC();
    
}
