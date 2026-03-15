using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Input Actions")]
    public InputAction moveAction;

    [Header("References")]
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private Quaternion targetRotation;
    private int isWalkingHash = Animator.StringToHash("IsWalking");


    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnSpeed = 5f;
    Vector2 inputVector;
    Vector3 moveDirection;



    private void OnEnable()
    {
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
    }

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }   


    private void Update()
    {
        inputVector = moveAction.ReadValue<Vector2>();
        moveDirection.Set(inputVector.x, 0, inputVector.y);

        moveDirection.Normalize();

        //Set Animation
        bool isWalking = !Mathf.Approximately(moveDirection.sqrMagnitude, 0f);
        playerAnimator.SetBool(isWalkingHash, isWalking);

    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        moveDirection.Normalize();
        playerRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }

    private void Rotate()
    {
        Vector3 desiredForard = Vector3.RotateTowards(transform.forward, moveDirection, turnSpeed * Time.fixedDeltaTime, 0f);
        targetRotation = Quaternion.LookRotation(desiredForard);

        playerRigidbody.MoveRotation(targetRotation);
    }


}
