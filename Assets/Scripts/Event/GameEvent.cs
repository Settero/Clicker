using UnityEngine;

public enum GameEventBonusType
{
    Knowledge,  // Знания
    Money       // Деньги
}

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    public string eventName;                    // Имя ивента
    [TextArea]
    public string description;                  // Описание ивента

    public bool hasChoice;                       // true — если есть выбор, false — если нет

    // Если ивент без выбора
    public GameEventBonusType bonusType;
    public int bonusValue;

    // Если ивент с выбором
    public GameEventBonusType choiceBonus1Type;
    public int choiceBonus1Value;

    public GameEventBonusType choiceBonus2Type;
    public int choiceBonus2Value;

    // Геттер для вывода бонуса без выбора
    public string GetSingleBonusSummary()
    {
        return FormatBonus(bonusType, bonusValue);
    }

    // Геттеры для вывода бонусов с выбором
    public string GetChoice1Summary()
    {
        return FormatBonus(choiceBonus1Type, choiceBonus1Value);
    }

    public string GetChoice2Summary()
    {
        return FormatBonus(choiceBonus2Type, choiceBonus2Value);
    }

    // Форматирование бонуса
    private string FormatBonus(GameEventBonusType type, int value)
    {
        string sign = value > 0 ? "+" : "";
        string label = type == GameEventBonusType.Knowledge ? "Знания" : "Деньги";
        return $"{sign}{value} {label}";
    }
}
