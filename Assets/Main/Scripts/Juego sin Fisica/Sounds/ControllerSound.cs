using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Graphs;
using UnityEngine;

public class ControllerSound : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    ControllerGame controllerGame;

    [SerializeField] AudioSource ASEnemys;
    [SerializeField] AudioSource ASPlayer;
    [SerializeField] AudioSource ASKey;
    [SerializeField] AudioSource ASGeneral;
    [Space]
    [SerializeField] AudioClip ClipGetKey;
    [SerializeField] AudioClip ClipCapturedKey;

    [Space]
    [SerializeField] AudioClip ClipStepPlayer;
    [SerializeField] AudioClip ClipJumpPlayer;
    [SerializeField] AudioClip ClipLostLifePlayer;
    [SerializeField] AudioClip ClipDeadPlayer;
    [SerializeField] AudioClip ClipPlayButton;
    [SerializeField] AudioClip ClipGeneralMusic;

    [SerializeField] float speedTimeSoundEnemy;
    private float counterTimerSoundEnemy = 0;
    private bool stoped;

    private void Awake()
    {
        if (!configuration) Debug.LogError("Falta asignar el archivo Configuration");
        controllerGame = FindObjectOfType<ControllerGame>();

    }

    public void ReInit()
    {
        stoped = false;
    }

    public void StopGameSounds()
    {
        ASEnemys.Stop();
        ASPlayer.Stop();
        ASKey.Stop();
        stoped = true;
    }
    private void Update()
    {
        if (controllerGame.Playing)
        {
            speedTimeSoundEnemy = configuration.VelocityEnemies;

            counterTimerSoundEnemy += speedTimeSoundEnemy * Time.deltaTime;
            if (counterTimerSoundEnemy > 1)
            {
                PlayEnemy();
                counterTimerSoundEnemy = 0;
            }
        }
        else if (!stoped)
        {
            StopGameSounds();
        }
    }

    public void PlayEnemy() { 
        ASEnemys.Play(); 
    }

    public void PlayStepPlayer()
    {
        ASPlayer.clip = ClipStepPlayer;
        ASPlayer.Play();
    }
    public void PlayPlayerJump() {
        
        ASPlayer.clip = ClipJumpPlayer;
        ASPlayer.Play();
    }

    public void PlayLostLifePlayer()
    {
        ASPlayer.clip = ClipLostLifePlayer;
        ASPlayer.Play();
    }

    public void PlayDeadPlayer()
    {
        ASPlayer.clip = ClipDeadPlayer;
        ASPlayer.Play();
    }

    public void PlayGetKey() {
        ASKey.clip = ClipGetKey;
        ASKey.Play();
    }
    public void PlayCapturedKey()
    {
        ASKey.clip = ClipCapturedKey;
        ASKey.Play();
    }

    public void PlayGame()
    {
        ASGeneral.clip = ClipPlayButton;
        ASGeneral.Play();
    }

    public void PlayGeneral() {
        ASGeneral.clip = ClipGeneralMusic;
        ASGeneral.Play();
    }
}
