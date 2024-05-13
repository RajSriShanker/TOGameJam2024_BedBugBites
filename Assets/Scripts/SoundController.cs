using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource[] jumpAudio;
    public AudioSource[] walkAudio;

    public AudioSource backgroundMusic;

    public AudioSource shootAudio;

    public AudioSource loadAudio;

    public AudioSource winAudio;

    public AudioSource bounceAudio;

    public AudioSource dazedAudio;

    private void Awake()
    {
        int numberOfSoundControllers = FindObjectsOfType<GameController>().Length;
        if (numberOfSoundControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void PlayWinAudio()
    {
        winAudio.Play();
    }

    public void StopWinAudio()
    {
        winAudio.Stop();
    }

    public void PlayShootAudio()
    {
        shootAudio.Play();
    }

    public void PlayLoadAudio()
    {
        loadAudio.Play();
    }

    public void PlayBounceAudio()
    { 

    }

    public void PlayDazedAudio()
    { 
    
    }

    public void PlayWalkAudio()
    {
        int randomIndex = Random.Range(0, walkAudio.Length);
        walkAudio[randomIndex].Play();
    }

    public void PlayJumpAudio()
    {
        int randomIndex = Random.Range(0, jumpAudio.Length);
        jumpAudio[randomIndex].Play();
    }
}
