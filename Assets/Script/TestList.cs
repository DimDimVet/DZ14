using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestList : MonoBehaviour
{
    public bool Str=false;
    List<RegistratorConstruction> tt;
    void Start()
    {
        RegistratorExecutor executor = new RegistratorExecutor();
        tt = GlobalList.DataObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Str)
        {
            for (int i = 0; i < tt.Count; i++)
            {
                Debug.Log($"{tt[i].PhotonIsMainGO}- {tt[i].Hash} - {PhotonView.Get(this.gameObject).IsMine} -{tt[i].Name}");
            }
            Str = false;
        }
    }
}
