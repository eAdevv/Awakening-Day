using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Range(50, 500)]
    public float sensitivity;
    public Transform body;

    float xRot = 0f;

    public Transform leaner;
    public float zRot;
    bool isRotating;

    float currentRot;
    public float smoothing;

    private float bugFixed;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;     
    }
    private void Update()
    {
        #region Camera Movement
        float rotationX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        float rotationY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRot -= rotationY;
        xRot = Mathf.Clamp(xRot, -80f, 80f);

        currentRot += rotationX;
        currentRot = Mathf.Lerp(currentRot, 0, smoothing * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(xRot, 0f, currentRot);
        body.Rotate(Vector3.up * rotationX);
        
        
        #endregion

        #region Camera Lean
        if (Input.GetKey(KeyCode.E))
        {
            zRot = Mathf.Lerp(zRot, -20f, 5f * Time.deltaTime);
            isRotating = true;          
        }

        if (Input.GetKey(KeyCode.Q))
        {
            zRot = Mathf.Lerp(zRot, 20f, 5f * Time.deltaTime);
            isRotating = true;

        }
        
        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.Q))
            isRotating = false;
        

        if (!isRotating)
            zRot = Mathf.Lerp(zRot, 0.0f, 5f * Time.deltaTime);

        leaner.localRotation = Quaternion.Euler(0, 0, zRot);
        #endregion

    }

    // CURSOR PROBLEMİ GİDERİLECEK
    // SAĞ SOL DÖNÜŞ SMOOTLUĞU BUGU FİXLENECEK
}
