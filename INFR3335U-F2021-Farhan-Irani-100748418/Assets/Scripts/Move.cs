using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class Move : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Joystick leftJoystick;
    public Joystick rightJoyStick;
    public PhotonView photoView;

    private Vector3 velocity;
    private CinemachineFreeLook cinemachine;

    public float speed = 5f;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    float camAngle;
    float camAngleSpeed = .3f;

    private float gravity = -9.8f;


    // Update is called once per frame
    void Update()
    {
        if (photoView.IsMine)
        {
            Movement();
        }
    }

    private void Movement()
    {

        //Y-Axis
        float horizontal = leftJoystick.Horizontal;

        //Z-Axis
        float vertical = leftJoystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cinemachine.transform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        if (direction.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(moveDirection.normalized * speed * Time.deltaTime);

        }

        cinemachine.m_XAxis.Value += rightJoyStick.Horizontal * 100 * Time.deltaTime;
        cinemachine.m_YAxis.Value += rightJoyStick.Vertical * Time.deltaTime;

        //Calculate Gravity
        velocity.y += gravity * Time.deltaTime;
        //Apply gravity
        controller.Move(velocity * Time.deltaTime);
    }

    public void SetJoysticks(GameObject camera)
    {
        Joystick[] tempJoystickList = camera.GetComponentsInChildren<Joystick>();
        foreach (Joystick temp in tempJoystickList)
        {
            if (temp.tag == "LeftJoystick")
                leftJoystick = temp;
            else if (temp.tag == "RightJoystick")
                rightJoyStick = temp;
        }

        cinemachine = camera.GetComponentInChildren<CinemachineFreeLook>();
        cinemachine.LookAt = transform.GetChild(0);
        cinemachine.Follow = transform;

        cam = camera.transform;
    }
}
