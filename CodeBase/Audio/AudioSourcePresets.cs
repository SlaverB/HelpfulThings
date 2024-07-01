using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Audio
{
  [CreateAssetMenu(fileName = "AudioSourcePresets", menuName = "Audio/AudioSourcePresets")]
  public class AudioSourcePresets: SerializedScriptableObject
  {
    [SerializeField] private Dictionary<SoundConfig, AudioSourcePres> _audioSourcePresets;
    
    public AudioSourcePres GetAudioSourcePresetsByEnum(SoundConfig soundConfig) => _audioSourcePresets[soundConfig];
  }

  [Serializable]
  public class AudioSourcePres
  {
    public float Volume;
    public float Pitch;
    public bool Loop;
  }
}