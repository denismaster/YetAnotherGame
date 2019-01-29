using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    private GameObject _player;

    private bool firstPointViewEnabled;

    [SerializeField]
    [Header("First point view camera position")]
    private Transform firstPointView = null;

    [SerializeField]
    [Header("Third point view camera position")]
    private Transform thirdPointView = null;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag(TagConstants.Player);
    }

    void Update()
    {
        if(Input.GetButtonDown(InputConstants.ChangeView))
        {
            firstPointViewEnabled = !firstPointViewEnabled;
        }
        var newCameraViewTransform = firstPointViewEnabled ? firstPointView : thirdPointView;

        UpdatePosition(newCameraViewTransform);
        Rotate(newCameraViewTransform);
    }
    
    public void UpdatePosition(Transform newCameraViewTransform)
    {
        Camera.main.transform.position = newCameraViewTransform.position;
        Camera.main.transform.rotation = newCameraViewTransform.rotation;
    }

    private void Rotate(Transform newCameraViewTransform)
    {
        var horizontalRotation = Input.GetAxis(InputConstants.MouseX);
        var verticalRotation = Input.GetAxis(InputConstants.MouseY);
        
        //separately rotate: player horizontally and camera point vertically
        _player.transform.Rotate(0, horizontalRotation, 0);
        newCameraViewTransform.Rotate(-verticalRotation, 0, 0);
    }
}
