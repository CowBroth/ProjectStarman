using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private InputAction ctrl_interact;
    private InputAction ctrl_exit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ctrl_interact = InputSystem.actions.FindAction("Interact");
        ctrl_exit = InputSystem.actions.FindAction("EXIT");
    }

    // Update is called once per frame
    void Update()
    {
        if (ctrl_exit.triggered)
        {
            Application.Quit();
        }
        if (ctrl_interact.triggered)
        {
            SceneManager.LoadScene(1);
        }
    }
}
