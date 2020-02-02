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
    
    //public AudioGroup

    
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
