using UnityEngine;


public class Bull : MonoBehaviour
{

    [SerializeField] private BullSettings bullSettings;

    private int hashGO;
    private IRegistrator dataReg;
    private RegistratorConstruction rezultListGO;

    [SerializeField] private GameObject decalGO;
    private int damage;
    private int speed;
    private Collider collaiderBullet;
    private Vector3 startPos;

    private void Start()
    {
        damage = bullSettings.Damage;
        speed = bullSettings.Speed;
        collaiderBullet = gameObject.GetComponent<Collider>();
        startPos = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        RaycastHit hit;
        GameObject decal;
        if (Physics.Linecast(startPos, transform.position, out hit))
        {
            ExecutorCollision(hit);

            collaiderBullet.enabled = false;
            decal = Instantiate(decalGO);
            decal.transform.position = hit.point + hit.normal * 0.001f;
            decal.transform.rotation = Quaternion.LookRotation(-hit.normal);
            Destroy(decal, 1);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5);
        }
        startPos = transform.position;
    }

    private void ExecutorCollision(RaycastHit hit)
    {
        //ищем объект
        hashGO = hit.collider.gameObject.GetHashCode();
        dataReg = new RegistratorExecutor();//доступ к листу
        rezultListGO = dataReg.GetData(hashGO);

        //Healt
        if (rezultListGO.Hash== hashGO)
        {
            if (rezultListGO.HealtObj!=null)
            {
                rezultListGO.HealtObj.Damage=damage;
            }
            if (rezultListGO.PlayerHealt!=null)
            {
                rezultListGO.PlayerHealt.Damage=damage;
            }
        }
        else
        {
            Debug.Log("No Script");
        }
    }
}
