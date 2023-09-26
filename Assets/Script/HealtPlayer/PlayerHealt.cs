using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
    [SerializeField] private HealtSetting settingsData;

    [HideInInspector] public int HealtCount;
    [HideInInspector] public int Damage;
    [HideInInspector] public bool Dead = false;

    //private bool isOneTriger = true;

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
            HealtCount -= Damage;
            if (HealtCount <= 0)
            {
                Dead = true;
                DestoyGO();
                //isOneTriger = false;
            }
            Damage = 0;
        }
    }

    public void DestoyGO()
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultNetManager = dataReg.NetManager();
        rezultNetManager.NetworkManager.DestroyThisGO(this.gameObject);
    }
}
