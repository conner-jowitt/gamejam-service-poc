using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    
    Vector2 targetVector = Vector2.zero;
    Vector3 direction;
    Rigidbody2D _rb;
    float angleOffset = -90;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        _rb.MovePosition(_rb.position + targetVector * _speed * Time.fixedDeltaTime);

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + angleOffset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        targetVector.x = Input.GetAxisRaw("Horizontal");
        targetVector.y = Input.GetAxisRaw("Vertical");
        targetVector.Normalize();
    }
}
