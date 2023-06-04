using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource, bgmAudioSource;

    [SerializeField]
    private AudioClip coinClip, hitClip, clickClip, flapClip, gameOverClip, highScoreClip;

    private static AudioManager instance;

    public static AudioManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void PlayCoin()
    {
        audioSource.PlayOneShot(coinClip, 1);
    }
    public void PlayHit()
    {
        audioSource.PlayOneShot(hitClip, 1f);
    }
    public void PlayClick()
    {
        audioSource.PlayOneShot(clickClip, 1);
    }
    public void PlayFlap()
    {
        audioSource.PlayOneShot(flapClip, 1f);
    }
    public void PlayHighScore()
    {
        audioSource.PlayOneShot(highScoreClip, 1f);
    }
    public void PlayGameover()
    {
        float time = audioSource.time;
        bgmAudioSource.volume = .04f;
        bgmAudioSource.PlayOneShot(gameOverClip, 1f);

        LeanTween.delayedCall(gameOverClip.length, () =>
        {
            LeanTween.value(.1f, .4f, .5f).setOnUpdate((float f) =>
            {
                bgmAudioSource.volume = f;
            });
        });
    }
}