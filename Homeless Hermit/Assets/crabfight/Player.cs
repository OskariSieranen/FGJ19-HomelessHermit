using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idle state
public class Player : MonoBehaviour
{
    public string PlayerState = "Idle";
    public bool Dodging = false;
    public bool Blocking = false;
    public static Player singleton;
    public KeyCode[] inputKeys;

    //public string PlayerState { get => playerState; set { Debug.Log("state tset " + value); playerState = value; } }

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
    void Update()
    {
        if (PlayerState == "Idle")
        {

            //left punch
            if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKey(KeyCode.UpArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.Space))
                {
                    fightLogic.singleton.StartPlayerAnimation("leftPunch");
                    //goto leftpunch state
                    PlayerState = "LeftPunch";
                    Debug.Log("State: " + PlayerState);
                }
            }

            //right punch
            else if (Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKey(KeyCode.UpArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.LeftControl))
                {
                    fightLogic.singleton.StartPlayerAnimation("rightPunch");
                    //goto rightpunch state
                    PlayerState = "RightPunch";
                    Debug.Log("State: " + PlayerState);
                }
            }

            //highpunch up+a/b
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.UpArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("highPunch");
                    //goto highpunch state
                    PlayerState = "HighPunch";
                    Debug.Log("State: " + PlayerState);
                }
            }

            //left/right dodge
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.LeftArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("leftDodge");
                    PlayerState = "LeftDodge";
                    Debug.Log("State: " + PlayerState);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.RightArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("rightDodge");
                    PlayerState = "RightDodge";
                    Debug.Log("State: " + PlayerState);
                }
            }

            //down, block
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (!IsAnyOtherKeyPressed(KeyCode.DownArrow))
                {
                    fightLogic.singleton.StartPlayerAnimation("block");
                    PlayerState = "Block";
                    Debug.Log("State: " + PlayerState);
                }
            }

            /* enterdodge
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

            fightLogic.singleton.playerDodge = true;
            fightLogic.singleton.animPlayer.SetBool("Dodge", true);
            fightLogic.singleton.StartPlayerAnimation(
            "IdleDodge");
            }
            // exitdodge
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {

            fightLogic.singleton.playerDodge = false;
            fightLogic.singleton.animPlayer.SetBool("Dodge", false);
            }

            */
        }
    }


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
