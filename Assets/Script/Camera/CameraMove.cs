using System;
using Unity.Mathematics;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [HideInInspector] public float2 AngleCamera;
    [HideInInspector] public Transform GetTransformPointCamera;
    //
    [SerializeField] private CameraSettings cameraSettings;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultListInput;

    private float2 inputMouse;
    private float yRot;
    private float mouseSensor;
    private float minStopAngle;
    private float maxStopAngle;

    private bool isRun;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //settings
        mouseSensor = cameraSettings.MouseSensor;
        minStopAngle = cameraSettings.MinStopAngle;
        maxStopAngle = cameraSettings.MaxStopAngle;

        //Ќайдем источника мыши управлени€ камерой
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultListInput = dataReg.GetDataPlayer();

    }
    void Update()
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
            inputMouse = rezultListInput.UserInput.InputData.Mouse;

            AngleCamera = inputMouse * mouseSensor * Time.deltaTime;
            yRot -= AngleCamera.y;
            yRot = Math.Clamp(yRot, minStopAngle, maxStopAngle);
            transform.localRotation = Quaternion.Euler(yRot, 0, 0);

            if (GetTransformPointCamera != null)
            {
                transform.position = GetTransformPointCamera.position/*+setPos*/;//получим позицию объекта прив€зки
                transform.rotation = GetTransformPointCamera.rotation;
            }
        }

    }
}
