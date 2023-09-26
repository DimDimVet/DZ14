using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ControlInventory : MonoBehaviour
{
    public Transform gridTransform;
    public int GridPlater;
    [SerializeField] private Button openCanvasButton;
    [SerializeField] private Button closeCanvasButton;
    [SerializeField] private Canvas canvas;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultListInput;
    private bool isRun;
    private float2 inputMouse;

    void Start()
    {
        openCanvasButton.onClick.AddListener(OpenCanvas);
        closeCanvasButton.onClick.AddListener(CloseCanvas);
        canvas.gameObject.SetActive(false);
        openCanvasButton.gameObject.SetActive(true);

        dataReg = new RegistratorExecutor();//доступ к листу
        rezultListInput = dataReg.GetDataPlayer();
    }

    private void Update()
    {
        //ищем если не нашли
        if (isRun == false)
        {
            rezultListInput = dataReg.GetDataPlayer();
            if (rezultListInput.PhotonIsMainGO)
            {
                if (rezultListInput.UserInput != null)
                {
                    isRun = rezultListInput.PhotonIsMainGO;
                }
            }
        }

        if (isRun)
        {
            GridPlater = rezultListInput.PhotonHash;
        }
    }
    private void ControlPlayer()
    {

    }


    private void OpenCanvas()
    {
        canvas.gameObject.SetActive(true);
        openCanvasButton.gameObject.SetActive(false);
    }
    private void CloseCanvas()
    {
        canvas.gameObject.SetActive(false);
        openCanvasButton.gameObject.SetActive(true);
    }
}
