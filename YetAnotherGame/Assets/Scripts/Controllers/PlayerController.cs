using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2;
    [SerializeField] private float _turnSpeed = 150;
    [SerializeField] private float _jumpForce = 4;
    private Animator _animator = null;
    private Rigidbody _rigidBody = null;

    private float _currentV = 0;
    private float _currentH = 0;

    //todo: move this constants to game settings
    private readonly float _interpolation = 10;
    private readonly float _walkScale = 0.33f;
    private readonly float _backwardsWalkScale = 0.16f;
    private readonly float _backwardRunScale = 0.66f;
    private float _jumpTimeStamp = 0;
    private float _minJumpInterval = 0.25f;
    //todo: move this constants to game settings

    private bool _wasGrounded;
    private bool _isGrounded;

    private Vector3 _currentDirection = Vector3.zero;
    private List<Collider> _collisions = new List<Collider>();

    private int _score;
    public Text _scoreText;

    public Text deathText;

    private int _hp = 0;
    public Text _hpText;

    private Slider _healthBar = null;

    private void Start()
    {
        Cursor.visible = false;
        _animator = gameObject.GetComponent<Animator>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();

        AddScore(0);
        AddHp(Settings.gameSettings.player.startingHp);

        //todo: review this in multiplayer mode
        _healthBar = GameObject.FindObjectOfType<Slider>();
        if(_healthBar == null)
        {
            Debug.LogError($"health bar not found");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case TagConstants.Coin: 
            {
                SoundManagerController.Play(SoundConstants.CoinCollect);
                Destroy(collision.gameObject);
                AddScore(1);
                break;
            }
            case TagConstants.Mine: 
            {
                SoundManagerController.Play(SoundConstants.MineExplosion);
                Destroy(collision.gameObject);
                AddHp(-Settings.gameSettings.damage.mine);
                break;
            }
            case TagConstants.Turkey:
            {
                SoundManagerController.Play(SoundConstants.EatTurkey);
                Destroy(collision.gameObject);
                AddHp(Settings.gameSettings.heal.turkey);
                break;
            }
            default: 
            {
                break;
            }
        }

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

    private void AddScore(int score)
    {
        _score += score;

        _scoreText.text = $"Score: {_score}";
    }

    private void AddHp(int hp)
    {
        _hp = Mathf.Clamp(_hp + hp, 0, Settings.gameSettings.player.startingHp);
        _hpText.text = $"Health: {_hp}/{Settings.gameSettings.player.startingHp}";

        if(_hp==0){
            Time.timeScale = 0f;
            deathText.gameObject.SetActive(true);
        }

        if(_healthBar != null)
        {
            _healthBar.value = _hp;
        }
    }
}
