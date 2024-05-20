using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float cameraRotationLimit;

    private float currentCameraRotationX;
    private Camera _playerCam;
    private Rigidbody _rigid;

    private void Awake()
    {
        _playerCam = GetComponent<Camera>();
        _rigid = GetComponentInParent<Rigidbody>();

    }
    void Update()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * mouseSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        _playerCam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);

        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * mouseSensitivity;
        _rigid.MoveRotation(_rigid.rotation * Quaternion.Euler(_characterRotationY)); // ÄõÅÍ´Ï¾ð * ÄõÅÍ´Ï¾ð
        MouseCursor();
    }

    public void MouseCursor()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            //Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

}
