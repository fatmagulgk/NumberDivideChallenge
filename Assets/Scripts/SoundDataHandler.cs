using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundDataHandler : MonoBehaviour
{
    AudioSource _source;
    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    public void PlayClip(AudioClip _clip)
    {
        _source.clip = _clip;
        _source.Play();

    }
    public void PlayOneShotClip(AudioClip _clip)
    {
        _source.PlayOneShot(_clip);
        Destroy(gameObject,0.2f);
    }
}
