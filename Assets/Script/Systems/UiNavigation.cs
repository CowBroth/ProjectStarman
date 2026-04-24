using UnityEngine;
using UnityEngine.EventSystems;

public class UiNavigation : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject firstToSelect;
    private void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstToSelect);
    }
    public void OnSelect()
    {
        Debug.Log(gameObject);
    }
}
