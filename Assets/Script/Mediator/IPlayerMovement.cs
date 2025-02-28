using System;
using UnityEngine;

public interface IPlayerMovement
{
    // PLAYER INPUT
    //public event Action<float> inputHz;
    //public event Action<float> inputVt;
    //public event Action<float> speed;
    //public event Action<float> rotation;

    public void SetInputHz(float value, Action<float> inputHz) => inputHz?.Invoke(value);
    public void SetInputVt(float value, Action<float> inputVt) => inputVt?.Invoke(value);
    public void SetSpeed(float value, Action<float> speed) => speed?.Invoke(value);
    public void SetRotation(float value, Action<float> rotation) => rotation?.Invoke(value);
}
