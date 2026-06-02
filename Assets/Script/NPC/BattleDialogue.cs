using UnityEngine;

public class BattleDialogue : MonoBehaviour
{
    public BattleBaseText dialogue;
    public void TriggerDialogue(int move)
    {
        Debug.Log("called");
        FindFirstObjectByType<DialogueManager>().StartBattleDialogue(dialogue.turnDialogue[move]);
    }
}
