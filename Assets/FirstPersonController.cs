﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstPersonController : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float mouseSensitivity = 2f;
    private float upDownRange = 50f;

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

    public static float stemina;
    public static bool exhaustion;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        runSpeed = movementSpeed * 2f;
        stemina = 100f;
        exhaustion = false;

        StartCoroutine(Exhaustion());
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.paused)
        {
            FPMove();
            FPRotate();
        }
        else
        {
            audioSource.Pause();
        }
    }

    //플레이어의 x축, z축 움직임을 담당
    private void FPMove()
    {
        if (stemina < 20)
            exhaustion = true;

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) //가만히 있을때 발소리 안남
        {
            forwardSpeed = 0;
            sideSpeed = 0;
            sphereCollider.radius = 8f;

            if (stemina < 100f)
                stemina += Time.deltaTime * 20f;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && !exhaustion && (forwardSpeed > 0 || sideSpeed != 0)) //Shift 누를 경우 달리기
        {
            forwardSpeed = Input.GetAxis("Vertical") * runSpeed;
            sideSpeed = Input.GetAxis("Horizontal") * runSpeed;
            audioSource.clip = audioClips[0];
            sphereCollider.radius = 15f;

            stemina -= Time.deltaTime * 10f;
        }
        else
        {
            forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
            sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
            audioSource.clip = audioClips[1];
            sphereCollider.radius = 10f;

            if (stemina < 100f)
                stemina += Time.deltaTime * 15f;
        }

        //모션과 사운드
        if (forwardSpeed == 0 && sideSpeed == 0)    //멈췄을때
        {
            animator.runtimeAnimatorController = animatorControllers[0];
            audioSource.Pause();
        }
        else if (Input.GetKey(KeyCode.LeftShift) && (forwardSpeed > 0 || sideSpeed != 0) && !exhaustion)   //달릴때
        {
            animator.runtimeAnimatorController = animatorControllers[2];
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (forwardSpeed > 0 || sideSpeed != 0)    //앞으로 걸을때
        {
            animator.runtimeAnimatorController = animatorControllers[1];
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else if (forwardSpeed < 0)  //뒤로갈때
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
        if (colliderHit.gameObject.CompareTag("Monster"))
        {
            //게임 오버
            GameManager.loser = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //코인 습득(점수 증가)
        if (other.CompareTag("Crystal"))
        {
            GameManager.Score += 1;
            Destroy(other.gameObject);
        }
    }

    IEnumerator Exhaustion()
    {
        while (true)
        {
            if (exhaustion)
            {
                yield return new WaitForSeconds(5f);
                exhaustion = false;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}