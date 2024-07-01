using UnityEngine;

namespace CodeBase.Audio
{
  public interface IAudioManager
  {
    public void LoadAndPlayClipFromSource(SoundTarget clipTarget);
  }
}