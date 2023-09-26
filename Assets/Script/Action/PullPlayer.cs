using Photon.Pun;
using UnityEngine;
using Zenject;

public class PullPlayer : MonoBehaviour
{
                                        //
    [SerializeField] private MoveSettings moveSettings;

    private IRegistrator dataReg;
    private RegistratorConstruction rezultListInput;

    public Transform[] PointGnd;
    private Rigidbody rigidbodyObj;
    private float force, height;
    private LayerMask groundLayer;

    private float shootDelay;
    private float shootTime = float.MinValue;
    void Start()
    {
        rigidbodyObj = gameObject.GetComponent<Rigidbody>();

        force = moveSettings.Force;
        height = moveSettings.Height;
        groundLayer = moveSettings.GroundLayer;
        shootDelay= moveSettings.ShootDelay;

        //ищем управление
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultListInput = dataReg.GetDataPlayer();

    }

    void Update()
    {
        if (PhotonView.Get(this.gameObject).IsMine)
        {
            if (rezultListInput.UserInput == null)
            {
                rezultListInput = dataReg.GetDataPlayer();
                return;
            }

            if (rezultListInput.UserInput.InputData.Pull != 0)//получим нажатие
            {
                Jamp();
            }
        }
        
    }

    public void Jamp()
    {
        if (Time.time < shootTime + shootDelay)
        {
            return;
        }
        else
        {
            shootTime = Time.time;
        }

        if (GndControl())
        {
            Vector3 tempVector = transform.forward;
            tempVector.y = height;
            tempVector.z = tempVector.z * force;
            tempVector.x = tempVector.x * force;

            rigidbodyObj.AddForce(tempVector, ForceMode.Impulse);
        }

    }
    private bool GndControl()
    {
        for (int i = 0; i < PointGnd.Length; i++)//переберем все точки-датчики контакта с GND
        {
            if (Physics.CheckSphere(PointGnd[i].position, 0.1f, groundLayer, QueryTriggerInteraction.Ignore))
            {
                return true;
            }
        }
        return true;
    }
}
