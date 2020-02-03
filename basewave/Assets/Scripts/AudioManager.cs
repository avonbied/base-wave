using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip PlasmaBoltSound, LaserBeamSound, ShotgunBlastSound, SwordSound;
    public AudioClip BaseDamagedSound;
    public AudioClip[] RepairSounds;
    public AudioClip[] EnemyDeathSounds;
    public AudioClip UnitSpawnSound;
    public AudioClip HUDClickSound;
    public AudioClip EnemySuicideExplosionSound;
    public AudioClip EnemySuicideScreechSound;
    public AudioClip RobotDialogue1Sound, RobotDialogue2Sound, RobotDialogue3Sound, RobotDialogue4Sound;
    public AudioSource MyLocalAudioSource;
    public TrackingAudioSource TrackingAudioSourcePrefab;
    public List<TrackingAudioSource> AudioSourcePool = new List<TrackingAudioSource>();
    public int AudioSourcePoolIndex = 0;


    // Start is called before the first frame update
    public static AudioManager Instance { get; private set; }

    void Start()
    {
        if (this != null)
            Destroy(this);

        Instance = this;
        DontDestroyOnLoad(Instance);
    }

    //public AudioGroup
    public static void PlayLocalSound(AudioClip clip)
    {
        PlayLocalSound(clip, 1f);
    }

    public static void PlayLocalSound(AudioClip clip, float volume)
    {
        if (Instance != null)
        {
            Debug.Log("play");
            Instance.MyLocalAudioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("No AudioManager in Scene.");
        }
    }

    public static void PlayTrackedSound(AudioClip clip, float volume, GameObject o)
    {
        TrackingAudioSource ts = null;
        for (int i = 0; i < AudioManager.Instance.AudioSourcePool.Count; i++)
        {
            if ((AudioManager.Instance.AudioSourcePool[i] != null) && (!AudioManager.Instance.AudioSourcePool[i].gameObject.activeInHierarchy))
            {
                ts = AudioManager.Instance.AudioSourcePool[i];
                break;
            }
        }
        if (ts == null)
        {
            ts = GameObject.Instantiate(Instance.TrackingAudioSourcePrefab, Instance.transform);

        }
        else
        {
            AudioManager.Instance.AudioSourcePool.Remove(ts); //Put it at the back of the list when activated so that the inactive ones show up first
        }
        AudioManager.Instance.AudioSourcePool.Add(ts);
        ts.Rent(o);
        ts.PlaySound(clip, volume);
    }
    //public static TrackingAudioSource PlaySoundOnObject(AudioClip clip,float volume,GameObject obj)
    //{

    //}

    // Update is called once per frame
    void Update()
    {

    }
}
