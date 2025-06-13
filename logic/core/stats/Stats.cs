using System.Collections.Generic;

namespace MPAutoChess.logic.core.stats;

public class Stats {
    
    private Dictionary<StatType, Calculation> values = new Dictionary<StatType, Calculation>();
    
    public Calculation GetCalculation(StatType statType) {
        if (!values.ContainsKey(statType)) {
            values[statType] = new Calculation(0);
        }
        return values.GetValueOrDefault(statType);
    }
    
    public float GetValue(StatType statType) {
        return GetCalculation(statType).Evaluate();
    }
    
}