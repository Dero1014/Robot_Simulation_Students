using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float sensitivity=0; //Koliko će se brzo micat kamera u odnosu na miš

    [Header("Zoom settings")]
    public float zoomSensitivity=0; //Koliko će brzo zumirat u odnosu na micanja kotača na mišu
    
    [Space(10)]
    public float minZoom = 0; //minimalni zoom
    public float maxZoom = 0; //maksimalni zoom

    [Header("Pan settings")]
    public float panSpeed = 0; //brzina micanja miša

    
    [Header("Camera settings")]
    [Tooltip("The distance the camera will be on focus")] public float focusDistance = 0; // Preporučena vrijednost za fokus je 2

    float rotX = 0;
    float rotY = 0;
    void Start()
    {
        rotX = transform.localEulerAngles.x;
        rotY = transform.localEulerAngles.y;
    }


    void Update()
    {
        //I just clumped everything together

        if (Input.GetKey(KeyCode.Mouse1))
        {
            CameraRotation();
            if (Input.GetKey(KeyCode.F))
                CameraFocus();
        }
        else if (Input.GetKey(KeyCode.Mouse2))
            CameraPan();
        else if(Input.GetKeyDown(KeyCode.Mouse0))
            CheckObject();
        else if (Input.GetKeyDown(KeyCode.F))
            CameraFocus();

        //prima input ond kotačića miša i tamo miće kameru
        CameraZoom();

    }

    void CameraRotation()
    {
        float xMouse = Input.GetAxisRaw("Mouse X");
        float yMouse = Input.GetAxisRaw("Mouse Y");

        float yR = xMouse * sensitivity * Time.deltaTime;
        float xR = yMouse * sensitivity * Time.deltaTime;

        rotX -= xR;
        rotY += yR;

        transform.localEulerAngles = new Vector3(rotX, rotY, 0);
    }

    void CameraZoom()
    {
        float scrollWheel = Input.GetAxisRaw("Mouse ScrollWheel");
        float zoom = scrollWheel * zoomSensitivity * Time.deltaTime;
        transform.position += transform.forward * zoom;
    }

    void CameraPan()
    {
        float xMouseDrag = Input.GetAxisRaw("Mouse X");
        float yMouseDrag = Input.GetAxisRaw("Mouse Y");

        transform.position += (-transform.right * xMouseDrag * panSpeed * Time.deltaTime);
        transform.position += (-transform.up * yMouseDrag * panSpeed * Time.deltaTime);

    }

    private Transform target; //last clicked on target
    private GameObject gm;
    public Transform me;
    
    void CameraFocus() //focusing on the object
    {
        if (target != null)
        {
            Vector3 focusPosition = target.position;
            //print("These are positions " + focusPosition + "  " + transform.position);
            Vector3 direction = -(focusPosition - transform.position);
            //print("This is direction " + direction);

            float x = target.localScale.x;
            float y = target.localScale.y;
            float z = target.localScale.z;
            float max;
            if (x > y)
            {
                if (x > z)
                    max = x;
                else
                    max = z;
            }
            else
            {
                if (y > z)
                    max = y;
                else
                    max = z;
            }
            // float focusDistance =  (x +  y + z)/ 3;
            focusPosition += ((direction).normalized * focusDistance * max); 
            transform.position = focusPosition;
            transform.LookAt(target);
            //ili
            //transform.rotation = Quaternion.LookRotation((direction).normalized, Vector3.up);
            rotX = transform.localEulerAngles.x;
            rotY = transform.localEulerAngles.y;
        }
    }
    private GameObject ground;
    void CheckObject() //for having an object to focus on
    {
        ground = GameObject.Find("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.tag != "Move Tool")
            target = hit.transform;
        if (target.transform == ground.transform)
               target = null;
        if (target == null)
            return;

        while (target.parent != null) // gets the ultimate parent
        {
            if (target.parent != null)
            {
                target = target.parent;
                print(target);

            }
        }
    }


}
