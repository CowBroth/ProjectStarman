using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public BaseText dialogue;
    public void TriggerDialogue()
    {
        FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
    }
}
