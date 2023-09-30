using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D missileRigidbody2D;
    [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update()
    {
        missileRigidbody2D.MovePosition(transform.position + speed * Time.deltaTime * transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
