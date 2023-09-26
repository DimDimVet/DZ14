using UnityEngine;

[CreateAssetMenu]
public class CameraSettings : ScriptableObject
{
    [Header("Параметры движения")]
    public float MouseSensor = 0.5f;
    public float MinStopAngle = -30f;
    public float MaxStopAngle = 30f;
}
