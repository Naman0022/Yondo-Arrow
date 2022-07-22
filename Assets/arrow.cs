using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public Transform arrowObj;
    Vector3 positionChange;
    public float speed = 0.05f;
    public float speedChange = 0.05f;
    public bool arrowhit;

    // Start is called before the first frame update
    void Start()
    {
        positionChange = arrowObj.position;
        // speed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 8;
        if (arrowhit == false)
        {
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(new Vector3(arrowObj.position.x,arrowObj.position.y,arrowObj.position.z-1), arrowObj.TransformDirection(Vector3.forward), out hit, 1, layerMask))
            {
                arrowObj.GetComponent<ParticleSystem> ().enableEmission = false;
                arrowhit=true; 
            }
            movearrow();
        }    
    }

    void movearrow()
    {
        bool forwardPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool backPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift);
        // positionChange = new Vector3(0,0,0);
        if (forwardPressed )
        {
            
            positionChange.y += speedChange; 
        }
        if (backPressed )
        {
            // Debug.Log(backPressed);
            positionChange.y -= speedChange; 
        }
        if (leftPressed )
        {
            positionChange.x -= speedChange; 
        }
        if (rightPressed )
        {
            positionChange.x += speedChange; 
        }
        positionChange.z+= speed;
        arrowObj.position = positionChange;    
    }
    
}
