using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Rigidbody2D rb;
    public float strength = 200f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) rb.AddRelativeForce(new Vector2(0, strength), ForceMode2D.Impulse);
    }
}
