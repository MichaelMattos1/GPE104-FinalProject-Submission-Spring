using UnityEngine;
using System;
public class PlayerMovementState : MonoBehaviour
{
    public enum MoveState
    {
        Idle,
        Run,
        Jump,
    }
    public MoveState CurrentMoveState {get; private set;}

    public Animator animator;
    private const string idleAnim = "Idle";
    private const string runAnim = "Run";
    private const string jumpAnim = "Jump";
    public static Action<MoveState> OnPlayerMoveStateChanged;
    private Rigidbody2D rigidBody2D;
    private float xPosLastFrame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x == xPosLastFrame && rigidBody2D.linearVelocity.y == 0)
        {
            SetMoveState(MoveState.Idle);
        }
        else if (transform.position.x != xPosLastFrame && rigidBody2D.linearVelocity.y == 0)
        {
            SetMoveState(MoveState.Run);
        }

        xPosLastFrame = transform.position.x;
    }

    public void SetMoveState(MoveState moveState)
    {
        if (moveState == CurrentMoveState) return;

        switch (moveState)
        {
            case MoveState.Idle:
                HandleIdle();
                break;

            case MoveState.Run:
                HandleRun();
                break;
            case MoveState.Jump:
                HandleJump();
                break;

            default:
                Debug.LogError($"Invalid Movement state: {moveState}");
                break;
        }


        OnPlayerMoveStateChanged?.Invoke(moveState);
        CurrentMoveState = moveState;
    }

    private void HandleIdle()
    {
        animator.Play(idleAnim);
    }
    private void HandleRun()
    {
        animator.Play(runAnim);
    }

    private void HandleJump()
    {
        animator.Play(jumpAnim);
    }
}
