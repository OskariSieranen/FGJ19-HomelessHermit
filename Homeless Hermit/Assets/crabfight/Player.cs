using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Idle state
public class Player : MonoBehaviour
{
    public string PlayerState;
    public static Player singleton;

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
            if (Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.UpArrow))
            {
                fightLogic.singleton.StartPlayerAnimation(
                     "leftPunch");
                //goto leftpunch state
                PlayerState = "LeftPunch";
            }

            //right punch
            if (Input.GetKeyDown(KeyCode.LeftControl) && !Input.GetKeyDown(KeyCode.UpArrow))
            {
                fightLogic.singleton.StartPlayerAnimation(
                     "rightPunch");
                //goto rightpunch state
                PlayerState = "RightPunch";
            }

            //highpunch up+a/b
            if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftControl))
            {
                fightLogic.singleton.StartPlayerAnimation(
                     "highPunch");
                //goto highpunch state
                PlayerState = "HighPunch";
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
            //left/right dodge
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                fightLogic.singleton.StartPlayerAnimation(
                    "leftDodge");
                PlayerState = "LeftDodge";
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                fightLogic.singleton.StartPlayerAnimation(
                    "rightDodge");
                PlayerState = "RightDodge";
            }
            //down, block
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                fightLogic.singleton.StartPlayerAnimation(
                    "block");
                PlayerState = "Block";
            }

        }

    }
}