// Copyright 2020 Marc Rüdel. All Rights Reserved.
// based on previous work of (https://github.com/azixMcAze/Unity-SerializableDictionary)


using System;
using UnityEngine;


namespace SerializableReferenceDictionary.Example
{

    [Serializable]
    public class StringStringDictionary : SerializableReferenceDictionary<string, string> { }

    [Serializable]
    public class ObjectColorDictionary : SerializableReferenceDictionary<UnityEngine.Object, Color> { }

    [Serializable]
    public class ColorArrayStorage : SerializableReferenceDictionary.Storage<Color[]> { }

    [Serializable]
    public class StringColorArrayDictionary : SerializableReferenceDictionary<string, Color[], ColorArrayStorage> { }

    [Serializable]
    public class StringMyClassDictionary : SerializableReferenceDictionary<string, MyClass> { }


    [Serializable]
    public class MyClass
    {
        private static int Ctr = 0;
        public static int GetNewClassID() { return Ctr++; }

        public int i;
        public string str;
    }

    [Serializable]
    public class QuaternionMyClassDictionary : SerializableReferenceDictionary<Quaternion, MyClass> { }

#if NET_4_6 || NET_STANDARD_2_0
    [Serializable]
    public class StringHashSet : SerializableReferenceHashSet<string> { }
#endif

}