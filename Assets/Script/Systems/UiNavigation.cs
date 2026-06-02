using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UiNavigation : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject firstToSelect;

    private InputAction ctrl_interact;

    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstToSelect);
    }
    private void Start()
    {
        ctrl_interact = InputSystem.actions.FindAction("Interact");
    }
    private void Update()
    {
        if (ctrl_interact.triggered && eventSystem.currentSelectedGameObject != null)
        {
            UiSelect();
        }
    }
    void UiSelect()
    {
        eventSystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
    }
    public void OnSelect()
    {
        //FindFirstObjectByType<DialogueManager>().StartDialogue(eventSystem.currentSelectedGameObject.GetComponent<Dialogue>().dialogue);
        gameObject.SetActive(false);
    }
}
