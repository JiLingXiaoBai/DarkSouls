using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float horizontalSpeed = 100.0f;
    public float verticalSpeed = 80.0f;
    public float cameraDampValue = 0.05f;
    
    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject model;
    private GameObject cameraGO;
    private float tempEulerX;
    private Vector3 cameraDampVelocity;
    private UserInput pi;
    
    void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        tempEulerX = 20.0f;
        ActorController ac = playerHandle.GetComponent<ActorController>();
        model = ac.model;
        pi = ac.pi;
        cameraGO = Camera.main.gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    void FixedUpdate()
    {
        Vector3 tempModelEuler = model.transform.eulerAngles;
        
        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        tempEulerX -= pi.Jup * verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);
        model.transform.eulerAngles = tempModelEuler;

        cameraGO.transform.position =
            Vector3.SmoothDamp(cameraGO.transform.position, transform.position, ref cameraDampVelocity, cameraDampValue);
        // cameraGO.transform.eulerAngles = transform.eulerAngles;
        cameraGO.transform.LookAt(cameraHandle.transform);
    }
}
