using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimatorEventController : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private List<AudioClip> footstepClips;
    
    public void PlayRandomFootstep()
    {
        var randomClip = footstepClips[Random.Range(0, footstepClips.Count)];
        footstepAudioSource.PlayOneShot(randomClip);
    }
}
