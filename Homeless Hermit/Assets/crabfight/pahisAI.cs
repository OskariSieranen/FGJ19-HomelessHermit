using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pahisAI : MonoBehaviour
{
    public bool HurtPlayer = false;
    public bool Shield = false;
    public static pahisAI singleton;
    //States:Attack, Block, Idle
    public string PahisState;
    //attack frequency
    public int Aggressiveness;
    //delay between attacks
    public float attackPeriod;
    public Animator animPahis;
    private bool delayed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;
    }

    public void Update()
    {
        //Idling, Blocking, Attacking
        //if state = idle, block
        if (PahisState == "Idle")
        {
            if (!delayed)
            {
                animPahis.Play("pahisBlock", 0);
                PahisState = "Block";
            }

        }
        //if state = block, decide weather to attack or not
        else if (PahisState == "Block" && !delayed)
        {
            //Idle before attacking
            StartCoroutine(Delay());
            //attack?
            bool attack = Random.Range(0, 100) < Aggressiveness;
            if (attack)
            {
                PahisState = "Attack";
                animPahis.Play("pahisPrepAttack");
            }
        }
        //if state = attack, wait?
        else if (PahisState == "Attack" && !delayed)
        {
            //trigger attack
            StartCoroutine(Delay());
            animPahis.Play("pahisAttack", 0);
        }
    }

    private IEnumerator Delay()
    {
        delayed = true;
        yield return new WaitForSeconds(attackPeriod);
        delayed = false;
    }
}
