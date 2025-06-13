using System.Collections.Generic;

namespace MPAutoChess.logic.core.stats;

public class Calculation {

    private Value baseValue;
    public Value BaseValue { get => BaseValue; set { baseValue = value; Invalidate(); } }
    
    private List<Value> preMults = new List<Value>();
    private List<string> preMultIds = new List<string>();
    
    private List<Value> adds = new List<Value>();
    private List<string> addIds = new List<string>();
    
    private List<Value> postMults = new List<Value>();
    private List<string> postMultIds = new List<string>();
    
    public Calculation(float baseValue) {
        BaseValue = new ConstantValue(baseValue);
    }
    
    public Calculation(Value baseValue) {
        BaseValue = baseValue;
    }
    
    public float Evaluate() {
        float result = baseValue.Get();
        float preMult = 1;
        foreach (Value value in preMults) {
            preMult += value.Get();
        }
        result *= preMult;
        foreach (Value value in adds) {
            result += value.Get();
        }
        float postMult = 1;
        foreach (Value value in postMults) {
            postMult += value.Get();
        }
        result *= postMult;

        return result;
    }
    
    public void AddPreMult(Value value, string id) {
        int index = preMultIds.IndexOf(id);
        if (index >= 0) {
            preMults[index] = value;
        } else {
            preMults.Add(value);
            preMultIds.Add(id);
        }
        Invalidate();
    }
    
    public bool RemovePreMult(string id) {
        int index = preMultIds.IndexOf(id);
        if (index == -1) return false;
        preMults.RemoveAt(index);
        preMultIds.RemoveAt(index);
        Invalidate();
        return true;
    }


    private void Invalidate() {
        
    }
    
}