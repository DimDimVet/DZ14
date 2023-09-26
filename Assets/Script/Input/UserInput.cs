using Photon.Pun;
using UnityEngine;
using Zenject;

public class UserInput : MonoBehaviour
{
    //Подключаем к игроку, класс связка между системой управления MapCurrent и классом Move и Camera
    
    public InputData InputData;//Передадим данные
    public bool DebugLogOnOff;//для тестов
    //Подключим класс MapCurrent(new input)
    private MapCurrent inputAction;

    void Start()
    {

        inputAction = new MapCurrent();//инициализируем карту input
        InputData=new InputData();

        if (inputAction!=null)//проверим на null
        {
            //подпишем на event события нажатий и значения присвоим локальным переменым
            inputAction.UIMap.WASD.performed += context => { InputData.Move = context.ReadValue<Vector2>(); };
            inputAction.UIMap.WASD.started += context => { InputData.Move = context.ReadValue<Vector2>(); };
            inputAction.UIMap.WASD.canceled += context => { InputData.Move = context.ReadValue<Vector2>(); };

            inputAction.Map.WASD.performed += context => { InputData.Move = context.ReadValue<Vector2>(); };
            inputAction.Map.WASD.started += context => { InputData.Move = context.ReadValue<Vector2>(); };
            inputAction.Map.WASD.canceled += context => { InputData.Move = context.ReadValue<Vector2>(); };

            inputAction.Map.Look.performed += context => { InputData.Mouse = context.ReadValue<Vector2>(); };
            inputAction.Map.Look.started += context => { InputData.Mouse = context.ReadValue<Vector2>(); };
            inputAction.Map.Look.canceled += context => { InputData.Mouse = context.ReadValue<Vector2>(); };

            inputAction.Map.Shoot.performed += context => { InputData.Shoot = context.ReadValue<float>(); };
            inputAction.Map.Shoot.started += context => { InputData.Shoot = context.ReadValue<float>(); };
            inputAction.Map.Shoot.canceled += context => { InputData.Shoot = context.ReadValue<float>(); };

            inputAction.Map.Pull.performed += context => { InputData.Pull = context.ReadValue<float>(); };
            inputAction.Map.Pull.started += context => { InputData.Pull = context.ReadValue<float>(); };
            inputAction.Map.Pull.canceled += context => { InputData.Pull = context.ReadValue<float>(); };

            inputAction.Map.ModePlayer.performed += context => { InputData.Mode = context.ReadValue<float>(); };
            inputAction.Map.ModePlayer.started += context => { InputData.Mode = context.ReadValue<float>(); };
            inputAction.Map.ModePlayer.canceled += context => { InputData.Mode = context.ReadValue<float>(); };
            //запустим 
            inputAction.Enable();
        }
        else
        {
            Debug.LogError("Нет связи с классом MapCurrent");
        }
        
    }

    void Update()
    {
        if (DebugLogOnOff)
        {
            Debug.Log($"Движение Х = {InputData.Move.x}, Движение Y = {InputData.Move.y}");
            Debug.Log($"Мышь Х = {InputData.Mouse.x}, Мышь Y = {InputData.Mouse.y}");
            Debug.Log($"Выстрел = {InputData.Shoot}");
            Debug.Log($"Толчек = {InputData.Pull}");
            Debug.Log($"Режим = {InputData.Mode}");
        }
    }
}
