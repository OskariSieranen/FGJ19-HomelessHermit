using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public string NextLevel;

    // Start is called before the first frame update
    void Start()
    {
        PlayData.NextScene = NextLevel;
        PlayData.CurrentScene = SceneManager.GetActiveScene().buildIndex;
        PlayData.Power = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
