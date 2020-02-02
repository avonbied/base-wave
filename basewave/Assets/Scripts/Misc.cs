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
}