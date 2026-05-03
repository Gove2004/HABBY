
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBarFill;
    [SerializeField] private TextMeshProUGUI hpText;
    private Character character;

    public void SetCharacter(Character character)
    {
        if (this.character != null)
        {
            this.character.OnHPChanged -= UpdateHPBar;
            this.character.OnLevelUp -= UpdateHPBar;
        }
        this.character = character;
        this.character.OnHPChanged += UpdateHPBar;
        this.character.OnLevelUp += UpdateHPBar;
        UpdateHPBar(0);
    }

    public void UpdateHPBar(int v)
    {
        if (character.MaxHP > 0)
        {
            hpBarFill.fillAmount = (float)character.CurrentHP / character.MaxHP;
            hpText.text = $"Lv{character.Level} {character.CurrentHP}/{character.MaxHP}";
        }
        else
        {
            hpBarFill.fillAmount = 0f;
            hpText.text = $"Lv.{character.Level}    {character.CurrentHP}/{character.MaxHP}";
        }
    }
}
