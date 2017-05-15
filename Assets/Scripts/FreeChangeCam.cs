using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeChangeCam : MonoBehaviour
{
    private Transform tr;
    private Transform ptr;
    public float angleValue = 0.0f;
    public float rotXSpeed = 100.0f;
    public float rotYSpeed = 20.0f;
    Vector3 pos;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        pos = new Vector3(0.0f, 13.4f, 0.0f);
        ptr = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        angleValue += Input.GetAxis("Mouse Y");
        tr.Rotate(Vector3.left * Input.GetAxis("Mouse Y")*5.0f);

    }
}
