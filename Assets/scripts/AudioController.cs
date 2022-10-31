using UnityEngine;

public class AudioController : MonoBehaviour
    {
    [SerializeField] private AudioClip _step;

    private AudioSource _sound;

    private void OnEnable()
        {
        Events.OnPlaySoundMoveTile += PlayStepAudio;
        }
    private void Start()
        {
        _sound = GetComponent<AudioSource>();
        }
    private void OnDisable()
        {
        Events.OnPlaySoundMoveTile -= PlayStepAudio;
        }
    private void PlayStepAudio()
        {
        if(Config.AUDIO)
            _sound.PlayOneShot(_step);
        }
    }