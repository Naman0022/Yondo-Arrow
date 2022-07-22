using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    float rotationSpeed = 1;
    public Transform Target, player, playerNeck;
    float mouseX, mouseY;
    float zoomSpeed=1;
    public Transform Obstruction;

    private Vector3 offsetPosition = new Vector3(0,2.5f,-2.5f);

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

     public float turnSpeed = 4.0f;
    public float height = 1.5f;
    public float distance = 2f;
        
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

    
    void Start()
    {
        offsetX = new Vector3 (0, height, -distance);
        offsetY = new Vector3 (0, 0, distance);
        aimoffsetX = new Vector3 (0, 1.7f, -0.5f);
        aimoffsetY = new Vector3 (0, 0, 0.5f);
        defaultpos = new Vector3(playerNeck.eulerAngles.x,playerNeck.eulerAngles.y,playerNeck.eulerAngles.z);
        Cursor.visible = false;
        
    }

    private void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }
    

    void CamControl()
    {
        transform.LookAt(Target);

        offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
        // Getting mouse input into a Vector3 variable
        transpos =  new Vector3(player.position.x + offsetX.x, player.position.y + offsetY.y, player.position.z + offsetX.z);
        transform.position = Vector3.Slerp(transform.position, transpos, SmoothFactor);
    }
    

    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag != "player")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                
                if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
    }
}