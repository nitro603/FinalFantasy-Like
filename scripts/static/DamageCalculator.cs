

public static class DamageCalculator {
	
	public static CalculationResult basicAttack(int damage, int defense) {
		return new CalculationResult(damage - defense);
	}
}
