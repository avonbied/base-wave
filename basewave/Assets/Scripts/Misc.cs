public enum ClassType
{
    Melee,
    // Laser Beam
    Ranged,
    // Shotgun & Plasma
    RangedProjectile,
    RangedMortar,
    // Enemy Specific
    SuicideBomber
}
public static class Global
{
    public static bool GameOver = false;
    public static Controller Controller;
}