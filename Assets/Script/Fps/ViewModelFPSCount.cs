using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModelFPSCount : MonoBehaviour
{
    [SerializeField] private Text textCountFPS;
    [SerializeField] private Text textMinFPS;
    [SerializeField] private Text textMaxFPS;
    private int countTextDigital = 500;
    private string[] textDigital;

    private FPSCount fpsCount;
    void Start()
    {
        fpsCount = GetComponent<FPSCount>();
        textDigital=new string[countTextDigital];

        for (int i = 0; i < countTextDigital; i++) 
        {
            textDigital[i] = $"{i}";
        }

    }
    void Update()
    {
        textCountFPS.text = textDigital[Mathf.Clamp(fpsCount.FPS, 0, countTextDigital)];
        textMinFPS.text = $"min-{textDigital[Mathf.Clamp(fpsCount.LoFPS, 0, countTextDigital)]}";
        textMaxFPS.text = $"max-{textDigital[Mathf.Clamp(fpsCount.HiFPS, 0, countTextDigital)]}";
    }
}
