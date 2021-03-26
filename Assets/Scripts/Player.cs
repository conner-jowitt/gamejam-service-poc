using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed = 5;
    
    Vector2 targetVector = Vector2.zero;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        _rb.MovePosition(_rb.position + targetVector * _speed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        targetVector.x = Input.GetAxisRaw("Horizontal");
        targetVector.y = Input.GetAxisRaw("Vertical");
        targetVector.Normalize();
    }
}
