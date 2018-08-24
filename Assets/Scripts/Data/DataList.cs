using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList : MonoBehaviour {
	
	public MonData[] monsterpedia;


    public Move[] moveDex = new Move[] 
    {
        new Move{},
        new Move{moveID=001, moveName="Claw", type = TypeNum.normal, basePower = 50, accuracy = 90, physSpec = PhysSpec.physical},
        new Move{moveID=002, moveName="Leaf Blast", type= TypeNum.grass, basePower = 40, accuracy = 100, physSpec = PhysSpec.special},
        new Move{moveID=003, moveName="Water Splash", type = TypeNum.water, basePower = 40, accuracy = 100, physSpec = PhysSpec.special},
        new Move{moveID=004, moveName="Spicy Slash", type = TypeNum.fire, basePower = 40, accuracy = 100, physSpec = PhysSpec.physical, causesStatus = StatusEffect.burned, statusChance = 10}
    };


    private void Start()
    {
        foreach (MonData monster in monsterpedia)
        {
            if (monster != null)
            {
                print(monster.monName);
            }
        }
    }

    Move[] GetMoveDex()
    {
        return moveDex;
    }

    


}
