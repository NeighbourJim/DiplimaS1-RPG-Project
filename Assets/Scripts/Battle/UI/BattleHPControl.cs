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
    public Text playerStatus;
    public GameObject playerHPBar;
    private RectTransform playerHPBarRect;
    public GameObject playerHPDamage;
    private RectTransform playerHPDamageRect;

    [Header("Enemy UI Elements")]
    public Text enemyMonName;
    public Text enemyMonLevel;
    public Text enemyMonHPCount;
    public Text enemyStatus;
    public GameObject enemyHPBar;
    private RectTransform enemyHPBarRect;
    public GameObject enemyHPDamage;
    private RectTransform enemyHPDamageRect;

    float hpSizeMax;
    float hpSize;

    MonBattleData player;
    MonBattleData enemy;

    public Color healthy;
    public Color damaged;

    Coroutine flashBarP;
    Coroutine flashBarE;

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
        flashBarP = StartCoroutine(LowHPFlash(playerHPBarRect, player));
        flashBarE = StartCoroutine(LowHPFlash(enemyHPBarRect, enemy));
    }

    public void UpdateMonsterHP(MonBattleData monster)
    {
        if(monster == player)
        {
            SetHPBarSize(playerHPBarRect, playerHPDamageRect, monster.curHP, monster.maxHP);
            playerMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
            if (GetPer(monster.curHP, monster.maxHP) < 0.30)
            {
                playerHPBarRect.GetComponent<Image>().color = damaged;
            }
            else
            {
                playerHPBarRect.GetComponent<Image>().color = healthy;
            }
        }
        else
        {
            SetHPBarSize(enemyHPBarRect, enemyHPDamageRect, monster.curHP, monster.maxHP);
            enemyMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
            if (GetPer(monster.curHP, monster.maxHP) < 0.30)
            {
                enemyHPBarRect.GetComponent<Image>().color = damaged;
            }
            else
            {
                enemyHPBarRect.GetComponent<Image>().color = healthy;
            }
        }
        UpdateMonsterStatus(monster);
    }

    public void UpdateMonsterStatus(MonBattleData monster)
    {
        string statusDesc = "";
        if(monster == player)
        {
            switch (monster.hasStatus)
            {
                case (StatusEffect.none):
                    statusDesc = "";
                    break;
                case (StatusEffect.burned):
                    statusDesc = "Burned";
                    break;
                case (StatusEffect.confused):
                    statusDesc = "Confused";
                    break;
                case (StatusEffect.frozen):
                    statusDesc = "Frozen";
                    break;
                case (StatusEffect.paralyzed):
                    statusDesc = "Paralyzed";
                    break;
                case (StatusEffect.poisoned):
                    statusDesc = "Poisoned";
                    break;
                case (StatusEffect.sleep):
                    statusDesc = "Asleep";
                    break;
                default:
                    statusDesc = "";
                    break;
            }
            playerStatus.text = statusDesc;
        }
        else
        {
            switch (monster.hasStatus)
            {
                case (StatusEffect.none):
                    statusDesc = "";
                    break;
                case (StatusEffect.burned):
                    statusDesc = "Burned";
                    break;
                case (StatusEffect.confused):
                    statusDesc = "Confused";
                    break;
                case (StatusEffect.frozen):
                    statusDesc = "Frozen";
                    break;
                case (StatusEffect.paralyzed):
                    statusDesc = "Paralyzed";
                    break;
                case (StatusEffect.poisoned):
                    statusDesc = "Poisoned";
                    break;
                case (StatusEffect.sleep):
                    statusDesc = "Asleep";
                    break;
                default:
                    statusDesc = "";
                    break;
            }
            enemyStatus.text = statusDesc;
        }
    }

    void SetPlayerMonster(MonBattleData monster)
    {
        playerMonName.text = monster.monName;
        playerMonLevel.text = string.Format("Lv: {0}", monster.level);
        playerMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
        SetHPBarSize(playerHPBarRect, playerHPDamageRect, monster.curHP, monster.maxHP);
        UpdateMonsterStatus(monster);
    }

    void SetEnemyMonster(MonBattleData monster)
    {
        enemyMonName.text = monster.monName;
        enemyMonLevel.text = string.Format("Lv: {0}", monster.level);
        enemyMonHPCount.text = string.Format("{0}/{1}", monster.curHP, monster.maxHP);
        SetHPBarSize(enemyHPBarRect, enemyHPDamageRect, monster.curHP, monster.maxHP);
        UpdateMonsterStatus(monster);
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

    IEnumerator LowHPFlash(RectTransform bar, MonBattleData monster)
    {
        Color newColor;
        Color oldColor;
        Image barImage = bar.GetComponent<Image>();
        while (true)
        {
            if (GetPer(monster.curHP, monster.maxHP) <= 0.30f)
            {
                bar.GetComponent<Image>().color = damaged;
            }
            else
            {
                bar.GetComponent<Image>().color = healthy;
            }
            while (GetPer(monster.curHP, monster.maxHP) <= 0.3f)
            {
                oldColor = barImage.color;
                if (oldColor.r < 0.95)
                {
                    newColor = new Color(oldColor.r * 1.5f, (oldColor.g + 0.1f) * 1.5f, (oldColor.b + 0.1f) * 1.5f);
                    barImage.color = newColor;
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    newColor = new Color(oldColor.r / 1.5f, (oldColor.g / 1.5f) - 0.1f, (oldColor.b / 1.5f) - 0.1f);
                    barImage.color = newColor;
                    yield return new WaitForSeconds(0.1f);
                }
            }
            yield return null;
        }
    }
}
