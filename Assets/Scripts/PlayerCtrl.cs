using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerCtrl : NetworkBehaviour {

    float h = 0.0f;
    float v = 0.0f;

    bool jumping;

    private Transform tr;
    Animator ani;
    CharacterController cc;
    public Transform spine;

    public float moveSpeed = 30.0f;
    public float rotSpeed = 100.0f;

    public float spineAngleChangeSpeed = 0.0f;

    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        if (v < 0.0f)
        {
            tr.Translate(moveDir * Time.deltaTime * (moveSpeed-20.0f), Space.Self);
        }
        else
        {
            tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);
        }
        
        ani.SetFloat("forward", v);
        ani.SetFloat("strafe", h);
        ani.SetBool("isJump", jumping);
        if (Input.GetButton("Jump"))
        {
            if (!jumping)
            {
                jumping = true;
                Debug.Log("Jump");
                StartCoroutine(StopJump());
            }
            
        }
        if (!Input.GetButton("AngleRotFree"))
        {
            tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
            
            
        }

    }

    void LateUpdate()
    {

        spineAngleChangeSpeed += Input.GetAxis("Mouse Y");
        spine.Rotate(Vector3.left * spineAngleChangeSpeed * 5.0f);
    }

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(1.0f);     //movement.JumpTime까지 작동을 지연시켜 한번만 수행되도록 한다.
        jumping = false;
    }
}
