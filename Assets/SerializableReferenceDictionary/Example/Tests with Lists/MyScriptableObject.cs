using System;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "new MyScriptableObject", menuName = "Create MyScriptableObject")]
public class MyScriptableObject : ScriptableObject
{
    [SerializeField] public List<CoolClass> objList;
    [NonSerialized] public bool setNewIdOnFirstItem = false;
    void Awake() {
        CoolClass anObject = new CoolClass { id = 123456 };
        if (objList == null)
            objList = new List<CoolClass>() { null, anObject, anObject, null, anObject };
        (objList[0] as CoolClass).id = (int)UnityEngine.Random.Range(0, 1000000);
    }
}
