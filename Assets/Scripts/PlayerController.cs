using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(RotateToMouse))]
[RequireComponent (typeof(Movement))]
public class PlayerController : MonoBehaviour {
    public static PlayerController instance;
    [SerializeField] private float moveSpeed;           // 플레이어 이동 속도
    [SerializeField] private float rotateSpeedX = 5;    // 카메라 X축 회전 감도 (위/아래)
    [SerializeField] private float rotateSpeedY = 5;    // 카메라 Y축 회전 감도 (좌/우)

    private RotateToMouse rotateToMouse;
    private PlayerAnimatorController playerAnimatorController;
    private Movement movement;
    private PlayerTilt playerTilt;
    private float mouseX;
    private float mouseY;
    private float horizontal;
    private float vertical;
    private float run;
    [HideInInspector] public bool isRun;


    private void Init() {
        if (instance == null) {
            instance = this;
        }

        Cursor.visible = false;     // 마우스 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;

        this.rotateToMouse = gameObject.GetComponent<RotateToMouse>();
        this.movement = gameObject.GetComponent<Movement>();
        this.playerTilt = gameObject.GetComponent<PlayerTilt>();
        this.playerAnimatorController = gameObject.GetComponent<PlayerAnimatorController>();
    }

    private void Awake() {
        Init();
    }

    private void Update() {
        UpdateRotate();     // 플레이어 시점
        UpdateMovement();   // 플레이어 이동
    }

    // 플레이어 시점
    private void UpdateRotate() {
        this.mouseX = Input.GetAxis("Mouse X");
        this.mouseY = Input.GetAxis("Mouse Y");

        this.rotateToMouse.UpdateRotate(this.mouseX, this.mouseY, this.rotateSpeedX, this.rotateSpeedY);  // 마우스로 시점 조작
    }

    // 플레이어 이동
    private void UpdateMovement() {
        this.horizontal = Input.GetAxisRaw("Horizontal");
        this.vertical = Input.GetAxisRaw("Vertical");
        this.run = Input.GetAxisRaw("Run"); // Left Shift 키 입력

        if (this.run > 0 && this.vertical == 1) { // 달리기 상태일 경우,
            this.isRun = true;
            this.moveSpeed = 5;
        }
        else {
            this.isRun = false;
            this.moveSpeed = 3;
        }

        this.playerAnimatorController.MovementAnimation(this.horizontal, this.vertical, this.isRun);
        this.movement.UpdateMovement(this.horizontal, this.vertical, this.moveSpeed);
    }

    // TODO: 플레이어 좌/우 기울이기



    
}