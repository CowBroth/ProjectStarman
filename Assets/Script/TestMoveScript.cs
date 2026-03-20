using UnityEngine;
using UnityEngine.InputSystem;

public class TestMoveScript : MonoBehaviour
{
    public float m_speed;
    private InputAction m_move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_move = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = m_move.ReadValue<Vector2>();
        transform.Translate(moveValue * m_speed * Time.fixedDeltaTime);
        Debug.Log(moveValue);
    }
}
