using UnityEngine;

class StratagemHeroScreenShakeComponent : MonoBehaviour
{
    float _shakeStopTime;
    Vector3[] _originalPositions;
    
    public StratagemHeroScreen Screen;

    public float ShakeTime = .1f;
    public float ShakeMagnitude = .5f;
    public bool IsShaking { get; private set; }

    void Update()
    {
        if (!IsShaking)
            return;

        if (_shakeStopTime <= Time.time)
            StopShake();
        else
            DoShake();
    }

    public void Shake()
    {
        if (IsShaking)
            return;

        IsShaking = true;
        _shakeStopTime = Time.time + ShakeTime;
        _originalPositions = new Vector3[Screen.Commands.Length];
        for (int i = 0; i < Screen.Commands.Length; i++)
            _originalPositions[i] = Screen.Commands[i].transform.position;
    }

    void DoShake()
    {
        for (int i = 0; i < Screen.Commands.Length; i++)
        {
            Screen.Commands[i].transform.position =
                _originalPositions[i] + (Vector3)(Random.insideUnitCircle.normalized * ShakeMagnitude);
        }
    }

    void StopShake()
    {
        IsShaking = false;
        for (int i = 0; i < Screen.Commands.Length; i++)
            Screen.Commands[i].transform.position = _originalPositions[i];
    }

}