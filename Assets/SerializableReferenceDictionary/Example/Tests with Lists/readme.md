# ListTest example

This examples shows that simple objects will be cloned, if you serialize them in standard lists. 

[TOC]

------

## Using Unity classes as base for data objects?

In Unity you can work with classes, that are not inherited from Unity classes. In some cases, this may be a more light weight and faster solution than working with Unity classes like `MonoBehavior` and `ScriptableObject`.

But there a couple of drawbacks in using non or no Unity classes as base for you class. For me, one aspect was the most annoying one: My non Unity classes will be cloned after being deserialized. The reason for this behavior is simple: Unity is based on a fast deserialisation system, which only could handle objects as values. So every time I assigned an object A to a serialized member this object will be serialized as set of values, but without an identity.  

### Creating an investigatable example

So, if you create a new class inherited from an Unity class like in the following example, only the values of the object are stored without a hint for the identity of the object. To be able to investigate just the stored information with a simple text editor, I use a class inherited from `ScriptableObject` . 

```c#
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
            objList = new List<CoolClass>() { anObject, anObject, null, anObject };
        (objList[0] as CoolClass).id = (int)UnityEngine.Random.Range(0, 1000000);
    }
}
```

When you create a new object (better: *asset*, because instances of `ScriptableObject`s best should be stored in an asset container) by right clicking in the asset area and select *Create > Create MyScriptableObject*, this new instance will be initialized with a List of four items, three pointing to the same object, one is `null`. This object was initialized with an id of 123456. This id is change to a random number each time the `ScriptableObject` awakes which takes place, when the object has to be reloaded. By the way, have a look on the third item, we declared it to be `null`...

If you have just followed these steps and now have a look in the asset file with your favorite text editor, you will see a [YAML](https://en.wikipedia.org/wiki/YAMLstructure "YAML on Wikipedia") structure of your stored data like shown in the following box.

```yaml
%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!114 &11400000
MonoBehaviour:
  m_ObjectHideFlags: 0
  m_CorrespondingSourceObject: {fileID: 0}
  m_PrefabInstance: {fileID: 0}
  m_PrefabAsset: {fileID: 0}
  m_GameObject: {fileID: 0}
  m_Enabled: 1
  m_EditorHideFlags: 0
  m_Script: {fileID: 11500000, guid: cf388b11c699d3343b5c2e44729306cf, type: 3}
  m_Name: new MyScriptableObject
  m_EditorClassIdentifier: 
  objList:
  - name: default
    id: 210647
  - name: default
    id: 210647
  - name: default
    id: 0
  - name: default
    id: 210647
```

The value of the id is a random number and probably will be different in your asset file.  If you can read the contents of the asset file of your `ScriptableObject`, you have to set the *Asset Serialization Mode* in the *Project Settings*.

![If you can read the contents of the asset file of your ScriptableObject, you have to set the Asset Serialization Mode in the Project Settings.](../../docs/ProjectSettings-ForceText.png)

Now, close Unity, save and than reopen your project. Now, if you have a look in your `ScriptableObject` asset in Unity, you will see a different id *only on the first list item*. Why is there a different id? Because it's defined in the last line of the awake function, so everything is okay. But remember, all list items where pointing to the same object before we closed Unity, so when the id of this object was changed, all list items were showing the new id. Now, only the first item was changed. Our object was cloned in three equal objects, one for each reference.

Furthermore, what about the null references? `null` references will be serialized as objects with default values, so after deserializing your data structure, each null reference will be turned into a normal object. If you like, you can add a *isNull* flag to each object and detect in the `OnAfterDeserialize` event handler these objects and transform them back to null references. Oh, of course you can do the same thing with all the other duplicated objects... But is that the way you like to use a serialization system, fixing all the errors provied by the system afterwards? For me, the answer is NO, absolutely!

## And now: Using Unity classes as base for data objects?

No, because these classes also have a lot of drawbacks. For me, the two most annoying ones are (a) every `ScriptableObject` has to be stored in an asset, to be usable as a reference in other `ScriptableObjects` and (b) never ever move your data structure to a new place in your project (or move it into another project) otherwise all references may be lost, because the internally used reference id is stored in these *meta* files, and these files will be recreated when these assets are moved. 

But what is the problem with the first drawback? Why can it be a drawback? Isn't it nice to have all these information in some files? If you have to managed some coins or weapons, everything is fine, if you have to manage a graph with some ten thousands of nodes and leaves, you will have ten thousands of small files in your project folder, each have to be loaded and stored back each time the system wants to serialize or deserialize the data structure. If your data structure is big enough you wont be able to work with Unity any more, just look how Unity manages himself...

------

If you just use the serialization feature to store unchangeable data, this behavior may be okay for you. If you are working with changeable, heavy linked data structures, this behavior blows up the file size of your assets and--more important--breaks links between objects





If you can read the contents of the asset file of your `ScriptableObject`, you have to set the *Asset Serialization Mode* in the *Project Settings*.

