using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
public class TypeData : ScriptableObject
{
    public int typeID = 0;
    public string typeName = "?TYPE?";
    public TypeData[] weaknesses;
    public TypeData[] resistances;
    public TypeData[] immunities;
}
