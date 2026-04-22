using UnityEngine;

public class NPC : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue()
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
