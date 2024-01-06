using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    static public CameraController Instance;

    public Camera cam;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float strength, float duration)
    {
        cam.DOShakePosition(duration, strength);
    }
}
