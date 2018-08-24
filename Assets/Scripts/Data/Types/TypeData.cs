using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TypeData : ScriptableObject
{
    public int typeID = 0;
    public string typeName = "?TYPE?";
    public TypeData[] weaknesses;
    public TypeData[] resistances;
    public TypeData[] immunities;

    public Color buttonColour;

    public bool FindInWeaknesses(TypeData toFind)
    {
        if (weaknesses.Length > 0)
        {
            foreach (TypeData t in weaknesses)
            {
                if (t.typeID == toFind.typeID)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool FindInResistances(TypeData toFind)
    {
        if (resistances.Length > 0)
        {
            foreach (TypeData t in resistances)
            {
                if (t.typeID == toFind.typeID)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool FindInImmunities(TypeData toFind)
    {
        if (immunities.Length > 0)
        {
            foreach (TypeData t in immunities)
            {
                if (t.typeID == toFind.typeID)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
