using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenerController : MonoBehaviour
{
    public float doorOpenAngle = 90.0f;
    private Vector3 _rotationForOpenDoor;
    private Vector3 _defaultDoorRotation;
    private bool _open;
    private bool _enter;
    private void Start()
    {
        _defaultDoorRotation = transform.eulerAngles;
        _rotationForOpenDoor = new Vector3(_defaultDoorRotation.x,_defaultDoorRotation.y + doorOpenAngle,_defaultDoorRotation.z);
    }
    private void Update()
    {
        if (_open)
        {
            this.gameObject.transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,_rotationForOpenDoor,Time.deltaTime);
        }
        else
        {
            this.gameObject.transform.eulerAngles = Vector3.Slerp(transform.eulerAngles,_defaultDoorRotation,Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.E) && _enter)
        {
            _open = !_open;
        }
    }
    private void OnCollisionEnter(Collision collide)
    {
        if (collide.collider)
        {
            _enter = true;
        }
    }
    private void OnCollisionExit(Collision collide)
    {
        if (!collide.collider)
        { 
            _enter = false; 
        }
    }
}
