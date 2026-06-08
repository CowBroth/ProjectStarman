using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class TestMoveScript : MonoBehaviour
{
    public Stats stats;

    public float m_speed;
    public bool canBattle;
    private int m_lookdirection;
    //private string[] m_lookdirectionString = {"up", "right", "down", "left"};

    private InputAction ctrl_move;
    private InputAction ctrl_interact;
    private SpriteLibrary sprite_library;
    private SpriteRenderer sprite_renderer;
    private BoxCollider2D obj_interact;
    private Rigidbody2D rb;
    private Animator anim;

    public GameObject interact_target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ctrl_move = InputSystem.actions.FindAction("Move");
        ctrl_interact = InputSystem.actions.FindAction("Interact");

        sprite_library = GetComponent<SpriteLibrary>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        obj_interact = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sprite_renderer.sprite = sprite_library.GetSprite("EwanLib", "E_WalkD");

        stats = ManagerScript.instance.playerStats;
        transform.position = ManagerScript.instance.position;
        StartCoroutine(EnemDespawn());
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

        rb.linearVelocity = new Vector2(moveValue.x, moveValue.y).normalized * m_speed;

        if (moveValue != Vector2.zero)
        {
            anim.SetBool("IsMoving", true);
            SpriteChange(moveValue);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        if (ctrl_interact.triggered)
        {
            InteractTrigger(interact_target);
        }
        //anim.SetBool("IsMoving", moveValue != Vector2.zero);
        //anim.SetInteger("LookDirection", m_lookdirection);
    }

    public IEnumerator EnemDespawn()
    {
        canBattle = false;
        yield return new WaitForSeconds(0.5f);
        canBattle = true;
    }
    void SpriteChange(Vector2 value)
    {
        if (value == Vector2.up)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("EwanLib", "E_WalkU");
            m_lookdirection = 0;
        }
        else if (value == Vector2.right)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("EwanLib", "E_WalkR");
            m_lookdirection = 1;
        }
        else if (value == Vector2.down)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("EwanLib", "E_WalkD");
            m_lookdirection = 2;
        }
        else if (value == Vector2.left)
        {
            sprite_renderer.sprite = sprite_library.GetSprite("EwanLib", "E_WalkL");
            m_lookdirection = 3;
        }
        //Debug.Log(m_lookdirectionString[m_lookdirection] + " " + value);
        anim.SetInteger("LookDirection", m_lookdirection);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (!canBattle)
            {
                collision.gameObject.SetActive(false);
                return;
            }
            FindFirstObjectByType<AudioManager>().Stop("OST");
            FindFirstObjectByType<AudioManager>().Play("Battle!");
            ManagerScript.instance.BattleScene(collision.gameObject.GetComponent<EnemyScript>().stats, collision.gameObject.GetComponent<EnemyScript>().prefab, collision.gameObject, gameObject.transform);
        }
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
            rb.linearVelocity = Vector2.zero;
            npc.GetComponent<Dialogue>().TriggerDialogue();
        }
        else
        {
            
        }
    }
}