using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList : MonoBehaviour {
	
	public Copymon[] copyDex;


    public Move[] moveDex = new Move[] 
    {
        new Move{},
        new Move{moveID=001, moveName="Claw", type = MonType.normal, basePower = 50, accuracy = 90, physSpec = PhysSpec.physical},
        new Move{moveID=002, moveName="Leaf Blast", type= MonType.grass, basePower = 40, accuracy = 100, physSpec = PhysSpec.special},
        new Move{moveID=003, moveName="Water Splash", type = MonType.water, basePower = 40, accuracy = 100, physSpec = PhysSpec.special},
        new Move{moveID=004, moveName="Spicy Slash", type = MonType.fire, basePower = 40, accuracy = 100, physSpec = PhysSpec.physical, causesStatus = StatusEffect.burned, statusChance = 10}
    };


    private void Start()
    {

    }

    Move[] GetMoveDex()
    {
        return moveDex;
    }

    


}
