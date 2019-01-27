using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Prime31.TransitionKit;

public class fightLogic : MonoBehaviour
{
    public string returnscene = PlayData.NextScene;
    public static fightLogic singleton;
    public Animator animPlayer;

    //Health related
    public int PlayerHealthMax = PlayData.Health;
    public int EnemyHealthMax = PlayData.EnemyHealth;
    public int PlayerHealth = PlayData.Health;
    public int EnemyHealth = PlayData.EnemyHealth;
    public int PlayerPower = PlayData.Power;
    public int EnemyPower = PlayData.EnemyPower;

    public bool EnemyEnteredHurtState = false;

    public Slider PlayerHealthSlider;
    public Slider EnemyHealthSlider;

    //audio
    public AudioClip bellSound;
    public AudioClip fillSound;
    public AudioClip pHurtSound;
    public AudioClip pBlockSound;
    public AudioClip eHurtSound;
    public AudioClip transitionSound;
    public AudioClip fightBGM;
    public AudioClip eBlockSound;
    public AudioClip[] splosions;
    public AudioClip DeathCry;

    private AudioSource source;
    private AudioSource bgmSource;

    //intro stuff
    private bool intro = true;
    private int introphase = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;

        //DEBUG delete later
        PlayerHealthMax = 20;
        PlayerHealth = 20;
        PlayerPower = 1;
        EnemyHealthMax = 20;
        EnemyHealth = 1;
        EnemyPower = 1;

        //set health bars
        PlayerHealthSlider.value = 0;
        EnemyHealthSlider.value = 0;
        PlayerHealthSlider.minValue = 0;
        PlayerHealthSlider.maxValue = PlayerHealthMax;
        EnemyHealthSlider.minValue = 0;
        EnemyHealthSlider.maxValue = EnemyHealthMax;

        //set audio sources
        source = GetComponent<AudioSource>();
        bgmSource = Camera.main.GetComponent<AudioSource>();
        //entering scene sfx
        source.PlayOneShot(transitionSound);


    }

    // Update is called once per frame
    private void Update()
    {
        //intro plays once when entering scene
        if (intro)
        {
            switch (introphase)
            {
                case 0:
                    if (!source.isPlaying)
                    {
                        source.PlayOneShot(fillSound);
                        introphase = 1;
                    }
                    break;
                case 1:
                    if (PlayerHealthSlider.value != PlayerHealthSlider.maxValue)
                    {
                        PlayerHealthSlider.value += 0.25f;
                    }
                    if (EnemyHealthSlider.value != EnemyHealthSlider.maxValue)
                    {
                        EnemyHealthSlider.value += 0.25f;

                    }
                    if (PlayerHealthSlider.value == PlayerHealthSlider.maxValue && EnemyHealthSlider.value == EnemyHealthSlider.maxValue)
                    {
                        if (!source.isPlaying)
                        {
                            introphase = 2;
                            source.PlayOneShot(bellSound);
                        }
                    }
                    break;
                case 2:
                    if (!source.isPlaying)
                    {
                        
                        bgmSource.Play();
                        intro = false;
                        Player.singleton.PlayerState = "Idle";
                        pahisAI.singleton.PahisState = "Idle";
                    }
                    break;
            }
        }
        //normal gameplay loop
        else
        {
            //check if player hit HP
            if (pahisAI.singleton.HurtPlayer && !Player.singleton.Dodging)
            {
                pahisAI.singleton.HurtPlayer = false;
                if (Player.singleton.Blocking)
                {
                    PlayerHealth -= EnemyPower / 2;
                    source.PlayOneShot(pBlockSound);
                }
                else
                {
                    PlayerHealth -= EnemyPower;
                    source.PlayOneShot(pHurtSound);
                    Player.singleton.PlayerState = "Idle";
                    animPlayer.Play("hurt", 0);
                    //TODO: screenshake
                    Camera.main.GetComponent<CameraShake>().shakeDuration = .05f;
                }
                PlayerHealthSlider.value = PlayerHealth;
            }
            //check if enemy hit
            if (Player.singleton.HurtEnemy)
            {
                //check if enemy blocking
                if (!pahisAI.singleton.Shield)
                {
                    //extra check due to animation system quirks
                    if (EnemyEnteredHurtState == false)
                    {
                        EnemyEnteredHurtState = true;
                        EnemyHealth -= PlayerPower;
                        EnemyHealthSlider.value = EnemyHealth;
                        pahisAI.singleton.PahisState = "Idle";
                        pahisAI.singleton.animPahis.Play("pahisIdle", 0);

                        //enemy hurt sfx
                        source.PlayOneShot(eHurtSound);
                        //TODO: screen shake
                        Camera.main.GetComponent<CameraShake>().shakeDuration = .05f;
                    }
                }
                //enemy blocked attack
                else
                {
                    source.PlayOneShot(eBlockSound);
                }
            }
            Player.singleton.HurtEnemy = false;
            pahisAI.singleton.HurtPlayer = false;

            //win if enemy hp 0
            if (EnemyHealth == 0)
            {
                bgmSource.Stop();
                //stop player and cpu
                Player.singleton.PlayerState = null;
                animPlayer.speed = 0;
                pahisAI.singleton.PahisState = null;
                pahisAI.singleton.animPahis.Play("pahisPrepAttack");
                EnemyHealth -= 1;

                //death noise & explosions
                source.PlayOneShot(DeathCry);
                StartCoroutine(PlaySound());
                StartCoroutine(CountDown());
                //TODO: voittotunnari
                
            }
            //TODO: game over
            else if (PlayerHealth == 0)
            {
                //game over
            }
        }

    }
    public void StartPlayerAnimation(string anim)
    {
        animPlayer.Play(anim, 0);
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(8);
        var pixelater = new PixelateTransition()
        {
            nextScene = 2,
            finalScaleEffect = PixelateTransition.PixelateFinalScaleEffect.ToPoint,
            duration = 1.0f
        };
        TransitionKit.instance.transitionWithDelegate(pixelater);
        //SceneManager.LoadScene(returnscene, LoadSceneMode.Single);
    }

    //explosion thing
    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(Random.Range(0f, .4f));

        source.PlayOneShot(splosions[Random.Range(0, splosions.Length - 1)], 1f);
        //TODO: splosion effects, screen shake
        Camera.main.GetComponent<CameraShake>().shakeDuration = .1f;
        StartCoroutine(PlaySound());
    }
}
