using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraRotationController : NetworkBehaviour
{
    private GameObject _player;

    private bool firstPointViewEnabled;

    private Transform firstPointView = null;

    private Transform thirdPointView = null;

    public void setPointViews(GameObject player, Transform fpv, Transform tpv)
    {
        _player = player;
        firstPointView = fpv;
        thirdPointView = tpv;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if(firstPointView == null || thirdPointView == null)
        {
            return;
        }
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
