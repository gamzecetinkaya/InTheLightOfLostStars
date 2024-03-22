using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    //public Transform cameraTarget;
    //public float sSpeed = 10.0f;
    //public Vector3 dist;
    //public Transform lookTarget;

    //public GameObject target; // playerimiz
    //Vector3 distance; //kmaeranýn belli bir uzaklýðý olsun

    //void Start()
    //{
    //    distance = transform.position - target.transform.position; // Kamera ile oyuncu arasýndaki sabit uzaklýk

    //}
    //void FixedUpdate()
    //{
    //    //Vector3 dPos = cameraTarget.position + dist;
    //    //Vector3 sPos = Vector3.Lerp(transform.position, dPos, sSpeed * Time.deltaTime);

    //    //transform.position = sPos;

    //    //transform.LookAt(lookTarget.position);

    //}

    // public Transform PlayerTransform;
    // private Vector3 cameraOffSet;
    // [Range(0.01f, 1.0f)]
    // public float SmoothFactor = 0.5f;
    // public bool LookAtPlayer = false;
    // public bool RotateAroundPlayer = true;
    // public float RotationSpeed = 5.0f;

    //void Start()
    // {
    //     cameraOffSet = transform.position - PlayerTransform.position;
    // }

    // void LateUpdate()
    // {
    //     if (RotateAroundPlayer)
    //     {
    //         Quaternion camTurnAngle =
    //             Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);
    //         cameraOffSet = camTurnAngle * cameraOffSet;
    //     }
    //     Vector3 newPos = PlayerTransform.position + cameraOffSet;
    //     transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    //     if (LookAtPlayer || RotateAroundPlayer)
    //         transform.LookAt(PlayerTransform);
    // }

    [SerializeField]
    private float mouseSensitivity = 5f;
    private float rotationY;
    private float rotationX;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distanceFromTarget = 14f;
    private Vector3 currentRotation;
  private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 0.9f;
    [SerializeField]
    private Vector3 rotationXMinMax = new Vector3(14,10,5);


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX += mouseY;
        rotationY += mouseX;

        rotationX = Mathf.Clamp(rotationX, rotationXMinMax.x, rotationXMinMax.y);
        Vector3 nextRotation = new Vector3(rotationX, rotationY);

        currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);
        transform.localEulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }









}
