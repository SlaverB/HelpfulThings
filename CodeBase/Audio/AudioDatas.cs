using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Audio
{
  [CreateAssetMenu(fileName = "AudioData", menuName = "Audio/AudioData")]
  public class AudioDatas: SerializedScriptableObject
  {
    [SerializeField] private Dictionary<SoundTarget, AudioData> _audioCollection;

    public AudioClip GetAudioClipByEnum(SoundTarget soundTarget) => _audioCollection[soundTarget].AudioClip;
    
    public AudioData GetAudioDataByEnum(SoundTarget soundTarget) => _audioCollection[soundTarget];
  }

  [Serializable]
  public class AudioData
  {
    public AudioClip AudioClip;
    public SoundType SoundType;
    public SoundConfig SoundConfig;
  }
  
  public enum SoundTarget
  {
    TwoDBackground,
    ThreeDBackground,
    TransportAssetSound,
    GameOver,
    PickUp,
    ButtonClick,
    WithinTheShell,
    MainTheme,
  }
  
  public enum SoundType
  {
    Background,
    SoundEffect,
  }

  public enum SoundConfig
  {
    Default,
    LowLevel,
    MidLevel,
    HighLevel,
    GameOver,
    PickUp,
    ButtonClick,
    WithinTheShell,
  }
}