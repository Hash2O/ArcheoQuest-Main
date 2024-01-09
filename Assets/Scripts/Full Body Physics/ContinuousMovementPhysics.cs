using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ContinuousMovementPhysics : MonoBehaviour
{

    public float speed = 2;

    public float runSpeed = 5;

    public float turnSpeed = 60;
    private float jumpVelocity = 10;
    public float jumpHeight = 1.5f;

    public bool onlyMoveWhenGrounded = false;

    //Bonus
    public bool jumpWithHand = true;
    public float minJumpWithHandSpeed = 2;
    public float maxJumpWithHandSpeed = 7;


    public InputActionProperty moveInputSource;

    public InputActionProperty runInputSource;

    public InputActionProperty turnInputSource;
    public InputActionProperty jumpInputSource;

    public CapsuleCollider bodyCollider;
    public Rigidbody rb;

    //Bonus
    public Rigidbody leftHandRb;
    public Rigidbody rightHandRb;

    public LayerMask groundLayer;

    private Vector2 inputMoveAxis;
    private float inputTurnAxis;
    private bool isGrounded;
    public bool runModeActivated;

    public Transform directionSource;
    public Transform turnSource;


    //Game UI
    private float stamina;
    [SerializeField] private float maxStamina = 100f;

    [SerializeField] private Image greenWheel;
    [SerializeField] private Image redWheel;

    private bool staminaExhausted;

    private ContinuousMovementPhysics continuousMovementPhysics;

    private bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        //At start, stamina initialization
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        inputMoveAxis = moveInputSource.action.ReadValue<Vector2>();
        inputTurnAxis = turnInputSource.action.ReadValue<Vector2>().x;

        //bool isRunning = runInputSource.action.IsPressed();

        if (runInputSource.action.IsPressed())
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        bool jumpInput = jumpInputSource.action.WasPressedThisFrame();

        if (isRunning && !runModeActivated && !staminaExhausted)
        {
            speed = runSpeed;
            runModeActivated = true;
        }
        else if (isRunning && runModeActivated || staminaExhausted) 
        {
            speed = 2;
            runModeActivated = false;
        }

        if (!jumpWithHand)
        {
            if (jumpInput && isGrounded)
            {
                jumpVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * jumpHeight);
                rb.velocity = Vector3.up * jumpVelocity;
            }
        }
        else
        {
            bool inputJumpPressed = jumpInputSource.action.IsPressed();

            float handSpeed = ((leftHandRb.velocity - rb.velocity).magnitude + (rightHandRb.velocity - rb.velocity).magnitude) / 2;

            if (inputJumpPressed && isGrounded && handSpeed > minJumpWithHandSpeed)
            {
                rb.velocity = Vector3.up * Mathf.Clamp(handSpeed, minJumpWithHandSpeed, maxJumpWithHandSpeed);
            }
        }


        //Game UI
        //When run key is hit and condition is matched
        if (isRunning && !staminaExhausted)
        {
            if (stamina > 0)
            {
                //Stamina is drained (amount could be a variable if needed)
                stamina -= 30 * Time.deltaTime;
            }
            else
            {
                //If exhausted, green wheel disappear
                greenWheel.enabled = false;
                //Bool is set to true, disabling player's use of stamina until gauge is filled again
                staminaExhausted = true;
            }

            redWheel.fillAmount = (stamina / maxStamina + 0.07f);
        }
        else
        {
            if (stamina < maxStamina)
            {
                stamina += 30 * Time.deltaTime;
            }
            else
            {
                //Stamina is restored, player can use it again
                greenWheel.enabled = true;
                staminaExhausted = false;
            }

            redWheel.fillAmount = (stamina / maxStamina);
        }

        greenWheel.fillAmount = (stamina / maxStamina);


    }

    void FixedUpdate()
    {
        isGrounded = CheckIfGrounded();

        if (!onlyMoveWhenGrounded || (onlyMoveWhenGrounded && isGrounded))
        {
            //Moving around
            Quaternion yaw = Quaternion.Euler(0, directionSource.eulerAngles.y, 0);
            Vector3 direction = yaw * new Vector3(inputMoveAxis.x, 0, inputMoveAxis.y);
            Vector3 targetMovePosition = rb.position + direction * Time.fixedDeltaTime * speed;

            //Turning around
            Vector3 axis = Vector3.up;
            float angle = turnSpeed * Time.fixedDeltaTime * inputTurnAxis;
            Quaternion q = Quaternion.AngleAxis(angle, axis);

            rb.MoveRotation(rb.rotation * q);

            Vector3 newPosition = q * (targetMovePosition - turnSource.position) + turnSource.position;

            rb.MovePosition(newPosition);
        }
    }

    public bool CheckIfGrounded()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLength = bodyCollider.height / 2 - bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo,rayLength, groundLayer);

        return hasHit;
    }
}
