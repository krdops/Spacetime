// ==================================================================================================
// Author: <your company name>
// Editor: MonoDevelop-Unity
// Description: Calculates and applies player movement to attached GameObject based on control input.
// Dependencies: Rigidbody, Transform
// ==================================================================================================


using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]          // Ensures that this script REQUIRES "Rigidbody" component as a dependency.
[RequireComponent(typeof(Transform))]			// Ensures that this script REQUIRES "Transform" component as a dependency.

public class player_movement_old: MonoBehaviour
{

    #region VARIABLES
    // Static Vars
    public float moveSpeed = 10f;
    public float rotateSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -10f;
    public float maxY = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;
    public float jumpSpeed = 2f;
    public float gravity = 9.81f;


    // Public Vars -- shown in Unity Inspector, accessible to linked classes.
    public Vector3 velocity = new Vector3(0, 0, 0);
    public Vector3 minVelocity;
    public Vector3 maxVelocity;

    public bool isWalking = false;
    public bool isJumping = true;
    public bool isGrounded = false;
    public bool inputJump = false;
    public bool inputUp = false;
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputDown = false;


    // Private Vars -- not shown in Unity Inspector, inaccessible to linked classes.
    private Transform _transform;
    private Rigidbody _rigidbody;
    #endregion

    #region UNITY_FUNCTIONS
    // Initialize any vars, script linking, component linking.
    void Start()
    {

        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;

    }


    // Update is called once per frame; user-hardware dependent!
    // Recommended for control input.
    void Update()
    {

        checkInput();

    }


    // Update is called at fixed time intervals; not user-hardware dependent!
    // Recommended for physics calculations.
    void FixedUpdate()
    {

        // 1. Calculate Velocities
        calcVelocity();

        // 2. Apply velocities to movement.
        applyPhysics();

    }
    #endregion

    #region COLLISION_HANDLING
    // Collision checks between this GameObject and other GameObjects.
    void OnCollisionEnter(Collision target)
    {

        // On ground.
        if (target.gameObject.CompareTag("Ground")) isGrounded = true;

    }

    // Collision checks while GameObject is "touching" the other GameObject.
    void OnCollisionStay(Collision target)
    {

        // Staying on ground.
        if (target.gameObject.CompareTag("Ground")) isGrounded = true;

    }

    // Collision checks once GameObject departs from the other GameObject.
    void OnCollisionExit(Collision target)
    {

        // Leaving ground.
        if (target.gameObject.CompareTag("Ground")) isGrounded = false;

    }
    #endregion

    #region INPUT_CHECKING
    // Checks for user input key presses.
    void checkInput()
    {

        // 1. Check movement inputs.
        checkInput_arrowkeys();

        // 2. Check jump input.
        checkInput_action();

    }

    // Checks any pressing of arrow keys. You will see the result on the script component on the inspector.
    void checkInput_arrowkeys()
    {
        // 1a. Up Arrow
        if (Input.GetKey(KeyCode.UpArrow) == true)
            inputUp = true;
        else
            inputUp = false;

        // 1b. Down Arrow
        if (Input.GetKey(KeyCode.DownArrow) == true)
            inputDown = true;
        else
            inputDown = false;

        // 1c. Left Arrow
        if (Input.GetKey(KeyCode.LeftArrow) == true)
            inputLeft = true;
        else
            inputLeft = false;

        // 1d. Right Arrow
        if (Input.GetKey(KeyCode.RightArrow) == true)
            inputRight = true;
        else
            inputRight = false;
    }

    // Checks any pressing of non-directional keys. You will see the result on the script component on the inspector.
    void checkInput_action()
    {
        // 2a. Jumping?
        if (Input.GetKey(KeyCode.Space))
            inputJump = true;
        else
            inputJump = false;
    }
    #endregion

    #region VELOCITY_CALCULATIONS
    // Calculates the velocity (before applying the physics, ideally).
    void calcVelocity()
    {

        // 1. Get walking into velocity calculations.
        calcVelocity_walking();

        // 2. Get jumping into velocity calculations.
        calcVelocity_jumping();

        // 3. Apply max velocity ceiling.
        calcVelocity_adjustForMax();

    }

    // Calculates rotation and walking speed for final velocity value.
    void calcVelocity_walking()
    {

        // 1. Rotating?
        calcVelocity_walking_rotation();

        // 2. Moving forward/backward?
        calcVelocity_walking_walkSpeed();

    }

    // Rotates player on left/right arrow key presses.
    void calcVelocity_walking_rotation()
    {

        _transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);

    }

    // Adds walking into velocity calculation if player moves forward/backward.
    void calcVelocity_walking_walkSpeed()
    {

        velocity = _transform.TransformDirection(new Vector3(0, 0, Input.GetAxis("Vertical") * moveSpeed)) - _rigidbody.velocity;

    }

    // Adds jumping into velocity calculation if player decides to jump.
    void calcVelocity_jumping()
    {

        // 1a. Player is grounded, has pressed "Jump" key.
        if (inputJump && isGrounded)
        {
            isJumping = true;
            _rigidbody.velocity = new Vector3(0, Mathf.Sqrt(2 * jumpSpeed * gravity), 0);
        }
        // 1b. Player is grounded, not pressing "Jump" key.
        else if (!inputJump && isGrounded)
            isJumping = false;

    }

    // Clamps current velocity calculations within min/max values.
    void calcVelocity_adjustForMax()
    {

        // 1. Get current min/max velocity values.
        minVelocity = new Vector3(minX, minY, minZ);
        maxVelocity = new Vector3(maxX, maxY, maxZ);


        // 2. Clamp currently calculated velocity within those ranges.
        velocity.x = Mathf.Clamp(velocity.x, minX, maxX);
        velocity.y = 0;                                      // Y-Velocity handled with jumping.
        velocity.z = Mathf.Clamp(velocity.z, minY, maxZ);

    }

    // Finally takes velocity values and applies it to gameworld.
    void applyPhysics()
    {

        // 1. Walking
        _rigidbody.AddForce(velocity, ForceMode.VelocityChange);

        // 2. Environmental force.                 
        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0));

    }

    #endregion
}
