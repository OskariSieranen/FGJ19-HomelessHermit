using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideEnemyEasy01 : SideEnemyBase
{
    private int directionSeconds;
    private float secondsElapsed;

    // Start is called before the first frame update
    void Start()
    {
        direction = 0;
        secondsElapsed = 0;
        directionSeconds = Random.Range(3, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(secondsElapsed > directionSeconds && direction > 0)
        {
            direction = (Direction)Random.Range(1, 3);
            secondsElapsed = 0;
        }
        WalkToDirection();
        RemoveIfOutsideScreen();

        secondsElapsed += Time.deltaTime;
    }

    /*void FixedUpdate()
    {
        
    }*/

    void OnCollisionEnter2D(Collision2D col)
    {
        IgnoreCollisionWithItem(col);
        if (direction == 0)
        {
            var spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null && "Wall".Equals(spriteRenderer.sortingLayerName))
            {
                // We hit the ground, start moving
                direction = (Direction)Random.Range(1, 3);
            }
        }
    }
}
