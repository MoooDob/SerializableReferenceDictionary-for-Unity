// Copyright 2020 Marc Rüdel. All Rights Reserved.
// based on previous work of (https://github.com/azixMcAze/Unity-SerializableDictionary)


using UnityEditor;
using System.Text;
using SerializableReferenceDictionary;

public static class DebugUtilsEditor
{
	public static string ToString(SerializedProperty property)
	{
		StringBuilder sb = new StringBuilder();
		var iterator = property.Copy();
		var end = property.GetEndProperty();
		do
		{
			sb.AppendLine(iterator.propertyPath + " (" + iterator.type + " " + iterator.propertyType + ") = "
				+ SerializableReferenceDictionaryPropertyDrawer.GetPropertyValue(iterator)
				#if UNITY_5_6_OR_NEWER
				+ (iterator.isArray ? " (" + iterator.arrayElementType + ")" : "")
				#endif
				);
		} while(iterator.Next(true) && iterator.propertyPath != end.propertyPath);
		return sb.ToString();
	}
}
