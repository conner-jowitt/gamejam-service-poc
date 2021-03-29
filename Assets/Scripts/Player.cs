using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    [SerializeField] float _shootDelay = 0.25F;
    [SerializeField] Rigidbody2D _bulletPrefab;
    
    Vector2 targetVector = Vector2.zero;
    Vector3 direction;
    Vector3 velocity;
    Rigidbody2D _rb;
    float _nextFireTime;
    float angleOffset = -90;

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

        if(ReadyToFire())
        {
            Fire();
        }
    }

    bool ReadyToFire() => Time.time >= _nextFireTime;

    void Fire()
    {
        _nextFireTime = Time.time + _shootDelay;
        var shot = Instantiate(_bulletPrefab, transform.position + Vector3.forward, transform.rotation);
        Debug.Log(velocity);
        Debug.Log(velocity + transform.up * 5);
        shot.velocity = velocity + (transform.up * 15);
    }
}
