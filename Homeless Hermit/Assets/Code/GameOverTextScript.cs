using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameOverTextScript : MonoBehaviour
{
    public string[] DeathTexts = { "“A thing is not necessarily true because a man dies for it.” ― Oscar Wilde",
    "“I do not fear death. I had been dead for billions and billions of years before I was born, and had not suffered the slightest inconvenience from it.” ― Mark Twain",
    "“Unbeing dead isn't being alive.” ― E. E. Cummings",
    "“Life should not be a journey to the grave with the intention of arriving safely in a pretty and well preserved body, but rather to skid in broadside in a cloud of smoke, thoroughly used up, totally worn out, and loudly proclaiming “Wow! What a Ride!” ― Hunter S. Thompson",
    "“I go to seek a Great Perhaps.” ― François Rabelais" };
    public string ChosenText;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        ChosenText = DeathTexts[Random.Range(0, DeathTexts.Length - 1)];

        text = GetComponent<Text>();
        text.text = ChosenText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
