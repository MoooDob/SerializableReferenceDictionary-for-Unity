// Copyright 2020 Marc Rüdel. All Rights Reserved.
// based on previous work of (https://github.com/azixMcAze/Unity-SerializableDictionary)


using UnityEngine;


namespace SerializableReferenceDictionary.Example
{

    //[Serializable]
    [CreateAssetMenu(fileName = "new SerializableDictionaryExampleSO", menuName = "Create SerializableDictionaryExampleSO")]
    public class SerializableDictionaryExampleSO : ScriptableObject
    {

        //    public GameObject aGameObject;

        //    // The dictionaries can be accessed throught a property
        //    [SerializeField]
        //    StringStringDictionary m_stringStringDictionary = null;
        //    public IDictionary<string, string> StringStringDictionary
        //    {
        //        get { return m_stringStringDictionary; }
        //        set { m_stringStringDictionary.CopyFrom(value); }
        //    }

        //    public ObjectColorDictionary m_objectColorDictionary;
        //    public StringColorArrayDictionary m_stringColorArrayDictionary;
        //#if NET_4_6 || NET_STANDARD_2_0
        //    public StringHashSet m_stringHashSet;
        //#endif

        //[SerializeField]
        public StringMyClassDictionary stringMyClassDictionary;

        public void Reset()
        {
            // access by property
            //StringStringDictionary = new Dictionary<string, string>() { { "first key", "value A" }, { "second key", "value B" }, { "third key", "value C" } };
            //m_objectColorDictionary = new ObjectColorDictionary() { { aGameObject, Color.blue }, { this, Color.red } };
            stringMyClassDictionary = new StringMyClassDictionary();
        }
    }
}