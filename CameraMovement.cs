using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField, Range(0, 100)] float movementSpeed;

    [Header("Camera")]
    [SerializeField, Range(0, 1000)] float cameraSensitivity;
    [SerializeField, Range(0, 90)] float maxYAngle;
    Quaternion defaultRotation;


    //horizontal = a--d, vertical = w--s
    const string horizontalId = "Horizontal";
    const string verticalId = "Vertical";

    //Camera & mouselook
    const string mouseHorizontal = "Mouse X";
    const string mouseVertical = "Mouse Y";

    private void Awake()
    {
        if(!playerCamera)
        {
            playerCamera = Camera.main.transform;
        }
        defaultRotation = playerCamera.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        DoMovement();
        CameraRotate();
    }

    private void CameraRotate()
    {
        //Y = left,right (mouseX)
        //-X = up, down (mouseY)

        float lookLR = Input.GetAxis(mouseHorizontal) * Time.deltaTime * cameraSensitivity;
        transform.localRotation *= Quaternion.AngleAxis(lookLR, Vector3.up);

        float lookUD = Input.GetAxis(mouseVertical) * Time.deltaTime * cameraSensitivity;
        Quaternion nextRotation = playerCamera.localRotation * Quaternion.AngleAxis(lookUD, -Vector3.right);
        if(Quaternion.Angle(nextRotation, defaultRotation) < maxYAngle)
            playerCamera.localRotation = nextRotation;
        
    }

    private void DoMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis(horizontalId), 0f, Input.GetAxis(verticalId)).normalized;
        movement *= Time.deltaTime * movementSpeed;
        transform.Translate(movement);
    }
}
