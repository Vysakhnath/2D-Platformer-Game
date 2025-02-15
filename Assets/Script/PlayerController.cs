using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Player Config value")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private PlayerCollisionController collisionController;

    private Rigidbody2D player;

    private Animator playerAnimator;

    private Vector3 directionVector;

    private GameInputController gameInputController;

    private void Start()
    {
        gameInputController = GetComponent<GameInputController>();
        playerAnimator = GetComponent<Animator>();
        player = GetComponent<Rigidbody2D>();
        directionVector = Vector3.one;
    }

    private void OnJump(InputValue jumpValue)
    {
        Debug.Log("IsGrounded" + collisionController.isGrounded);
        if (collisionController.isGrounded)
        {
            player.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnimator.SetTrigger("IsJumping");
        }
    }

    private void FixedUpdate()
    {
        if (gameInputController != null)
        {
            player.velocity = new Vector2(gameInputController.moveInput * moveSpeed, player.velocity.y);
            playerAnimator.SetFloat("Speed", Mathf.Abs(gameInputController.moveInput) * moveSpeed);
            if (gameInputController.moveInput < 0)
            {
                directionVector.x = -1;
            }
            else if (gameInputController.moveInput > 0)
            {
                directionVector.x = 1;
            }
            transform.localScale = directionVector;

            moveSpeed = gameInputController.isCrouching ? 0 : gameInputController.isSprinting ? 8 : 2;
            playerAnimator.SetBool("IsCrouching", gameInputController.isCrouching);

        }
    }
}
