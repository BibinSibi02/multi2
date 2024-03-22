using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float movementSpeed;


    [SerializeField] private GameObject ball;

    private Rigidbody2D rb;
    private Vector2 playerMove;


    // Start is called before the first frame update
    public override void OnStartClient()
    {
        base.OnStartClient();
        if(!base.IsOwner)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false; //makes it so that only owner of object can control it
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()//called per frame and is frame dependant 
    { 
        playerMove = new Vector2(0, Input.GetAxisRaw("Vertical")); //detects input in a non-fixed rate
    }

    void FixedUpdate()//time dependant, and not dependant on the fps of the player
    {
        rb.velocity = playerMove * movementSpeed; // has player movement be updated at a fixed rate
    }

  
    
}
