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
    //public AudioGroup
    public static void PlayLocalSound(AudioClip clip)
    {
        PlayLocalSound(clip, 1f);
    }
    public static void PlayLocalSound(AudioClip clip,float volume)
    {
        if (TheAudioManager != null)
        {
            TheAudioManager.MyLocalAudioSource.PlayOneShot(clip, volume);
        } else
        {
            Debug.LogWarning("No AudioManager in Scene.");
        }
    }

    public static void PlayTrackedSound(AudioClip clip,float volume,GameObject o)
    {
        TrackingAudioSource ts = null;
        for (int i = 0; i < AudioManager.TheAudioManager.AudioSourcePool.Count; i++)
        {
            if ((AudioManager.TheAudioManager.AudioSourcePool[i] != null) && (!AudioManager.TheAudioManager.AudioSourcePool[i].gameObject.activeInHierarchy))
            {
                ts = AudioManager.TheAudioManager.AudioSourcePool[i];
                break;
            }
        }
        if (ts == null)
        {
            ts = GameObject.Instantiate(TheAudioManager.TrackingAudioSourcePrefab,TheAudioManager.transform);
            
        } else
        {
            AudioManager.TheAudioManager.AudioSourcePool.Remove(ts); //Put it at the back of the list when activated so that the inactive ones show up first
        }
        AudioManager.TheAudioManager.AudioSourcePool.Add(ts);
        ts.Rent(o);
            ts.PlaySound(clip,volume);
    }
    //public static TrackingAudioSource PlaySoundOnObject(AudioClip clip,float volume,GameObject obj)
    //{

    //}
    
    // Start is called before the first frame update
    public static AudioManager TheAudioManager;
    void Start()
    {
        
        TheAudioManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
