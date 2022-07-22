using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform player;
    public Transform target;
    public Transform playerNeck;
    public Transform camsphere;
    public Vector3 offsetX;
    public Vector3 offsetY;
    public Vector3 transpos;
    public Vector3 currentRotation;
    public Vector3 currentNeckRotation;
    public float mousex = 0.0f;
    public float mousey = 0.0f;
    private float SmoothFactor = 0.4f;
    public float turningspeed = 0.4f; 
    public Quaternion rot;



    // Start is called before the first frame update
    void Start()
    {
        offsetX = new Vector3(0,1.5f,-2f);
        offsetY = new Vector3(0,0,2f); 
        Cursor.visible = false;
        currentRotation = camsphere.eulerAngles;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1000, layerMask) && hit.collider.tag == "Player")
        {
            // Debug.Log(hit.collider.name+", "+hit.collider.tag);
            // if (hit.collider.tag == "Player" && camsphere.transform.localScale == new Vector3(5f,5f,5f))
            // {
            //     camsphere.transform.localScale += new Vector3(0,0,0.25f);
            // }
        }
        else
        {
            // camsphere.transform.localScale -= new Vector3(0,0,0.25f);
        }
        // transform.LookAt(target);

        mousex = Input.GetAxis("Mouse X") + mousex;
        mousey = Input.GetAxis("Mouse Y") + mousey;
        camsphere.eulerAngles = new Vector3 (currentRotation.x - mousey ,currentRotation.y + mousex ,camsphere.eulerAngles.z);

        if (Input.GetKey(KeyCode.R))
        {
            if((mousex > 0.5f || mousex < -0.5f) )
            {
                Debug.Log("Changing value of mousex and mousey");
                mousex -= mousex * 0.02f;
                
            }
            if ((mousey > 0.5f || mousey < -0.5f))
                mousey -= mousey * 0.02f;
            else if ((mousex < 0.5f || mousex > -0.5f) && (mousey < 0.5f || mousey > -0.5f))
            {
                Debug.Log("Changed to " + mousey + "," + mousex);
            }
        }
    }

    void LateUpdate()
    {
        

        // Neck rotation
        
        // if (playerNeck.eulerAngles.x )
        // playerNeck.eulerAngles = playerNeck.eulerAngles + new Vector3(-mousey,mousex,playerNeck.eulerAngles.z);
    }
}