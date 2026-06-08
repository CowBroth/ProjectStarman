using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text nameText;
    public Text dialogueText;

    public Queue<string> sentences;

    public static bool active;

    void Start()
    {
        sentences = new Queue<string>();
        active = false;
    }

    private void Update()
    {
        dialogueBox.SetActive(active);
    }

    public void StartDialogue(BaseText dialogue)
    {
        active = true;
        FindFirstObjectByType<AudioManager>().Play("Interact");

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        { 
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence();
    }
    
    public void StartBattleDialogue(string dialogue)
    {
        Debug.Log("start");
        active = true;
        sentences.Clear();
        //sentences.Enqueue(dialogue); 
        dialogueText.text = dialogue;
        StartCoroutine(DisplayTimer());
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        FindFirstObjectByType<AudioManager>().Play("Dia_Scroll");
        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation :(");
        active = false;
    }

    IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(2f);
        EndDialogue();
    }
}
