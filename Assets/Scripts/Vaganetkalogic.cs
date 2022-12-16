using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vaganetkalogic : MonoBehaviour
{
    private SlotMachineScript slotManager;

    public float speed;
    public float normalspeed;
    private float moveInput;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slotManager = FindObjectOfType<SlotMachineScript>();

    }
    private void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
    public void OnLeftButton()
    {
        if (speed >= 0f)
        {
            speed -= normalspeed;
        }
    }
    public void OnRightButton()
    {
        if (speed <= 0f)
        {
            speed = normalspeed;
        }
    }
    public void OnButtonUp()
    {
        speed = 0f;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gift"))
        {
            slotManager.PriceUp();
        }
    }
}
