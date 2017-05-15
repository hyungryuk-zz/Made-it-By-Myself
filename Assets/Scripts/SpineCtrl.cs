
using UnityEngine;
public class SpineCtrl : MonoBehaviour {
    Transform tr;
    public float angleValue = 0.0f;
    // Use this for initialization
    void Start () {
        
        tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        angleValue += Input.GetAxis("Mouse Y");
        tr.Rotate(Vector3.left * angleValue*5.0f);
    }
}
