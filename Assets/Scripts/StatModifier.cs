using System.Collections.Generic;

public class StatModifier
{
    public readonly float value;
    public readonly float percent;
    public readonly object source;

    public StatModifier(float new_value, float new_percent, object new_source=null)
    {
        value = new_value;
        percent = new_percent;
        source = new_source;
    }

    public List<float> AddEffect(float statValue, float statPercent)
    {
        List<float> updatedStat = new List<float>();
        updatedStat.Add(statValue + value);
        updatedStat.Add(statPercent + percent);
        return updatedStat;
    }
}
