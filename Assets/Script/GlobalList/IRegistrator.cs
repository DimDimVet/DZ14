using System.Collections.Generic;
using UnityEngine;

public interface IRegistrator
{
    public Transform OutPos { get; set; }
    List<RegistratorConstruction> GetDataList();
    void SetData(RegistratorConstruction data);
    RegistratorConstruction GetData(int hash);
    RegistratorConstruction GetDataCamera();
    RegistratorConstruction GetDataPlayer();
    void DestroyObject(RegistratorConstruction go);
    RegistratorConstruction NetManager();
    RegistratorConstruction Inventory();
}
