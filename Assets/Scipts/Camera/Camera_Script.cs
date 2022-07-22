using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{ 
    public Transform player;
    public Transform playerNeck;
    public Transform target;
    public Transform playerSpine;
    // public GameManager manager;
    
    public float X;
    public float Y;
    
    // Animator animator;
    public float turnSpeed = 4.0f;
    public float height = 1.5f;
    public float distance = 2f;
    private Vector3 pos;
    // public Vector3 transform.eulerAngles;

    private Vector3 offsetPosition = new Vector3(0,2.5f,-2.5f);

    
    private Space offsetPositionSpace = Space.Self;
     
    public Vector3 transpos;
    // private Vector3 transrot;
    // [SeializeField]
    private Vector3 offsetX;
    // [SeializeField]
    private Vector3 offsetY;
    public Vector3 aimoffsetX;
    public Vector3 aimoffsetY;
    private Vector3 eulerNeckRotation;
    private Vector3 eulerSpineRotation;
    private Vector3 VerticalRotation;
    private Vector3 defaultpos;
    // Floats
    
    private float SmoothFactor = 0.4f;
    
    private float SmoothFactor1 = 0.1f;
    // public float RotationSpeed = 3.0f;
    private float relativeRotation;
    // public float relativeRotationx;
    // ------------------------------------------ //
    // Private Variables

    // Floats
    private float playerRotation;
    private float NewNeckRotation;
    // private float NewSpineRotation;
    // public float playerRotationx;
    private float NewNeckRotationx;

    public float yaw = 0.0f;
    public float pitch = 0.0f;
    public float speedH = 2.0f;
    public float speedV = 2.0f;
     
     void Start () {
        // animator = GetComponent<Animator>();
        // var animator = target.GetComponent("Animator");
        offsetX = new Vector3 (0, height, -distance);
        offsetY = new Vector3 (0, 0, distance);
        aimoffsetX = new Vector3 (0, 1.7f, -0.5f);
        aimoffsetY = new Vector3 (0, 0, 0.5f);
        defaultpos = new Vector3(playerNeck.eulerAngles.x,playerNeck.eulerAngles.y,playerNeck.eulerAngles.z);
        Cursor.visible = false;
        // manager = target.GetComponent<changeWeapon>();
        // onAim = manager.bowAim;
        
     }
     
    void unarmedcamera()
    {
        if (Input.GetMouseButton(1))
        {
            
            offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
            offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
            // Getting mouse input into a Vector3 variable
            transpos =  new Vector3(player.position.x + offsetX.x, player.position.y + offsetY.y, player.position.z + offsetX.z);
            transform.position = Vector3.Slerp(transform.position, transpos, SmoothFactor);
        }

        // else if (Input.GetAxis("Mouse X")==0 && Input.GetAxis("Mouse Y")==0)
        else
        {
            if (offsetPositionSpace == Space.Self)
            {
                transpos = target.TransformPoint(offsetPosition);
            }
            else
            {
                transpos = target.position + offsetPosition;
                
            }
            transform.position = Vector3.Slerp(transform.position, transpos, SmoothFactor1) ;
        }


        
        transform.LookAt(player.position);
        
        // bool e_bow = animator.GetBool("e_bow");
        
    
        // _________________________________________ //
    // Relative Rotation of Head (playerNeck) of player
    
        // Make new Vector3 taking playerNeck (x and y angles) and camera's y angle       
        eulerNeckRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, playerNeck.eulerAngles.z);

        // Variables
        playerRotation = player.eulerAngles.y;
        NewNeckRotation = eulerNeckRotation.y;
        // playerRotationx = player.eulerAngles.x;
        NewNeckRotationx = eulerNeckRotation.x;

        // Cure for negative value of Rotations 
            if(NewNeckRotation < 0.0f)
            {
                NewNeckRotation = 360.0f + NewNeckRotation;   
            }

        // Calulating Relative Rotation of Player's Head (playerNeck)
            if (NewNeckRotation > playerRotation)
            {
                relativeRotation = NewNeckRotation - playerRotation;
            }
            if (playerRotation > NewNeckRotation)
            {
                relativeRotation = playerRotation - NewNeckRotation;
            } 

            if(NewNeckRotation < 0.0f)
            {
                NewNeckRotation = 360.0f + NewNeckRotation;   
            }
        
            if (((relativeRotation >= 0.0f && relativeRotation <= 70.0f) || (relativeRotation >= 280.0f && relativeRotation <= 360.0f)) && (NewNeckRotationx <= 40.0f || NewNeckRotationx >= 280.0f))
            {   
                playerNeck.rotation = Quaternion.Euler(eulerNeckRotation);
            }
    }

    // void armedbowcamera()
    // {
    //     if (Input.GetMouseButton(1))
    //     {
    //         yaw += speedH * Input.GetAxis("Mouse X");
    //         pitch -= speedV * Input.GetAxis("Mouse Y");

    //         transpos = new Vector3(pitch, yaw, 0.0f);
    //         transform.eulerAngles = transpos;
    //         // target.rotation.y
    //     }

    //     // else if (Input.GetAxis("Mouse X")==0 && Input.GetAxis("Mouse Y")==0)
    //     else
    //     {
    //         if (offsetPositionSpace == Space.Self)
    //         {
    //             transpos = target.TransformPoint(offsetPosition);
    //         }
    //         else
    //         {
    //             transpos = target.position + offsetPosition;
                
    //         }
    //         transform.position = Vector3.Slerp(transform.position, transpos, SmoothFactor1) ;
    //     }

        
        
    //     transform.LookAt(player.position);
        
    //     // bool e_bow = animator.GetBool("e_bow");
        
    
    //     // _________________________________________ //
    // // Relative Rotation of Head (playerNeck) of player
    //     eulerSpineRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, playerSpine.eulerAngles.z);
    //     playerSpine.rotation = Quaternion.Euler(eulerSpineRotation);
    // }

     void LateUpdate()
     {  
        // onAim = changeweapon.bowAim;
        unarmedcamera();
    }
}
