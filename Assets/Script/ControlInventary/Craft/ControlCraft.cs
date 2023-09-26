using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlCraft : MonoBehaviour
{
    public CraftSettings CraftSettings;//подключим сеттинг крафта(рецепт)
    [SerializeField] private Button craftButton;//подключим кнопку крафта
    [SerializeField] private Transform gridUI;//укажем трансформ сетки окна инвентаря
    private List<ICraft> items = new List<ICraft>();//локальный лист для хранения найденого объектов с Icraft
    private List<GameObject> select = new List<GameObject>();//локальный лист для хранения выборки листа с Icraft
    void Start()
    {
        craftButton.onClick.AddListener(EnterCraft);//подпишем кнопку на событие
    }
    public void EnterCraft()
    {
        select.Clear();
        items = GetComponentsInChildren<ICraft>().ToList(); //запишем лист найденого (дочер.объекты)

        for (int i = 0; i < items.Count; i++)
        {
            MonoBehaviour itemMB = (MonoBehaviour)items[i];
            if (itemMB.gameObject.TryGetComponent<Button>(out Button butt))//если из найденого в объекте есть кнопка то возврат выполнения
            {
                return;
            }
            else//если из найденого в объекте нет кнопки то присвоим кнопку и подпишемся к событию нажатия
            {
                butt = itemMB.gameObject.AddComponent<Button>();
                butt.onClick.AddListener(() => { Select(butt.gameObject); });//в случае нажатия запустим метод селект
            }
        }
    }

    private void Select(GameObject gameObject)
    {
        if (select.Contains(gameObject))//если содержание селекта имеет данный объект
        {
            select.Remove(gameObject);
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            select.Add(gameObject);
            gameObject.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        ComboCheck();
    }

    private void ComboCheck()
    {
        List<string> selectNames = new List<string>();//инициалю справолчник для имен
        for (int i = 0; i < select.Count; i++)
        {//поищем и запишем в лист имена найденых компонентов с ICraft
            string _name = select[i].GetComponent<ICraft>().Name;
            selectNames.Add(_name);
        }

        for (int i = 0; i < CraftSettings.combinatItem.Count; i++)//возьмем лист с комбинациями из сетинга
        {
            CraftCombinat findItem = CraftSettings.combinatItem[i];
            if (findItem.Sources.SequenceEqual(selectNames))//сравним по имени из найденого с листом из сеттинга
            {
                for (int y = 0; y < select.Count; y++)
                {
                    Destroy(select[y]);//убъем найденое
                }
                GameObject newObject = Instantiate(findItem.rezult, gridUI);//создадим новый объект по префабу из сеттинга
            }
        }
    }

}
