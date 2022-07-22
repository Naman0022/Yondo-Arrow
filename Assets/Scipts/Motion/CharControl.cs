using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControl : MonoBehaviour
{
    //Variables
    Animator animator;
    public Transform playerNeck;
    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    private float maximumWalkVelocity = 0.5f;
    private float maximumRunVelocity = 2.0f;
    private bool climb=false;
    Vector3 moveVector;

    // increase performance
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }
    
    // Custom Functions
    void changeVelocity(bool forwardPressed, bool backPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity)
    {
        // increasing velocity in z axis
        if ((forwardPressed) && (velocityZ < currentMaxVelocity)) {velocityZ += Time.deltaTime * acceleration;}
        if ((backPressed) && (velocityZ > -currentMaxVelocity)) {velocityZ -= Time.deltaTime * acceleration;}
        // increasing velocity in x axis
        if ((rightPressed) && (velocityX < currentMaxVelocity)) {velocityX += Time.deltaTime * acceleration;}
        if ((leftPressed) && (velocityX > -currentMaxVelocity)) {velocityX -= Time.deltaTime * acceleration;}
        // _____________________________________________________________________________________________________
        // Decreasing velocity in z axis
        if ((!forwardPressed) && (velocityZ > 0.0f)) {velocityZ -= Time.deltaTime * deceleration;}
        if ((!backPressed) && (velocityZ < 0.0f)) {velocityZ += Time.deltaTime * deceleration;}
        // Decreasing velocity in x axis
        if ((!rightPressed) && (velocityX > 0.0f)) {velocityX -= Time.deltaTime * deceleration;}
        if ((!leftPressed) && (velocityX < -0.0f)) {velocityX += Time.deltaTime * deceleration;}
    }

    void lockOrResetVelocity(bool forwardPressed ,bool backPressed, bool leftPressed, bool rightPressed, float currentMaxVelocity)
    {
        // reset to velocity to 0.0f
        // velocity z
        if ((!backPressed && !forwardPressed) && velocityZ!=0.0f && (velocityZ > -0.05f && velocityZ <0.05f)){velocityZ = 0.0f;}
        // velocity x
        if ((!leftPressed && !rightPressed) && velocityX!=0.0f && (velocityX > -0.05f && velocityX <0.05f)){velocityX = 0.0f;}
        //lock velocity
        // velocity z Back run -> back walk
        if ((backPressed) && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
            if (velocityZ > (-currentMaxVelocity - 0.05f)){velocityZ = -currentMaxVelocity;} 
        }
        if ((forwardPressed) && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ < (currentMaxVelocity + 0.05f)){velocityZ = currentMaxVelocity;} 
        }
        if ((leftPressed) && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX > (-currentMaxVelocity - 0.02f)){velocityX = -currentMaxVelocity;} 
        }
        if ((rightPressed) && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX < (currentMaxVelocity + 0.02f)){velocityX = currentMaxVelocity;} 
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //Bools_______________________________________________

        bool forwardPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift);
        //Bools_______________________________________________
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if ((forwardPressed && Physics.Raycast(playerNeck.position, playerNeck.TransformDirection(Vector3.forward), out hit, 1, layerMask)) )
        {
            climb=true;
            if (forwardPressed)
            {
                velocityZ += Time.deltaTime * acceleration;
                if (velocityZ >0.45){velocityZ = 0.5f;}
            }
            else if(!forwardPressed)
            {
                velocityZ -= Time.deltaTime * acceleration;
                if (velocityZ <0.05){velocityZ = 0.0f;}
            }
            if (Input.GetKey(KeyCode.E)){climb=false;}
            if (controller.isGrounded == false)
            {
                //Add our gravity Vecotr
                Debug.Log("is not grounded");
            }
            else
            {
                Debug.Log("is grounded");
            }
        }
        else 
        {
            climb=false;
                //REeset the MoveVector
            moveVector = Vector3.zero;
    
            //Check if cjharacter is grounded
            if (controller.isGrounded == false)
            {
                //Add our gravity Vecotr
                moveVector += Physics.gravity;
            }
    
            //Apply our move Vector , remeber to multiply by Time.delta
            controller.Move(moveVector * Time.deltaTime);
            
            

            //Changing current maximum velocity If run pressed
            float currentMaxVelocity = (runPressed) ? maximumRunVelocity : maximumWalkVelocity;

            // Function that changes the velocity (increase and decrease)
            changeVelocity(forwardPressed, backPressed, leftPressed, rightPressed, currentMaxVelocity);
            lockOrResetVelocity(forwardPressed, backPressed, leftPressed, rightPressed, currentMaxVelocity);
        }
        animator.SetFloat("var_z", velocityZ);
        animator.SetFloat("var_x", velocityX);
        animator.SetBool("Climb",climb);
    }

    
}
