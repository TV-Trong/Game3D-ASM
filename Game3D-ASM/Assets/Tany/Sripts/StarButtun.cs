using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[SerializeField]
public class SelectedBtnManager : MonoBehaviour
{
    public GameObject selectedBtn; // botao que estara selecionado quando esta tela ativar

    private EventSystem eventSystem; // referencia ao event system

    public GameObject buttonToHide; // Nút sẽ bị ẩn
    public GameObject buttonToShow; // Nút sẽ xuất hiện

    private void OnEnable()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        StartCoroutine(routine: HighlightBtn());
    }

    IEnumerator HighlightBtn()
    {
        yield return new WaitForEndOfFrame();
        eventSystem = GameObject.Find("EventSystem").GetComponent<UnityEngine.EventSystems.EventSystem>();
        eventSystem.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        eventSystem.SetSelectedGameObject(selectedBtn);
    }

    public void ToggleButtons()
    {
        if (buttonToHide != null)
            buttonToHide.SetActive(false); // Ẩn nút đầu tiên

        if (buttonToShow != null)
            buttonToShow.SetActive(true); // Hiện nút thứ hai
    }
}
