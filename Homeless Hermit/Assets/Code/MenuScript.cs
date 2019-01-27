using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Prime31.TransitionKit;

public class MenuScript : MonoBehaviour
{
    private AudioSource source;
    public int SceneNumberOfFirstScene;
    // Start is called before the first frame update
    void Start()
    {
        source = Camera.main.GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {

            //Transition with TransitionKit
            var rip = new SquaresTransition()
            {
                nextScene = SceneNumberOfFirstScene,
                duration = 1.5f
            };
            TransitionKit.instance.transitionWithDelegate(rip);
            //SceneManager.LoadScene();
        }
    }
}
