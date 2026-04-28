using UnityEngine;
using UnityEngine.InputSystem;

public class CombatScript : MonoBehaviour
{
    private InputAction ctrl_interact;

    void Start()
    {
        ctrl_interact = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (DialogueManager.active == true)
        {
            if (ctrl_interact.triggered)
            {
                FindFirstObjectByType<DialogueManager>().DisplayNextSentence();
            }
            return;
        }
    }
}
