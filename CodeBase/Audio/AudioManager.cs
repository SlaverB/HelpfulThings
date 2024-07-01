using CodeBase.Signals;
using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Audio
{
  public class AudioManager : MonoBehaviour, IAudioManager
  {
    private const string AudioDataPath = "Audio/AudioData";
    private const string AudioSourcePresetsPath = "Audio/AudioSourcePresets";
    
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioDatas _audioDatas;
    [SerializeField] private AudioSourcePresets _audioSourcePresets;
    
    [Inject] private SignalBus _signalBus;

    private void OnEnable()
    {
      _signalBus.Subscribe<SoundSignal>(TurnOnOffSounds);
    }
    
    private void OnDisable()
    {
      _signalBus.Unsubscribe<SoundSignal>(TurnOnOffSounds);
    }

    public void LoadAndPlayClipFromSource(SoundTarget clipTarget)
    {
      SoundType clipType = _audioDatas.GetAudioDataByEnum(clipTarget).SoundType;
      
      switch (clipType)
      {
        case SoundType.Background:
          PlayMusic(_audioDatas.GetAudioClipByEnum(clipTarget));
          break;
        case SoundType.SoundEffect:
          PlaySoundEffect(_audioDatas.GetAudioDataByEnum(clipTarget));
          break;
      }
    }

    
    private void PlayMusic(AudioClip clip)
    {
      _musicSource.clip = clip; 
      _musicSource.Play();
    }

    private void PlaySoundEffect(AudioData audioData)
    {
      SetupAudioSource(_sfxSource, audioData.SoundConfig);
      _sfxSource.PlayOneShot(audioData.AudioClip);
    }

    private void SetupAudioSource(AudioSource sfxSource, SoundConfig audioDataSoundConfig)
    {
      AudioSourcePres audioSourcePresets = _audioSourcePresets.GetAudioSourcePresetsByEnum(audioDataSoundConfig);
      sfxSource.volume = audioSourcePresets.Volume;
      sfxSource.pitch = audioSourcePresets.Pitch;
    }
    
    private void TurnOnOffSounds()
    {
      _musicSource.mute = !_musicSource.mute;
      _sfxSource.mute = !_sfxSource.mute;
    }
    
  }
}