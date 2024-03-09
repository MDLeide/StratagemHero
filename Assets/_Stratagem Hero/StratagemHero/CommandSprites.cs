using System;
using UnityEngine;

[Serializable]
class CommandSprites
{
    public Sprite UpSprite;
    public Sprite RightSprite;
    public Sprite DownSprite;
    public Sprite LeftSprite;

    public Sprite GetSprite(CommandKey key)
    {
        switch (key)
        {
            case CommandKey.Up:
                return UpSprite;
            case CommandKey.Down:
                return DownSprite;
            case CommandKey.Left:
                return LeftSprite;
            case CommandKey.Right:
                return RightSprite;
            default:
                throw new ArgumentOutOfRangeException(nameof(key), key, null);
        }
    }
}