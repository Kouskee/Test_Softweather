using System.Collections;
using UnityEngine;

public class EffectControlller : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioSource _audio;
    
    [SerializeField] private ParticleSystem _effect;

    private void Start()
    {
        _audio.clip = _clips[Random.Range(0, _clips.Length)];
        
        _audio.Play();
        _effect.Play();

        StartCoroutine(CheckForNullParticle());
    }
    
    private IEnumerator CheckForNullParticle()
    {
        yield return new WaitForSeconds(.5f);
        while (_effect.particleCount > 0)
            yield return null;
        
        Destroy(gameObject);
    }

}
