using System;
using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour
{
    public float fps = 30.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private MeshRenderer rendererMy;

    void Start()
    {
        rendererMy = GetComponent<MeshRenderer>();
        NextFrame();
      //  InvokeRepeating("NextFrame", 1 / fps, 1 / fps);
    }

    float timer;
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer > 0) return;
        NextFrame();
        timer = 1 / fps;
    }

    void NextFrame()
    {
        rendererMy.sharedMaterial.SetTexture("_MainTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
    }
}