public enum ClassType
{
    Melee,
    // Laser Beam
    RangedBeam,
    // Shotgun & Plasma
    RangedProjectile,
    RangedMortar,
    // Enemy Specific
    SuicideBomber,
    Shotgun
}

public static class Global
{
    public static bool GameOver = false;
    public static Controller Controller;

    public static PoolManager ProjectilePool;
    public static UnitSpawner UnitSpawner;

    public static float FriendlyDamageMultiplier = 1;
    public static float EnemyHPMultiplier = 1;
    public static float TimeGameStarted;
}