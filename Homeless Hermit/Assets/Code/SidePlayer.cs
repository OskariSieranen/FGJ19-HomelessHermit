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
    public int initialHp;
    private int currentHp;

    // Start is called before the first frame update
    void Start()
    {
        if (initialHp <= 0)
            initialHp = 100;
        currentHp = initialHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.velocity = new Vector2(-movementForce, body.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.velocity = new Vector2(movementForce, body.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        PreventOutOfCamera();
    }

    void PreventOutOfCamera()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if(pos.x < 0.01f)
        {
            body.velocity = new Vector2(Mathf.Abs(body.velocity.x), body.velocity.y);
            //transform.position = new Vector3(0.01f, transform.position.y);
        }
        else if (1.0 < pos.x)
        {
            body.velocity = new Vector2(-Mathf.Abs(body.velocity.x), body.velocity.y);
        }
        else if (pos.y < 0.0)
        {
            //We're below the camera, initiate gameover
            Debug.Log("TODO: Gameover!");
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance);
        return hit.collider != null;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var itemCollision = col.gameObject.GetComponent<SideItemBase>();
        if (itemCollision != null)
        {
            //TODO: Put item to inventory
            if(itemCollision is SideItemFood)
            {
                var food = itemCollision as SideItemFood;
                ChangeHealth(food.damageModifier);
            }
            Destroy(col.gameObject);
        }
        var enemyCollision = col.gameObject.GetComponent<SideEnemyBase>();
        if(enemyCollision != null)
        {
            //TODO: Change scene to fight!!
        }
    }

    private void ChangeHealth(int modifier)
    {
        if(currentHp + modifier > initialHp)
        {
            currentHp = initialHp;
        }
        else if(currentHp + modifier <= 0)
        {
            //TODO: initiate gameover
            Debug.Log("TODO: Gameover!");
            return;
        }
        currentHp += modifier;
        Debug.Log(string.Format("Current HP {0}", currentHp));
    }
}
