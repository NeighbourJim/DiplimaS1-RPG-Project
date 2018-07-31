using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList : MonoBehaviour {
	
	public Copymon[] copyDex;
	
	
    public Move[] moveDex = new Move[] 
    {
        new Move{},
        new Move{moveID=001, moveName="Claw", type = Type.normal, basePower = 50, accuracy = 90, physSpec = PhysSpec.physical},
        new Move{moveID=002, moveName="Leaf Blast", type= Type.grass, basePower = 40, accuracy = 100, physSpec = PhysSpec.special},
        new Move{moveID=003, moveName="Bubble", type = Type.water, basePower = 40, accuracy = 100, physSpec = PhysSpec.special},
        new Move{moveID=004, moveName="Spicy Slash", type = Type.fire, basePower = 40, accuracy = 100, physSpec = PhysSpec.physical, causesStatus = StatusEffect.burned, statusChance = 10}
    };

    public Dictionary<Type, Color> typeColours;

    private void Awake()
    {
        PopulateTypeColours();
    }

    Move[] GetMoveDex()
    {
        return moveDex;
    }

    void PopulateTypeColours()
    {
        typeColours.Add(Type.normal, new Color(245, 232, 206));
        typeColours.Add(Type.fire, new Color(255, 163, 64));
        typeColours.Add(Type.grass, new Color(131, 219, 64));
        typeColours.Add(Type.water, new Color(112, 165, 250));
    }

    
}
