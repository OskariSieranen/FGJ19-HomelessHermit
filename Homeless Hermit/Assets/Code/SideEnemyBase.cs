using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SideEnemyBase : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public float speed;

    protected void IgnoreCollisionWithItem(Collision2D col)
    {
        var itemCollision = col.gameObject.GetComponent<SideItemBase>();
        if (itemCollision != null)
        {
            // Ignore collision with items
            Physics2D.IgnoreCollision(itemCollision.boxCollider, boxCollider, true);
        }
    }
}
