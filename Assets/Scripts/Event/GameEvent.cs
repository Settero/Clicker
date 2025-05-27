using UnityEngine;

public enum GameEventBonusType
{
    None,       // Отсутствие бонуса (для Bonus2 в ивенте без выбора)
    Knowledge,  // Знания
    Money       // Деньги
}

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    public string eventName;        // Имя ивента
    [TextArea]
    public string description;      // Описание ивента

    public bool hasChoice;          // true — если есть выбор, false — если нет

    public GameEventBonusType Bonus1Type;    // Первый бонус (или единственный, если без выбора)
    public int Bonus1Value;

    public GameEventBonusType Bonus2Type;    // Второй бонус (None, если не используется)
    public int Bonus2Value;

    // Геттер для бонуса без выбора
    public string GetSingleBonusSummary()
    {
        return FormatBonus(Bonus1Type, Bonus1Value);
    }

    // Геттеры для бонусов с выбором
    public string GetChoice1Summary()
    {
        return FormatBonus(Bonus1Type, Bonus1Value);
    }

    public string GetChoice2Summary()
    {
        return FormatBonus(Bonus2Type, Bonus2Value);
    }

    // Форматирование бонуса
    private string FormatBonus(GameEventBonusType type, int value)
    {
        if (type == GameEventBonusType.None)
            return "Нет бонуса";

        string sign = value > 0 ? "+" : "";
        string label = type == GameEventBonusType.Knowledge ? "Знания" : "Деньги";
        return $"{sign}{value} {label}";
    }
}
