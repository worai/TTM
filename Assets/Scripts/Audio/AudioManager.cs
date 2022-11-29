using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

  [SerializeField] private AudioMixer mixer;
  [SerializeField] private AudioSource src;

  [SerializeField] private AudioMixerSnapshot maxSnapShot;
  [SerializeField] private AudioMixerSnapshot minSnapShot;

  public Sound[] sounds;

  private void Awake()
  {
    // make the gameobject persist between scenes!
    DontDestroyOnLoad(gameObject);

    foreach(Sound s in sounds)
    {
      s.source = gameObject.AddComponent<AudioSource>();
      s.source.clip = s.clip;
      s.source.volume = s.volume;
      s.source.pitch = s.pitch;
      
    }

  }

  private void Start()
  {
    // when this guy is in Start, and not in Awake, then it is respected
    // TODO fix audio
    //  - make it set the volume before the first frame
    //  - OR don't play anything on the first frame
    //  - OR make the music softer on the first frame... <- prob not an option
    //  - OR just make it volue zero from the start!
    //if(!mixer.SetFloat("UserMusicVolume", -80f)) Debug.LogError("not found");
    //src.enabled = true;
    mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { maxSnapShot }, new float[] { 1f }, 1f);
  }

}
