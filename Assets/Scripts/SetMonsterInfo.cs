using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMonsterInfo : MonoBehaviour {

    public GameObject nameText;
    public GameObject descText;
    public GameObject statText;

    public int monID = 1;

    Monpedia mp;

	// Use this for initialization
	void Start () {
        mp = GameObject.Find("GameDataController").GetComponent<Monpedia>();
        MonData mon = mp.FindByID(monID);

        nameText.GetComponent<Text>().text = string.Format("Name: {0}\nType: {1}", mon.name.Substring(5), mon.primaryType.typeName);
        descText.GetComponent<Text>().text = string.Format("Description:\n{0}", mon.monpediaEntry);
        statText.GetComponent<Text>().text = string.Format("HP: {5}\nAttack: {0}\nDefense: {1}\nSpecial Attack: {2}\nSpecial Defense: {3}\nSpeed: {4}", mon.baseAtk, mon.baseDef, mon.baseSpAtk, mon.baseSpDef, mon.baseSpeed, mon.baseHP);
	}
	
	
}
