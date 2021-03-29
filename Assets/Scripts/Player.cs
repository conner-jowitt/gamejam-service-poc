using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _shootDelay = 0.25F;
    [SerializeField] Rigidbody2D _bulletPrefab;
    [SerializeField] Canvas _sacrificeMenu;
    Vector2 targetVector = Vector2.zero;
    Vector3 direction;
    Vector3 velocity;
    Rigidbody2D _rb;
    float _nextFireTime;
    float angleOffset = -90;
    bool _lostArm = false;
    bool _lostLeg = false;
    bool _canShoot = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        _rb.MovePosition(_rb.position + targetVector * _speed * Time.fixedDeltaTime);
        velocity = targetVector * _speed;

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        targetVector.x = Input.GetAxisRaw("Horizontal");
        targetVector.y = Input.GetAxisRaw("Vertical");
        targetVector.Normalize();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _sacrificeMenu.gameObject.SetActive(!_sacrificeMenu.gameObject.activeSelf);
        }

        if(ReadyToFire())
        {
            Fire();
        }
    }

    bool ReadyToFire() => (Time.time >= _nextFireTime) && _canShoot;

    void Fire()
    {
        _nextFireTime = Time.time + _shootDelay;
        var shot = Instantiate(_bulletPrefab, transform.position + Vector3.forward, transform.rotation);
        shot.velocity = velocity + (transform.up * 15);
    }

    public void LoseLeg()
    {
        if(!_lostLeg)
        {
            _speed /= 2;
            _lostLeg = true;
        }
        else
            _speed = 0;
    }
    public void LoseArm()
    {
        if(!_lostArm)
        {
            _shootDelay *= 2;
            _lostArm = true;
        }
        else
            _canShoot = false;
    }
}
