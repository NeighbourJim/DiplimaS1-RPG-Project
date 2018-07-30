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
    
    Move[] GetMoveDex()
    {
        return moveDex;
    }
}
