// Copyright 2020 Marc Rüdel. All Rights Reserved.
// based on previous work of (https://github.com/azixMcAze/Unity-SerializableDictionary)


using UnityEditor;
using UnityEngine;


namespace SerializableReferenceDictionary.Example
{

    [ExecuteInEditMode]
    public class SerializableDictionaryExampleSORef : MonoBehaviour
    {

        [SerializeField]
        public SerializableDictionaryExampleSO scriptableObject;

        public bool addCurrentObj = false;
        public bool clearDict = false;

        [Space(10)]
        public string objKey = "Item 0";
        public MyClass currentObj;
        public bool createNewObj = false;

        // Update is called once per frame
        void Update()
        {
            if (addCurrentObj)
            {
                if (scriptableObject != null)
                {
                    if (currentObj == null)
                    {
                        currentObj = new MyClass();
                        currentObj.i = (int)Random.Range(0, 100000);
                        currentObj.str = "MyClass " + MyClass.GetNewClassID();
                    }
                    scriptableObject.stringMyClassDictionary.Add(objKey, currentObj);

                    EditorUtility.SetDirty(scriptableObject);
                    AssetDatabase.SaveAssets();
                }
                addCurrentObj = false;
            }

            if (createNewObj)
            {

                currentObj = new MyClass();
                currentObj.i = (int)Random.Range(0, 100000);
                currentObj.str = "MyClass " + MyClass.GetNewClassID();

                createNewObj = false;
            }

            if (clearDict)
            {
                scriptableObject.Reset();
                EditorUtility.SetDirty(scriptableObject);
                AssetDatabase.SaveAssets();

                clearDict = false;
            }
        }
    }
}