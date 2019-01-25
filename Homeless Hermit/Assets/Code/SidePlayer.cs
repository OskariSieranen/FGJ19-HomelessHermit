using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidePlayer : MonoBehaviour
{
    public Rigidbody2D body;
    public float movementForce;
    public float jumpForce;
    public GameObject rayOrigin;
    public float rayCheckDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            body.velocity = new Vector3(-movementForce, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = new Vector3(movementForce, body.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance);
        return hit.collider != null;
    }
}
