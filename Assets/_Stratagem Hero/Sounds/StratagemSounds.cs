using System.Collections.Generic;
using Cashew.Utility.Extensions;
using UnityEngine;

class StratagemSounds : MonoBehaviour
{
    public AudioSource AudioSource;

    public StratagemSoundClips Clips;
    public bool SoundOn = true;

    public void MenuConfirm() => Play(Clips.MenuConfirmClips);
    public void MenuSelect() => Play(Clips.MenuSelectClips);
    public void MenuCancel() => Play(Clips.MenuCancelClips);
    public void RoundEndScreenTextAppears() => Play(Clips.RoundEndScreenTextAppearsClips);
    public void GameOverScreenTextAppears() => Play(Clips.GameOverScreenTextAppearsClips);
    public void Error() => Play(Clips.ErrorClips);
    public void BadCommand() => Play(Clips.BadCommandClips);
    public void CorrectCommand() => Play(Clips.CorrectCommandClips);
    public void StratagemComplete() => Play(Clips.StratagemCompleteClips);
    public void RoundComplete() => Play(Clips.RoundCompleteClips);
    public void PerfectRound() => Play(Clips.PerfectRoundClips);
    public void PlayBegin() => Play(Clips.PlayBeginClips);
    public void GameOver() => Play(Clips.GameOverClips);
    void Play(List<AudioClip> clips)
    {
        if (!SoundOn)
            return;

        AudioSource.PlayOneShot(clips.Choose());
    }
}