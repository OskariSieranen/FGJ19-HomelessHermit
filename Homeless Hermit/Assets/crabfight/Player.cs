using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string PlayerState;
    public bool Dodging = false;
    public bool Blocking = false;

    public bool HurtEnemy = false;
    public bool HurtPlayer = false;
    public static Player singleton;
    //needed?
    public KeyCode[] inputKeys;

    public AudioClip[] dodgeSound;
    public AudioClip dodgeClip;
    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;

        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //pause until intro is done
        if (PlayerState == "Idle")
        {
            //left punch
            if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.UpArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.Space))
                {
                    fightLogic.singleton.StartPlayerAnimation("leftPunch");
                    PlayerState = "LeftPunch";
                }
            }

            //right punch
            else if (Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKey(KeyCode.UpArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.LeftControl))
                {
                    fightLogic.singleton.StartPlayerAnimation("rightPunch");
                    PlayerState = "RightPunch";
                }
            }

            //highpunch up+a/b
            //TODO: lasers etc.
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.UpArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("highPunch");
                    PlayerState = "HighPunch";
                }
            }

            //left/right dodge
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.LeftArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("leftDodge");
                    PlayerState = "LeftDodge";
                    source.PlayOneShot(dodgeSound[Random.Range(0, dodgeSound.Length)]);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.RightArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("rightDodge");
                    PlayerState = "RightDodge";
                    source.PlayOneShot(dodgeSound[Random.Range(0, dodgeSound.Length)]);
                }
            }

            //down, block
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.DownArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("block");
                    PlayerState = "Block";
                    source.PlayOneShot(dodgeSound[Random.Range(0, dodgeSound.Length)]);
                }
            }
        }

    }

    //is this check needed?
    bool IsAnyOtherKeyPressed(KeyCode ignoreKey)
    {
        if (inputKeys == null || inputKeys.Length == 0)
        {
            return false;
        }

        foreach (KeyCode theKey in inputKeys)
        {
            if (theKey != ignoreKey)
            {
                if (Input.GetKeyDown(theKey))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
