using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody2D m_Rigidbody2D;
    private Vector2 m_Movement;
    public float moveSpeed = 5f;
    
    public GameObject bulletPrefab;
    public float launchPower = 300.0f;
    Vector2 lookDirection = new Vector2(1,0);
    
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        m_Movement.Set(horizontal, vertical);
        m_Movement.Normalize();
        Vector2 move =new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Launch();
        }
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.AddForce(m_Movement * moveSpeed);
        m_Rigidbody2D.velocity *= 0.99f;
    }
    
    void Launch()
    {
        GameObject projectileObject = Instantiate(
            bulletPrefab, m_Rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity
            );
        BulletController bc = projectileObject.GetComponent<BulletController>();
        bc.Launch(lookDirection, launchPower);
    }

}
