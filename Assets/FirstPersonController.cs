using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float upDownRange = 70f;

    private Vector3 speed;
    private float forwardSpeed;
    private float sideSpeed;
    private float runSpeed;

    private float rotLeftRight;
    private float verticalRotation = 0f;

    private float verticalVelocity = 0f;

    private Animator animator;
    [SerializeField]
    private RuntimeAnimatorController[] animatorControllers = new RuntimeAnimatorController[4];
    [SerializeField]
    private AudioClip[] audioClips = new AudioClip[2];

    private AudioSource audioSource;

    private SphereCollider sphereCollider;

    private CharacterController cc;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        FPMove();
        FPRotate();
    }

    //플레이어의 x축, z축 움직임을 담당
    private void FPMove()
    {
        runSpeed = movementSpeed * 2;

        if (Input.GetKey(KeyCode.LeftShift)) //Shift 누를 경우 달리기
        {
            forwardSpeed = Input.GetAxis("Vertical") * runSpeed;
            sideSpeed = Input.GetAxis("Horizontal") * runSpeed;
            audioSource.clip = audioClips[0];
            sphereCollider.radius = 15f;
        }
        else if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) //가만히 있을때 발소리 안남
        {
            forwardSpeed = 0;
            sideSpeed = 0;
            sphereCollider.radius = 8f;
        }
        else
        {
            forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
            sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
            audioSource.clip = audioClips[1];
            sphereCollider.radius = 10f;
        }

        //모션과 사운드
        if (forwardSpeed == 0 && sideSpeed == 0)
        {
            animator.runtimeAnimatorController = animatorControllers[0];
            audioSource.Pause();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (forwardSpeed > 0 || sideSpeed != 0))
        {
            animator.runtimeAnimatorController = animatorControllers[2];
            if(!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (forwardSpeed > 0 || sideSpeed != 0)
        {
            animator.runtimeAnimatorController = animatorControllers[1];
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (forwardSpeed < 0)
        {
            animator.runtimeAnimatorController = animatorControllers[3];
            if (!audioSource.isPlaying)
                audioSource.Play();
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        speed = transform.rotation * speed;

        cc.Move(speed * Time.deltaTime);
    }

    private void FPRotate()
    {
        //좌우 회전
        rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0f, rotLeftRight, 0f);

        //상하 회전
        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void OnControllerColliderHit(ControllerColliderHit colliderHit)
    {
        if(colliderHit.gameObject.CompareTag("Monster"))    //게임끝
            print(colliderHit.gameObject.name);
    }
}