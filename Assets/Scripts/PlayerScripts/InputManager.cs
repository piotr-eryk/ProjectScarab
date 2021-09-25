using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] ChooseScarab chooseScarab;
    [SerializeField] CloseGame closeGame;

    PlayerController controls;
    PlayerController.MovementActions movementAction;

    PlayerController.GameActions gameAction;

    Vector2 horizontalInput;
    Vector2 mouseInput;

    private void Awake ()
    {
        controls = new PlayerController();
        movementAction = controls.Movement;
        gameAction = controls.Game;

        movementAction.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        movementAction.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        movementAction.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        movementAction.ChooseScarab.performed += _ => chooseScarab.ClickOnScarab();

        gameAction.ExitGame.performed += _ => closeGame.ExitGame();
    }

    private void Update ()
    {
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);
    }

    private void OnEnable ()
    {
        controls.Enable();
    }

    private void OnDestroy ()
    {
        controls.Disable();
    }
}