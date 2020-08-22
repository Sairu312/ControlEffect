using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip break1;
    public AudioClip break2;
    public AudioClip punch1;
    public AudioClip punch2;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject audioManager = GameObject.Find("AudioManager");
        AudioSource audioSourceBGM = audioManager.GetComponent<AudioSource>();
        audioSourceBGM.mute = true;
        StartCoroutine("RingSE");
    }

    IEnumerator RingSE()
    {
        audioSource.PlayOneShot(BGM);
        yield return new WaitForSeconds(10);
        audioSource.PlayOneShot(break1);
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(break2);
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(punch1);
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(punch2);
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(punch1);
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(break1);
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(break2);
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(break1);
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(punch2);
        yield return new WaitForSeconds(3);
        audioSource.PlayOneShot(punch2);
        yield return new WaitForSeconds(2);
        audioSource.PlayOneShot(punch1);
        yield return new WaitForSeconds(7.5f);
        audioSource.PlayOneShot(break1);
        
    }

}
