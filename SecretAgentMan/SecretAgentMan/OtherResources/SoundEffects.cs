using RetroGame.Audio;

namespace SecretAgentMan.OtherResources;

public static class SoundEffects
{
    public static SoundEffect? EnemyFire { get; set; }
    public static SoundEffect? PlayerFire { get; set; }
    public static SoundEffect? EnemyDie { get; set; }
    public static SoundEffect? PlayerDie { get; set; }
    public static SoundEffect? EnemyCoin { get; set; }
    public static SoundEffect? PlayerCoin { get; set; }
    public static SoundEffect? FireNoAmmo { get; set; }

    public static void CreateSoundEffects(RetroGame.RetroGame game)
    {
        EnemyFire = new SoundEffect(game);
        PlayerFire = new SoundEffect(game);
        EnemyDie = new SoundEffect(game);
        PlayerDie = new SoundEffect(game);
        EnemyCoin = new SoundEffect(game);
        PlayerCoin = new SoundEffect(game);
        FireNoAmmo = new SoundEffect(game);
    }

    public static void LoadSoundEffects()
    {
        EnemyFire!.Initialize("sfx_gun1", "sfx_gun2", "sfx_gun3", "sfx_gun4", "sfx_gun5", "sfx_gun6");
        PlayerFire!.Initialize("sfx_gun7", "sfx_gun8", "sfx_gun9", "sfx_gun10");
        EnemyDie!.Initialize("sfx_enemydeath1", "sfx_enemydeath2", "sfx_enemydeath3");
        PlayerDie!.Initialize("sfx_playerdeath");
        EnemyCoin!.Initialize("enemy_sfx_coin_1", "enemy_sfx_coin_2", "enemy_sfx_coin_3");
        PlayerCoin!.Initialize("player_sfx_coin_1", "player_sfx_coin_2");
        FireNoAmmo!.Initialize("sfx_noammo2");
    }
}