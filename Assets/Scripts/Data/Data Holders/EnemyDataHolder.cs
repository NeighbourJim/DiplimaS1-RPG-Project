using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataHolder : MonoBehaviour {

    public static MonData enemyMonster;

    Monpedia mp;

    private void Awake()
    {
        mp = GetComponent<Monpedia>();
    }

    public void SetData(int id, int level)
    {
        enemyMonster = ScriptableObject.CreateInstance<MonData>();
        enemyMonster.CreateFromBase(mp.FindByID(id));
        enemyMonster.SetLevel(level);
        enemyMonster.GenerateMoveset();
    }
}
