using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataHolder : MonoBehaviour {

    public static BattleType BattleType { get; set; }

    #region Trainer Battle
    public static Trainer EnemyTrainer = new Trainer();
    public static MonData[] EnemyTeam { get; set; }

    #endregion

    #region Wild Battle
    public static MonData EnemyMonster { get; set; }

    #endregion

    Monpedia mp;

    private void Awake()
    {
        mp = GetComponent<Monpedia>();
    }

    public void SetWildData(int id, int level, bool fleeable)
    {
        EnemyTrainer = null;
        if (fleeable)
        {
            BattleType = BattleType.WildFleeable;
        }
        else
        {
            BattleType = BattleType.WildUnfleeable;
        }

        EnemyMonster = ScriptableObject.CreateInstance<MonData>();
        EnemyMonster.CreateFromBase(mp.FindByID(id));
        EnemyMonster.SetLevel(level);
        EnemyMonster.GenerateMoveset();
    }

    public void SetTrainerData(Trainer t)
    {
        EnemyMonster = null;

        BattleType = BattleType.Trainer;
        EnemyTrainer.CreateFromBase(t);
        EnemyTeam = new MonData[EnemyTrainer.trainerTeam.Length];

        for (int i = 0; i < EnemyTeam.Length; i++)
        {
            EnemyTeam[i] = ScriptableObject.CreateInstance<MonData>();
            EnemyTeam[i].CreateFromBase(mp.FindByID(t.trainerTeam[i].BaseMonster.monID));
            EnemyTeam[i].SetLevel(t.trainerTeam[i].MonsterLevel);
            EnemyTeam[i].GenerateMoveset();
            EnemyTeam[i].GenerateWildStats(EnemyTeam[i].level);
            EnemyTeam[i].ownership = Ownership.trainer;

            Debug.Log(string.Format("Monster {0} set to Level {1} {2}", i, EnemyTeam[i].level, EnemyTeam[i].monName));
        }
    }
}
