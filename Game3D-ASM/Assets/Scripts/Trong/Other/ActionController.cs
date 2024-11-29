using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActionController : MonoBehaviour
{
    [SerializeField] private string openMenuKey = "escape";
    [SerializeField] private vThirdPersonCamera vTPCamera;
    private InputAction customAction;
    private CursorController cursorControl;
    private bool isMenuOpen;
    [Header("Menu")]
    [SerializeField] private GameObject menuGameObject;


    private void Awake()
    {
        cursorControl = GetComponent<CursorController>();
    }
    private void Start()
    {
        customAction = new InputAction("OpenMenu", InputActionType.Button);
        customAction.AddBinding("<Keyboard>/" +  openMenuKey);
        customAction.performed += OnOpenMenu;
        customAction.Enable();
    }

    private void OnOpenMenu(InputAction.CallbackContext context)
    {
        cursorControl.enabled = isMenuOpen;
        isMenuOpen = !isMenuOpen;
        vTPCamera.lockCamera = isMenuOpen;
        menuGameObject.SetActive(true);
        Time.timeScale = 0;
        customAction.performed -= OnOpenMenu;
        customAction.performed += OnCloseMenu;
    }

    public void OnCloseMenu()
    {
        cursorControl.enabled = isMenuOpen;
        isMenuOpen = !isMenuOpen;
        vTPCamera.lockCamera = isMenuOpen;
        menuGameObject.SetActive(false);
        Time.timeScale = 1;
        customAction.performed -= OnCloseMenu;
        customAction.performed += OnOpenMenu;
    }
    private void OnCloseMenu(InputAction.CallbackContext context)
    {
        cursorControl.enabled = isMenuOpen;
        isMenuOpen = !isMenuOpen;
        vTPCamera.lockCamera = isMenuOpen;
        menuGameObject.SetActive(false);
        Time.timeScale = 1;
        customAction.performed -= OnCloseMenu;
        customAction.performed += OnOpenMenu;
    }
}
