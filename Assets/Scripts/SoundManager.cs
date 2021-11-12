using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static bool audioIsPlaying = false;
    public static AudioClip ballSound;
    public static AudioClip strikeSound;
    static AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        ballSound = Resources.Load<AudioClip>("Audio/ball");
        strikeSound = Resources.Load<AudioClip>("Audio/strike");
        audioSource = GetComponent<AudioSource>();
    }

    public static void playBallSound()
    {
        audioSource.PlayOneShot(ballSound);
    }

    public static void playStrikeSound()
    {
        if (!audioIsPlaying)
        {
            audioIsPlaying = true;
            audioSource.PlayOneShot(strikeSound);
        }
    }
}
