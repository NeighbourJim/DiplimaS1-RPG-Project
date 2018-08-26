using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHPControl : MonoBehaviour {

    BattleControl battleController;
    BattleUIControl uiController;

    [Header("Panels")]
    public GameObject playerPanel;
    public GameObject enemyPanel;

    [Header("Player UI Elements")]
    public Text playerMonName;
    public Text playerMonLevel;
    public Text playerMonHPCount;
    public GameObject playerHPBar;
    private RectTransform playerHPBarRect;
    public GameObject playerHPDamage;
    private RectTransform playerHPDamageRect;

    [Header("Enemy UI Elements")]
    public Text enemyMonName;
    public Text enemyMonLevel;
    public Text enemyMonHPCount;
    public GameObject enemyHPBar;
    private RectTransform enemyHPBarRect;
    public GameObject enemyHPDamage;
    private RectTransform enemyHPDamageRect;

    float hpSizeMax;
    float hpSize;

    MonBattleData player;
    MonBattleData enemy;

    private void Start()
    {
        uiController = GetComponent<BattleUIControl>();
        battleController = uiController.GetComponent<BattleControl>();

        playerHPBarRect = playerHPBar.GetComponent<RectTransform>();
        playerHPDamageRect = playerHPDamage.GetComponent<RectTransform>();
        enemyHPBarRect = enemyHPBar.GetComponent<RectTransform>();
        enemyHPDamageRect = enemyHPDamage.GetComponent<RectTransform>();

        hpSizeMax = playerHPBarRect.rect.width;
        hpSize = hpSizeMax;
    }

    public void SetMonsters(MonBattleData p, MonBattleData e)
    {
        player = p;
        enemy = e;
        SetPlayerMonster(player);
        SetEnemyMonster(enemy);
    }

    public void UpdateMonsterHP(MonBattleData monster)
    {
        if(monster == player)
        {
            SetHPBarSize(playerHPBarRect, playerHPDamageRect, monster.curHP, monster.maxHP);
            playerMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
        }
        else
        {
            SetHPBarSize(enemyHPBarRect, enemyHPDamageRect, monster.curHP, monster.maxHP);
            enemyMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
        }
    }

    void SetPlayerMonster(MonBattleData monster)
    {
        playerMonName.text = monster.monName;
        playerMonLevel.text = string.Format("Lv: {0}", monster.level);
        playerMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
        SetHPBarSize(playerHPBarRect, playerHPDamageRect, monster.curHP, monster.maxHP);
    }

    void SetEnemyMonster(MonBattleData monster)
    {
        enemyMonName.text = monster.monName;
        enemyMonLevel.text = string.Format("Lv: {0}", monster.level);
        enemyMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
        SetHPBarSize(enemyHPBarRect, enemyHPDamageRect, monster.curHP, monster.maxHP);
    }

    void SetHPBarSize(RectTransform bar, RectTransform dmgBar, float hp, float maxHP)
    {
        float hpPer = GetPer(hp, maxHP);
        float realBarSize = Mathf.Floor(hpSizeMax * hpPer);
        float prevSize = bar.rect.width;

        bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, realBarSize);
        if(bar.rect.width < prevSize)
        {
            StartCoroutine(DamageBarShrink(dmgBar, realBarSize));
        }        
    }

    float GetPer(float numerator, float denominator)
    {
        return numerator / denominator;
    }

    IEnumerator DamageBarShrink(RectTransform bar, float targetSize)
    {
        while(bar.rect.width != targetSize)
        {
            bar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bar.rect.width - 1);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
