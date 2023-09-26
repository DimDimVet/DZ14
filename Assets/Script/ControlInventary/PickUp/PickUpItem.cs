using Photon.Pun;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{

    public PickSettings PickSettings;
    [SerializeField] private GameObject objectImg;
    private GameObject objectGrid;

    private Collider collaider;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultList;
    private RegistratorConstruction rezultGrid;
    private RegistratorConstruction rezultNetManager;
    void Start()
    {
        collaider = gameObject.GetComponent<Collider>();
        collaider.isTrigger = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        //if (PhotonView.Get(this.gameObject).IsMine)
        //{
            int tempHsh = other.gameObject.GetHashCode();
            ExecutorCollision(tempHsh);

            collaider.enabled = false;
            DestoyGO();
            //Destroy(gameObject, 1);
        //}
    }
    private void ExecutorCollision(int hash)
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultList = dataReg.GetData(hash);

        rezultGrid = dataReg.Inventory();
        objectGrid= rezultGrid.ControlInventory.gameObject;
        //Healt
       
        if (rezultGrid.ControlInventory != null)
        {
            Debug.Log("Лут взял - "+ rezultList.PhotonHash);
            if (rezultGrid.ControlInventory.GridPlater == rezultList.PhotonHash)
            {
                GameObject.Instantiate(objectImg, rezultGrid.ControlInventory.gridTransform);
            }
            
        }
        else
        {
            Debug.Log("No Script" + rezultList.Name);
        }
    }

    public void DestoyGO()
    {
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultNetManager = dataReg.NetManager();
        rezultNetManager.NetworkManager.DestroyThisLut(this.gameObject);
    }
}
