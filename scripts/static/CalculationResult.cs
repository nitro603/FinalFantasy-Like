public class CalculationResult {
    
    /*
     *Have the calculator return a small result object (a plain class or struct) containing:

Did it hit? (so the UI can show “Miss”)
Damage amount
Was it a crit?
Was it evaded? (can be the same as “didn’t hit” at first)
     * 
     */
    //public bool hit;
    public int Damage;
    //public bool crit;

    public CalculationResult(int dmg) {
        Damage = dmg;
    }
}