using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] private InputActionReference openBookAction;
    [SerializeField] private GameObject book;

    private void Update()
    {
        if (openBookAction.action.triggered) { book.SetActive(true); }
    }
}
