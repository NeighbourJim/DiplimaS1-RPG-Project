using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataHolder : MonoBehaviour {

    public static BattleType BattleType { get; set; }

    #region Trainer Battle
    public static Trainer enemyTrainer;
    public static MonData[] enemyTeam;

    #endregion

    #region Wild Battle
    public static MonData enemyMonster;

    #endregion

    Monpedia mp;

    private void Awake()
    {
        mp = GetComponent<Monpedia>();
    }

    public void SetWildData(int id, int level, bool fleeable)
    {
        enemyTrainer = null;
        if (fleeable)
        {
            BattleType = BattleType.WildFleeable;
        }
        else
        {
            BattleType = BattleType.WildUnfleeable;
        }

        enemyMonster = ScriptableObject.CreateInstance<MonData>();
        enemyMonster.CreateFromBase(mp.FindByID(id));
        enemyMonster.SetLevel(level);
        enemyMonster.GenerateMoveset();
    }

    public void SetTrainerData(Trainer t)
    {
        Debug.Log("Setting Battle Info for Trainer " + t.trainerType.ToString() + " " + t.TrainerName);
        enemyTrainer = t;

        enemyTeam = new MonData[enemyTrainer.trainerTeam.Length];

        for (int i = 0; i < enemyTeam.Length; i++)
        {
            enemyTeam[i] = ScriptableObject.CreateInstance<MonData>();
            enemyTeam[i].CreateFromBase(mp.FindByID(t.trainerTeam[i].BaseMonster.monID));
            enemyTeam[i].SetLevel(t.trainerTeam[i].MonsterLevel);
            enemyTeam[i].GenerateMoveset();
            enemyTeam[i].GenerateWildStats(enemyTeam[i].level);

            Debug.Log(string.Format("Monster {0} set to Level {1} {2}", i + 1, enemyTeam[i].level, enemyTeam[i].monName));
        }
    }
}
