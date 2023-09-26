using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftSettings", menuName = "Creat Craft Data")]
public class CraftSettings : ScriptableObject
{
    public List<CraftCombinat> combinatItem;
}

[Serializable]
public class CraftCombinat
{
    public List<string> Sources;
    public GameObject rezult;
}
