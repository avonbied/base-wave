using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAudioSource : MonoBehaviour
{
    public AudioSource MyAudioSource;
    public GameObject MyTrackedObject;
    // Start is called before the first frame update
    void Start()
    {
        StartedPlaying = true;
    }

    public void Rent(GameObject trackingtarget)
    {
        MyAudioSource.Stop();
        MyTrackedObject = trackingtarget;
        this.transform.SetParent(null);
        this.gameObject.SetActive(true);
        if (MyTrackedObject != null)
        {
            this.transform.position = MyTrackedObject.transform.position;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, 1f);
    }
    public bool StartedPlaying = false;
    public bool Expires = true; //Should this Audio be removed when it stops playing
    public void PlaySound(AudioClip clip, float volume)
    {

        if (MyTrackedObject != null)
        {
            this.transform.position = MyTrackedObject.transform.position;
        }
        MyAudioSource.PlayOneShot(clip, volume);


    }
    public void LoopSound(AudioClip clip, float volume)
    {

        MyAudioSource.loop = true;
        MyAudioSource.volume = volume;
        MyAudioSource.clip = clip;
        MyAudioSource.Play();


    }
    // Update is called once per frame
    void Update()
    {
        if (MyTrackedObject != null)
        {
            this.transform.position = MyTrackedObject.transform.position;
        }
        if (StartedPlaying)
        {
            StartedPlaying = false;
        }
        else
        {
            if (Expires)
                if (!MyAudioSource.isPlaying)
                {
                    Return();
                }
        }
    }

    public void Return()
    {
        if (this.gameObject.activeInHierarchy)
        {

            this.gameObject.SetActive(false);
            this.transform.SetParent(AudioManager.Instance.transform);
        }
    }
}
