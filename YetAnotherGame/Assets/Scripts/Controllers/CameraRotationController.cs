using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationController : MonoBehaviour
{
    private GameObject _player;
    private GameObject _fakeHead;

    void Start()
    {
        //get player and head of player
        _player = GameObject.FindGameObjectWithTag(Constants.Player);
        _fakeHead = GameObject.FindGameObjectWithTag(Constants.FakeHead);
    }

    void Update()
    {
       Rotate();
    }
    
    private void Rotate()
    {
        var horizontalRotation = Input.GetAxis(Constants.MouseX);
        var verticalRotation = Input.GetAxis(Constants.MouseY);
        
        //rotate head and body separately
        _player.transform.Rotate(0, horizontalRotation, 0);
        _fakeHead.transform.Rotate(-verticalRotation, 0, 0);
    }
}
