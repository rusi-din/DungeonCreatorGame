using UnityEngine;
using UnityEngine.UI;

public class SetStatsToExplorer : MonoBehaviour
{
    [Header("Player Stats")]
    public Slider hpSlider;
    public TMPro.TMP_InputField hpText;
    public Slider manaSlider;
    public TMPro.TMP_InputField manaText;
    public Slider staminaSlider;
    public TMPro.TMP_InputField staminaText;
    public Slider attackSlider;
    public TMPro.TMP_InputField attackText;

    // Item stats
    [Header("Item Stats")]
    public Slider itemAttackSlider;
    public TMPro.TMP_InputField itemAttackText;
    public Slider itemDefenseSlider;
    public TMPro.TMP_InputField itemDefenseText;

    // playerstats are must be 255 when combined without exceeding 255.For example, if the player has 100 HP, 100 Mana, and 55 Stamina, the total is 255. If the player has 200 HP, 30 Mana, and 25 Stamina, the total is also 255. The same applies to attack stats.
    // Item stats should also follow the same rule, where the combined total of item attack and item defense should not exceed 255.

    public void SetPlayerStats(int hp, int mana, int stamina, int attack, int itemAttack, int itemDefense)
    {
        if (hp + mana + stamina + attack > 255)
        {
            Debug.LogError("Combined player stats exceed 255!");
            return;
        }

        hpSlider.value = hp;
        hpText.text = hp.ToString();

        manaSlider.value = mana;
        manaText.text = mana.ToString();

        staminaSlider.value = stamina;
        staminaText.text = stamina.ToString();

        attackSlider.value = attack;
        attackText.text = attack.ToString();

        if (itemAttack + itemDefense > 255)
        {
            Debug.LogError("Combined item stats exceed 255!");
            return;
        }

        itemAttackSlider.value = itemAttack;
        itemAttackText.text = itemAttack.ToString();

        itemDefenseSlider.value = itemDefense;
        itemDefenseText.text = itemDefense.ToString();
    }

    void Start()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        SetPlayerStats((int)stats.health, (int)stats.mana, (int)stats.stamina, (int)stats.attack, (int)stats.itemDamage, (int)stats.itemDefense);
    }

    public void UpdatePlayerHp()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        if (hpSlider.value != stats.health)
        {
            if (((float)hpSlider.value) + stats.mana + stats.stamina + stats.attack > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                hpSlider.value = stats.health;
            }
            else
            {
                stats.health = hpSlider.value;
                hpText.text = hpSlider.value.ToString();
            }
        }

        if (hpText.text != stats.health.ToString())
        {
            if (float.Parse(hpText.text) + stats.mana + stats.stamina + stats.attack > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                hpSlider.value = stats.health;
            }
            else
            {
                stats.health = hpSlider.value;
                hpText.text = hpSlider.value.ToString();
            }
        }
    }

    public void UpdatePlayerMana()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        if (manaSlider.value != stats.mana)
        {
            if (stats.health + ((float)manaSlider.value) + stats.stamina + stats.attack > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                manaSlider.value = stats.mana;
            }
            else
            {
                stats.mana = manaSlider.value;
                manaText.text = manaSlider.value.ToString();
            }
        }

        if (manaText.text != stats.mana.ToString())
        {
            if (stats.health + float.Parse(manaText.text) + stats.stamina + stats.attack > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                manaSlider.value = stats.mana;
            }
            else
            {
                stats.mana = manaSlider.value;
                manaText.text = manaSlider.value.ToString();
            }
        }
    }

    public void UpdatePlayerStamina()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        if (staminaSlider.value != stats.stamina)
        {
            if (stats.health + stats.mana + ((float)staminaSlider.value) + stats.attack > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                staminaSlider.value = stats.stamina;
            }
            else
            {
                stats.stamina = staminaSlider.value;
                staminaText.text = staminaSlider.value.ToString();
            }
        }

        if (staminaText.text != stats.stamina.ToString())
        {
            if (stats.health + stats.mana + float.Parse(staminaText.text) + stats.attack > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                staminaSlider.value = stats.stamina;
            }
            else
            {
                stats.stamina = staminaSlider.value;
                staminaText.text = staminaSlider.value.ToString();
            }
        }
    }

    public void UpdatePlayerAttack()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        if (attackSlider.value != stats.attack)
        {
            if (stats.health + stats.mana + stats.stamina + ((float)attackSlider.value) > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                attackSlider.value = stats.attack;
            }
            else
            {
                stats.attack = attackSlider.value;
                attackText.text = attackSlider.value.ToString();
            }
        }

        if (attackText.text != stats.attack.ToString())
        {
            if (stats.health + stats.mana + stats.stamina + float.Parse(attackText.text) > 255)
            {
                Debug.LogError("Combined player stats exceed 255!");
                attackSlider.value = stats.attack;
            }
            else
            {
                stats.attack = attackSlider.value;
                attackText.text = attackSlider.value.ToString();
            }
        }
    }

    public void UpdateItemAttack()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        if (itemAttackSlider.value != stats.itemDamage)
        {
            if (((float)itemAttackSlider.value) + stats.itemDefense > 255)
            {
                Debug.LogError("Combined item stats exceed 255!");
                itemAttackSlider.value = stats.itemDamage;
            }
            else
            {
                stats.itemDamage = itemAttackSlider.value;
                itemAttackText.text = itemAttackSlider.value.ToString();
            }
        }

        if (itemAttackText.text != stats.itemDamage.ToString())
        {
            if (float.Parse(itemAttackText.text) + stats.itemDefense > 255)
            {
                Debug.LogError("Combined item stats exceed 255!");
                itemAttackSlider.value = stats.itemDamage;
            }
            else
            {
                stats.itemDamage = itemAttackSlider.value;
                itemAttackText.text = itemAttackSlider.value.ToString();
            }
        }
    }

    public void UpdateItemDefense()
    {
        ExplorerStats stats = ExplorerStats.getInstance();
        if (itemDefenseSlider.value != stats.itemDefense)
        {
            if (stats.itemDamage + ((float)itemDefenseSlider.value) > 255)
            {
                Debug.LogError("Combined item stats exceed 255!");
                itemDefenseSlider.value = stats.itemDefense;
            }
            else
            {
                stats.itemDefense = itemDefenseSlider.value;
                itemDefenseText.text = itemDefenseSlider.value.ToString();
            }
        }

        if (itemDefenseText.text != stats.itemDefense.ToString())
        {
            if (stats.itemDamage + float.Parse(itemDefenseText.text) > 255)
            {
                Debug.LogError("Combined item stats exceed 255!");
                itemDefenseSlider.value = stats.itemDefense;
            }
            else
            {
                stats.itemDefense = itemDefenseSlider.value;
                itemDefenseText.text = itemDefenseSlider.value.ToString();
            }
        }
    }
}
