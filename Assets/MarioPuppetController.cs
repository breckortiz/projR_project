using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPuppetController : MonoBehaviour
{
    [SerializeField] Camera cam = null;
    Animator animator;
    private Vector3 Look;
    private Vector3 moveDirection;
    [SerializeField] private float lookSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");
        DebugDraw.DrawVector(transform.position, transform.forward.normalized, 1.0f, 1.0f, Color.red, 0, true);
        if (verticalAxis != 0 || horizontalAxis != 0)
        {
            //reading the input:
            if (verticalAxis != 0 || horizontalAxis != 0)
            {
                Debug.Log("horizontal " + horizontalAxis);
                Debug.Log("vert " + verticalAxis);
            }



            //assuming we only using the single camera:


            //camera forward and right vectors:
            var forward = cam.transform.forward;
            var right = cam.transform.right;

            //project forward and right vectors on the horizontal plane (y = 0)
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 forwardRealitiveVertical = verticalAxis * forward;
            Vector3 RightRealitiveVertical = horizontalAxis * right;

            Vector3 lookDirection = forwardRealitiveVertical + RightRealitiveVertical;

            //transform.rotation = Quaternion.LookRotation(lookDirection.normalized);
            DebugDraw.DrawVector(transform.position, lookDirection.normalized, 1.0f, 1.0f, Color.yellow, 0, true);
            DebugDraw.DrawVector(transform.position, transform.localRotation.eulerAngles.normalized, 1.0f, 1.0f, Color.red, 0, true);
            RotateLookDirection(lookDirection.normalized, lookSpeed);

        }
    }

    private void RotateLookDirection(Vector3 target, float speed)
    {
        Look = Vector3.RotateTowards(transform.forward.normalized, target, speed * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(Look.normalized);
    }

    private void RotateMoveDirection(Vector3 target, float speed)
    {
        moveDirection = Vector3.RotateTowards(moveDirection, target, speed * Time.deltaTime, 0);
    }
}
