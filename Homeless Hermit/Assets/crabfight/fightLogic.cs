using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightLogic : MonoBehaviour
{
    public static fightLogic singleton;
    public Animator animPlayer;
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
    public void StartPlayerAnimation(string anim)
    {
        if (anim == "leftPunch")
        {

        }
        animPlayer.Play(anim, 0);
    }
}
