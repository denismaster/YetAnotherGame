using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _turnSpeed = 150;
    [SerializeField] private float _jumpForce = 4;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Rigidbody _rigidBody = null;

    private float _currentV = 0;
    private float _currentH = 0;

    private readonly float _interpolation = 10;
    private readonly float _walkScale = 0.33f;
    private readonly float _backwardsWalkScale = 0.16f;
    private readonly float _backwardRunScale = 0.66f;
    private float _jumpTimeStamp = 0;
    private float _minJumpInterval = 0.25f;

    private bool _wasGrounded;
    private bool _isGrounded;

    private Vector3 _currentDirection = Vector3.zero;
    private List<Collider> _collisions = new List<Collider>();

    private void Start()
    {
        Cursor.visible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!_collisions.Contains(collision.collider)) {
                    _collisions.Add(collision.collider);
                }
                _isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true;
                break;
            }
        }

        if(validSurfaceNormal)
        {
            _isGrounded = true;
            if (!_collisions.Contains(collision.collider))
            {
                _collisions.Add(collision.collider);
            }
        } 
        else
        {
            if (_collisions.Contains(collision.collider))
            {
                _collisions.Remove(collision.collider);
            }
            if (_collisions.Count == 0) 
            { 
                _isGrounded = false; 
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(_collisions.Contains(collision.collider))
        {
            _collisions.Remove(collision.collider);
        }
        if (_collisions.Count == 0)
        { 
            _isGrounded = false; 
        }
    }

	void Update ()
    {
        _animator.SetBool("Grounded", _isGrounded);

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0) 
        {
            v *= walk ? _backwardsWalkScale : _backwardRunScale;
        } 
        else if (walk)
        {
            v *= _walkScale;
        }

        _currentV = Mathf.Lerp(_currentV, v, Time.deltaTime * _interpolation);
        _currentH = Mathf.Lerp(_currentH, h, Time.deltaTime * _interpolation);

        transform.position += transform.forward * _currentV * _moveSpeed * Time.deltaTime;
        transform.Rotate(0, _currentH * _turnSpeed * Time.deltaTime, 0);

        _animator.SetFloat("MoveSpeed", _currentV);

        JumpingAndLanding();

        _wasGrounded = _isGrounded;
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - _jumpTimeStamp) >= _minJumpInterval;

        if (jumpCooldownOver && _isGrounded && Input.GetKey(KeyCode.Space))
        {
            _jumpTimeStamp = Time.time;
            _rigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        if (!_wasGrounded && _isGrounded)
        {
            _animator.SetTrigger("Land");
        }

        if (!_isGrounded && _wasGrounded)
        {
            _animator.SetTrigger("Jump");
        }
    }
}
