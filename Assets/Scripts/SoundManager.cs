using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource kickShotSource;
    public AudioSource hitScoreSource;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {  
        Player.onCollisionWithBallSound += PlayKickShot;
        ScoreBox.onHitScoreBoxSound += PlayHitScore;
    }

    private void OnDestroy()
    {
        Player.onCollisionWithBallSound -= PlayKickShot;
        ScoreBox.onHitScoreBoxSound -= PlayHitScore;
    }

    public void PlayKickShot()
    {
        if (!kickShotSource.isPlaying)
            kickShotSource.Play();
    }

    public void PlayHitScore()
    {
        hitScoreSource.Play();
    }

    public void PlayBGM()
    {
        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
}
