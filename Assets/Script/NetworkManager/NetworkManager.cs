using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerSample;
    public List<Transform> SpawnPonts;
    private int id;
    //
    public List<GameObject> InterObject;
    public List<Transform> InterTransformObject;

    private IRegistrator dataReg;
    private List<RegistratorConstruction> rezultList;

    private GameObject tempDestroy;

    //Bulls
    public List<GameObject> bulls;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();//запустим тестовый мастер-сервер
    }

    //возьмем в родителе OnConnectedToMaster() дл€ создани€ тестового мастер-сервера
    public override void OnConnectedToMaster()
    {
        //создали экземпл.настроек, выбрали настройку, задали значение
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = false
        };
        //при старте, ищет комнату, при ее отсутсвии - создает с предуст.настройками
        PhotonNetwork.JoinOrCreateRoom("TestDZ13", roomOptions, TypedLobby.Default);
    }

    //возьмем в родителе OnJoinedRoom()
    public override void OnJoinedRoom()
    {
        id = PhotonNetwork.LocalPlayer.ActorNumber;

        //проверим на ошибку количества
        if (id > (SpawnPonts.Count + 1))
        {
            Debug.Log("Ќет свободной точки");
        }
        else
        {
            GameObject tr = PhotonNetwork.Instantiate(PlayerSample.name, SpawnPonts[id - 1].position, Quaternion.identity);
            tr.name = $"Id {id}";
            //GlobalList.PhotonIdPlayer = id;
            // Debug.Log("ѕодключен игрок по id" + id + " " + PhotonView.Get(this.gameObject).IsMine + " " + PhotonView.Get(this.gameObject).GetHashCode() + " - с номером " + PhotonNetwork.CurrentRoom.PlayerCount);
            if (id == 1)//GO
            {
                for (int i = 0; i < InterTransformObject.Count; i++)
                {
                    PhotonNetwork.Instantiate(InterObject[i].name, InterTransformObject[i].position, Quaternion.identity);
                }
            }
        }
    }

    public void BullInst(Transform outPoint)
    {
        for (int i = 0; i < bulls.Count; i++)
        {
            if (bulls[i]!=null)
            {
                PhotonNetwork.Instantiate(bulls[i].name, outPoint.position, outPoint.rotation);
            }
        }
        
    }

    public void DestroyThisGO(GameObject go)
    {
        if (tempDestroy == go)
        {
            return;
        }
        else
        {
            tempDestroy = go;

            if (PhotonView.Get(go).IsMine)
            {
                PhotonNetwork.Destroy(go);
            }
            else
            {
                this.photonView.RPC("DestroyGO", RpcTarget.All, (int)PhotonView.Get(go).ViewID);
            }
        }
    }

    [PunRPC]//дл€ того чтоб фотон знал о данном методе
    public void DestroyGO(int go)
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultList = dataReg.GetDataList();

        for (int i = 0; i < rezultList.Count; i++)
        {
            if (rezultList[i].PhotonHash == go)
            {
                if (rezultList[i].HealtObj != null)
                {
                    rezultList[i].HealtObj.DestoyGO();
                }

            }
        }

    }

    public void DestroyThisLut(GameObject go)
    {
        if (tempDestroy == go)
        {
            return;
        }
        else
        {
            tempDestroy = go;

            if (PhotonView.Get(go).IsMine)
            {
                PhotonNetwork.Destroy(go);
            }
            else
            {
                this.photonView.RPC("DestroyGO", RpcTarget.All, (int)PhotonView.Get(go).ViewID);
            }
        }
    }

    [PunRPC]//дл€ того чтоб фотон знал о данном методе
    public void DestroyLut(int go)
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultList = dataReg.GetDataList();

        for (int i = 0; i < rezultList.Count; i++)
        {
            if (rezultList[i].PhotonHash == go)
            {
                if (rezultList[i].PickUpItem != null)
                {
                    rezultList[i].PickUpItem.DestoyGO();
                }

            }
        }

    }
    public void SayHello()//сказать фотону отправить указаный метод
    {
        //класс photonView €вл€етс€ генератором пакета, требуетс€ подвесить на gameobject(нейтральный) в проекте
        //важные настройки в инспекторе:синхронизации

        //им€ метода вызова,укажем клиентов,укажем отправл€емые параметры(к примеру вз€та инфа локального клиента)
        this.photonView.RPC("Hello", RpcTarget.All, (byte)PhotonNetwork.LocalPlayer.ActorNumber);
    }

    [PunRPC]//дл€ того чтоб фотон знал о данном методе
    public void Hello(byte playerId)
    {
        Debug.Log($"»грок ID {playerId} передал ѕривет");
    }

}
