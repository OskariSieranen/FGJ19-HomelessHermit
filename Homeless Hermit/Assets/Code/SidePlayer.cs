using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlayer : MonoBehaviour
{
    public Rigidbody2D body;
    public float movementForce;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            body.velocity = new Vector3(-movementForce, body.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            body.velocity = new Vector3(movementForce, body.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
