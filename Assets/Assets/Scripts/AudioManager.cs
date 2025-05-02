using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = Track1;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public AudioSource musicSource;
    public AudioSource SFXSource;


    public AudioClip Track1;
    public AudioClip Track3;
    public AudioClip Jump3;
    public AudioClip Landing1;


}
