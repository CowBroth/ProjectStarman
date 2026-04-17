using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class TestMoveScript : MonoBehaviour
{
    public float m_speed;
    private int m_lookdirection;
    private string[] m_lookdirectionString = {"up", "right", "down", "left"};

    private InputAction ctrl_move;
    private InputAction ctrl_interact;
    private SpriteLibrary sprite_library;
    private SpriteRenderer sprite_renderer;
    private CircleCollider2D obj_interact;

    public GameObject interact_target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ctrl_move = InputSystem.actions.FindAction("Move");
        ctrl_interact = InputSystem.actions.FindAction("Interact");

        sprite_library = GetComponent<SpriteLibrary>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        obj_interact = GetComponent<CircleCollider2D>();

        sprite_renderer.sprite = sprite_library.GetSprite("Direction", "t_north");
    }

    // Update is called once per frame
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
        Vector2 moveValue = ctrl_move.ReadValue<Vector2>();

        transform.Translate(moveValue * m_speed * Time.deltaTime);

        if (moveValue != Vector2.zero)
        {
            SpriteChange(moveValue);
        }
        if (ctrl_interact.triggered)
        {
            InteractTrigger(interact_target);
        }
    }

    void SpriteChange(Vector2 value)
    {
        if (value == Vector2.up)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("Direction", "t_north");
            m_lookdirection = 0;
        }
        else if (value == Vector2.right)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("Direction", "t_east");
            m_lookdirection = 1;
        }
        else if (value == Vector2.down)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("Direction", "t_south");
            m_lookdirection = 2;
        }
        else if (value == Vector2.left)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("Direction", "t_west");
            m_lookdirection = 3;
        }
        //Debug.Log(m_lookdirectionString[m_lookdirection] + " " + value);
        obj_interact.offset = value;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        interact_target = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        interact_target = null;
    }

    public void InteractTrigger(GameObject npc)
    {
        if (npc != null && npc.layer == 6)
        {
            npc.GetComponent<NPC>().TriggerDialogue();
            Debug.Log("buh");
        }
        else
        {
            Debug.Log("dih");
        }
    }
}