using UnityEngine;
using UnityEngine.InputSystem;

public class CombatScript : MonoBehaviour
{
    private InputAction ctrl_interact;
    public static int battleTurn;

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
                FindFirstObjectByType<DialogueManager>().Invoke("DisplayNextSentence", 1.5f);
                battleTurn = 2;
                EnemyTurn();
            }
            return;
        }
    }

    void EnemyTurn()
    {
        FindFirstObjectByType<DialogueManager>().Invoke("DisplayNextSentence", 1.5f);

    }
}