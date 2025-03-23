using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- AUDIO SOURCE -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----- AUDIO CLIP -----")]
    public AudioClip caveBackground;
    public AudioClip batSqueek;
    public AudioClip bombExplosion;
    public AudioClip bonk;
    public AudioClip collectible;
    public AudioClip dead;
    public AudioClip intro;
    public AudioClip jump;
    public AudioClip laughing;
    public AudioClip punch;
    public AudioClip vaseBreak;
    public AudioClip punchMonkey;
    public AudioClip dart;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource.clip = caveBackground;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}
