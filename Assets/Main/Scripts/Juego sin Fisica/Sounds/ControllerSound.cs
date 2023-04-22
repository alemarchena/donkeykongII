using System.Collections;
using System.Collections.Generic;
using UnityEditor.Graphs;
using UnityEngine;

public class ControllerSound : MonoBehaviour
{
    [SerializeField] Configuration configuration;
    ControllerGame controllerGame;

    [SerializeField] AudioSource ASEnemys;
    [SerializeField] AudioSource ASPlayer;
    [SerializeField] AudioSource ASKey;
    [SerializeField] AudioSource ASGeneral;

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

    public void PlayPlayerJump() {
        ASPlayer.Play();
    }
    public void PlayKey() { }
    public void PlayGeneral() { }
}
