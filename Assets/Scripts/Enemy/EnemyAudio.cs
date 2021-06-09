using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip screamClip, dieClip;

    [SerializeField] private AudioClip[] attackClips;

    // Use this for initialization
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayScreamSound()
    {
        audioSource.clip = screamClip;
        audioSource.Play();
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackClips[Random.Range(0, attackClips.Length)];
        audioSource.Play();
    }

    public void PlayDeadSound()
    {
        audioSource.clip = dieClip;
        audioSource.Play();
    }
}
