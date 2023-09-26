using Photon.Pun;
using UnityEngine;

public class Healt : MonoBehaviour
{
    [SerializeField] private HealtSetting settingsData;

    [HideInInspector] public int HealtCount = 0;
    [HideInInspector] public int Damage;
    [HideInInspector] public bool Dead = false;

    private bool isOneTriger = true;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultNetManager;

    void Start()
    {
        if (settingsData.Healt != 0)
        {
            HealtCount = settingsData.Healt;
        }
    }

    void Update()
    {
        if (Damage != 0)
        {
            HealtContoll(Damage);
            Damage = 0;
        }
    }
    public void HealtContoll(int damage)
    {
        HealtCount -= damage;
        if (HealtCount <= 0 && isOneTriger)
        {
            Dead = true;
            DestoyGO();
            isOneTriger = false;
        }
    }

    public void DestoyGO()
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultNetManager = dataReg.NetManager();
        rezultNetManager.NetworkManager.DestroyThisGO(this.gameObject);
    }
}
