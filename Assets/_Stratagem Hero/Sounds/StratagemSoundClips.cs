using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
class StratagemSoundClips
{
    public List<AudioClip> MenuConfirmClips;

    public List<AudioClip> MenuSelectClips;
    public List<AudioClip> MenuCancelClips;
    public List<AudioClip> RoundEndScreenTextAppearsClips;
    public List<AudioClip> GameOverScreenTextAppearsClips;

    [Space]
    public List<AudioClip> ErrorClips;

    [Space]
    public List<AudioClip> BadCommandClips;

    public List<AudioClip> CorrectCommandClips;
    public List<AudioClip> StratagemCompleteClips;
    public List<AudioClip> RoundCompleteClips;
    public List<AudioClip> PerfectRoundClips;

    [Space]
    public List<AudioClip> PlayBeginClips;

    public List<AudioClip> GameOverClips;
}