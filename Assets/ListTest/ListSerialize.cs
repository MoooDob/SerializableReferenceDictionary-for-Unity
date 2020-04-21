using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CoolClass { public int id; }


[ExecuteInEditMode]
[System.Serializable]
public class ListSerialize : MonoBehaviour
{
    [SerializeField]
    public static CoolClass anObject = new CoolClass
    {
        id = 123456
    };

    [SerializeField]
    public List<CoolClass> objList;

    public bool setNewIdOnFirstItem = false;

    void Start()
    {
        if (anObject == null)
        {
            anObject = new CoolClass
            {
                id = 123456
            };
        }

        if (objList == null)
            objList = new List<CoolClass>() { anObject, anObject, anObject };

        Debug.Log("initial ids:");
        PrintNames();
    }


    void Update()
    {
        if (setNewIdOnFirstItem)
        {
            if (objList != null)
            {
                Debug.Log("before new id:");
                PrintNames();

                (objList[0] as CoolClass).id = (int)Random.Range(0, 1000000);

                Debug.Log("after new id:");
                PrintNames();
            }
            else Debug.LogError("ObjList is null.");

            setNewIdOnFirstItem = false;
        }
    }


    private void PrintNames()
    {
        if (objList != null)
        {
            foreach (CoolClass obj in objList)
            {
                Debug.Log("  id: " + obj.id);
            }
        }
        else Debug.LogError("ObjList is null.");
    }


}
