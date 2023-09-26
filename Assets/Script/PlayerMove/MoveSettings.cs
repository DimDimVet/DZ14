using UnityEngine;

[CreateAssetMenu]
public class MoveSettings : ScriptableObject
{
    [Header("Параметры движения")]
    public float SpeedMove = 100f;

    [Header("Параметры прыжка")]
    public float Force = 1f;
    public float Height = 1f;

    [Header("Указать слой GND")]
    public LayerMask GroundLayer;

    [Header("Задержка нажатия")]
    public float ShootDelay=1f;
}
