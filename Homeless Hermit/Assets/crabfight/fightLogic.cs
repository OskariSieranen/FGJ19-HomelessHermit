using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class fightLogic : MonoBehaviour
{
    public string returnscene;
    public static fightLogic singleton;
    public Animator animPlayer;

    public int PlayerHealth;
    public int EnemyHealth;
    public int PlayerPower;
    public int EnemyPower;
    public bool EnemyEnteredHurtState = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;
    }

    // Update is called once per frame
    private void Update()
    {
        //vähennä pelaajan HP
        if (pahisAI.singleton.HurtPlayer && !Player.singleton.Dodging)
        {
            pahisAI.singleton.HurtPlayer = false;
            if (Player.singleton.Blocking)
            {
                PlayerHealth -= EnemyPower / 2;
                Debug.Log("Blocked!");
            }
            else
            {
                PlayerHealth -= EnemyPower;
            }
            Debug.Log("Player HP: " + PlayerHealth);
        }
        //vähennä vihun HP, extra check due to animation system quirks
        if (Player.singleton.HurtEnemy && EnemyEnteredHurtState == false && !pahisAI.singleton.Shield)
        {
            EnemyEnteredHurtState = true;
            EnemyHealth -= PlayerPower;
            
            Debug.Log("Enemy HP: " + EnemyHealth);
            pahisAI.singleton.PahisState = "Idle";
            pahisAI.singleton.animPahis.Play("pahisIdle", 0);
        }
        Player.singleton.HurtEnemy = false;
        pahisAI.singleton.HurtPlayer = false;

        if (EnemyHealth == 0)
        {
            //wingame
            SceneManager.LoadScene(returnscene);
        }
        else if (PlayerHealth == 0)
        {
            //losegame
        }
    }
    public void StartPlayerAnimation(string anim)
    {
        if (anim == "leftPunch")
        {

        }
        animPlayer.Play(anim, 0);
    }
}
