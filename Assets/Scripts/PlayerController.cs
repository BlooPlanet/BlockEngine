using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerController : MonoBehaviour {

    float moveSpeed;
    float walkSpeed = 4f;
    float flySpeed = 8f;
    float gravity = -9.8f;
    float gravityForce;
    
    float mouseSensitivityX = 2f;
    float mouseSensitivityY = 2f;
    float mouseRotationY;

    bool flyToggle;
    
    CharacterController controller;
    Transform cameraT;
    
    
    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
        moveSpeed = walkSpeed;
        cameraT = transform.GetChild(0).transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 moveDir = transform.forward * inputDir.y + transform.right * inputDir.x;

        CheckFlyToggle();
        if (flyToggle) {
            moveSpeed = flySpeed;
            gravityForce = 0;
            
            if (Input.GetKey(KeyCode.Space)) {
                gravityForce = flySpeed;
            }
            
            if (Input.GetKey(KeyCode.LeftShift)) {
                gravityForce = -flySpeed;
            }
        }
        else {
            moveSpeed = walkSpeed;
            if (controller.isGrounded) {
                gravityForce = -0.5f;
                if (Input.GetKeyDown(KeyCode.Space)) {
                    gravityForce = 5f;
                }
            }
            else {
                gravityForce += gravity * Time.deltaTime;
            
            }
        }
        
        Vector3 finalVelocity = moveDir * moveSpeed + Vector3.up * gravityForce;
        controller.Move(finalVelocity * Time.deltaTime);
        
        // Camera movement
        UpdateCameraMovement();
    }

    float lastJumpTime;
    void CheckFlyToggle() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastJumpTime < 0.35f) {
                flyToggle = !flyToggle;
            }
            lastJumpTime = Time.time;
        }
    }

    void UpdateCameraMovement() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
        
        transform.Rotate(Vector3.up * mouseX);

        mouseRotationY += mouseY;
        mouseRotationY = Mathf.Clamp(mouseRotationY, -90, 90);
        cameraT.eulerAngles = new Vector3(-mouseRotationY, cameraT.eulerAngles.y, cameraT.eulerAngles.z);
    }
}
