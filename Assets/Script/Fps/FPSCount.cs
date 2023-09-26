using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCount : MonoBehaviour
{
    [HideInInspector] public int FPS;
    [HideInInspector] public int HiFPS=0;
    [HideInInspector] public int LoFPS=0;
    [SerializeField] private int frameRange = 60;
    private int[] frameBuffer;
    private int frameBufferIndex;
    private int sum, currentFrame;
    private void Awake()
    {
        if (frameRange <= 0)
        {
            frameRange = 60;
        }
        else
        {
            frameBuffer = new int[frameRange];
        }
        frameBufferIndex = 0;
    }

    void Update()
    {
        UpDataBuffer();
    }

    public void UpDataBuffer()
    {
        frameBufferIndex++;

        if (frameBufferIndex >= frameRange)
        {
            CountFPS();
            frameBufferIndex = 0;
        }
        else
        {
            frameBuffer[frameBufferIndex] = (int)(1f / Time.unscaledDeltaTime);
        }

    }
    private void CountFPS()
    {
        sum = 0;
        for (int i = 0; i < frameBuffer.Length; i++)
        {
            currentFrame = frameBuffer[i];

            sum += currentFrame;
            FPS = sum / frameBuffer.Length;
            if (FPS > HiFPS)
            {
                HiFPS = FPS;
            }
            else if (FPS < HiFPS && FPS < LoFPS)
            {
                LoFPS = FPS;
            }
            else if (LoFPS == 0)
            {
                LoFPS = FPS;
            }
        }

    }
}
