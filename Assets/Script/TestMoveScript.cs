using UnityEngine;
using UnityEngine.InputSystem;

public class TestMoveScript : MonoBehaviour
{
    public float m_speed;
    bool interactButton;
    private InputAction m_move;
    private InputAction p_interact;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_move = InputSystem.actions.FindAction("Move");
        p_interact = InputSystem.actions.FindAction("Interact");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = m_move.ReadValue<Vector2>();
        interactButton = p_interact.IsPressed();
        transform.Translate(moveValue * m_speed * Time.fixedDeltaTime);

        if (interactButton)
        {
            FindAnyObjectByType<DialogueManager>().DisplayNextSentence();
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionStay2D(Collision2D npc)
    {
        if (interactButton && npc.gameObject.CompareTag("Friendly"))
        {
            npc.gameObject.GetComponent<NPC>().TriggerDialogue();
        }
    }
}
