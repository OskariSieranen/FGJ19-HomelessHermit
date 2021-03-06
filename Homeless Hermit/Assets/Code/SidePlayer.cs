﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Prime31.TransitionKit;

public class SidePlayer : MonoBehaviour
{
    public Rigidbody2D body;
    public float movementForce;
    public float jumpForce;
    public GameObject rayOrigin;
    public float rayCheckDistance;
    public int initialHp = 100;
    public AudioClip walkClip;
    public List<SidePlayerFeet> bodyParts;
    public List<GameObject> shells;
    public List<GameObject> happy;
    public List<GameObject> normal;
    public List<GameObject> damaged;
    private int currentHp;
    private BoxCollider2D boxCollider;
    private LayerMask wallMask;
    private Vector3 prevPosition;

    public AudioSource walkSource;
    public AudioSource itemSource;

    // Start is called before the first frame update
    void Start()
    {
        if (initialHp <= 0)
            initialHp = 100;
        currentHp = initialHp;
        PlayData.Health = currentHp;
        PlayData.HealthMax = initialHp;
        boxCollider = GetComponent<BoxCollider2D>();
        wallMask = LayerMask.NameToLayer("Wall");
        if(PlayData.NextShell >= 0)
        {
            SetShell(PlayData.NextShell);
        }
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

    void FixedUpdate()
    {
        if (transform.position != prevPosition)
        {
            if (!walkSource.isPlaying && walkClip)
                walkSource.PlayOneShot(walkClip);
            MoveBodyParts();
            prevPosition = transform.position;
        }
        else
        {
            if(walkSource.isPlaying)
                walkSource.Stop();
        }
    }

    void MoveBodyParts()
    {
        if(bodyParts != null)
        {
            foreach (SidePlayerFeet item in bodyParts)
            {
                item.Move();
            }
        }
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    bool IsGrounded()
    {
        //Debug.Log(string.Format("y: {0}, collider_y: {1}", transform.position.y, boxCollider.transform.position.y));
        ContactFilter2D filter = new ContactFilter2D
        {
            layerMask = wallMask
        };
        RaycastHit2D[] results = new RaycastHit2D[10];
        int count = boxCollider.Raycast(Vector2.down, filter, results, rayCheckDistance);
        return count > 0;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var itemCollision = col.gameObject.GetComponent<SideItemBase>();
        if (itemCollision != null)
        {
            //TODO: Put item to inventory
            if (itemCollision.audio)
            {
                itemSource.PlayOneShot(itemCollision.audio);
            }
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
            PlayData.EnemyHealth = enemyCollision.health;
            PlayData.EnemyPower = enemyCollision.power;
            PlayData.CurrentShell = GetActiveShell();
            PlayData.NextShell = enemyCollision.nextShell;
            //SceneManager.LoadScene("crabfight", LoadSceneMode.Single);

            //Transition with TransitionKit
            var wind = new WindTransition()
            {
                nextScene = 2,
                duration = 1.5f
            };
            TransitionKit.instance.transitionWithDelegate(wind);
        }
    }

    private void ChangeHealth(int modifier)
    {
        if (currentHp + modifier > initialHp)
        {
            currentHp = initialHp;
        }
        else if (currentHp + modifier <= 0)
        {
            //TODO: initiate gameover
            Debug.Log("TODO: Gameover!");
            return;
        }
        else
        {
            currentHp += modifier;
        }
        if(currentHp <= initialHp / 100d * 50d)
        {
            SetDamaged();
        }
        else
        {
            SetNormal();
        }
        PlayData.Health = currentHp;
        Debug.Log(string.Format("Current HP {0}", currentHp));
    }

    private void SetHappy()
    {
        SetVisible(normal, false);
        SetVisible(damaged, false);
        SetVisible(happy, true);
    }

    private void SetNormal()
    {
        SetVisible(normal, true);
        SetVisible(damaged, false);
        SetVisible(happy, false);
    }

    private void SetDamaged()
    {
        SetVisible(normal, false);
        SetVisible(damaged, true);
        SetVisible(happy, false);
    }

    private void SetShell(int index)
    {
        if (index < 0 || index >= shells.Count)
        {
            Debug.LogError("Index ouf of range in SetShell!");
            return;
        }
        SetVisible(shells, false);
        shells[index].SetActive(true);
    }

    private int GetActiveShell()
    {
        for (int i = 0; i < shells.Count; i++)
        {
            if (shells[i].activeSelf)
                return i;
        }
        return 0;
    }

    private void SetVisible(List<GameObject> items, bool visible)
    {
        foreach (GameObject item in items)
        {
            item.SetActive(visible);
        }
    }
}
