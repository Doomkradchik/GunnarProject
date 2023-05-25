using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementController movement;

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var animator = GetComponent<Animator>();
        movement.Init(rigidbody, animator, "moveStep");
    }

    private void Update()
    {
        MovementController.State state = MovementController.State.Walking;
        if (Input.GetKey(KeyCode.LeftControl))
            state = MovementController.State.Sneaking;
        else
            if (Input.GetKey(KeyCode.LeftShift))
                state = MovementController.State.Running;

        var shiftPressed = Input.GetKey(KeyCode.LeftShift);

        var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        state = input.normalized.magnitude == 0 ? MovementController.State.None : state;
        movement.Move(input, state);
    }

    
}
