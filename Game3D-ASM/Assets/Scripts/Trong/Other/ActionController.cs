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
    }
}
