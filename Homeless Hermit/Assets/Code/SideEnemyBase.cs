using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SideEnemyBase : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public float speed;

}
