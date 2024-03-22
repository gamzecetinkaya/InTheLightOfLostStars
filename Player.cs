using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using System;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] float speed = 60f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private Camera followCamera;
    [SerializeField]
    GameObject Dialog_Panel;     //*****************
    [SerializeField]
    GameObject ControlDialog;
    public bool canMove;

  
    public Animator anim;
    public Rigidbody rb;

    Vector3 moveVelocity;
    

    void Start()
    {

        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
       
    }

    void Update()
    {
        

       Move();

    }



    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
        Vector3 movementDirection = movementInput.normalized;

        anim.SetBool("isWalking", movementDirection != Vector3.zero);

        //characterController.Move(movementDirection * speed * Time.deltaTime);
        characterController.Move(movementDirection * speed * Time.deltaTime + 9.81f * Time.deltaTime * Vector3.down);



        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

      
    }

 

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "empty")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(col.gameObject.tag == "portal_1")
        {
            Dialog_Panel.SetActive(true);
        }

        if(col.gameObject.tag == "son")
        {
            SceneManager.LoadScene(0);
        }
    }


    
    
}
