using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan.Sprites;

public class IngameFire
{
    public FireList PlayerFire { get; }
    public FireList EnemyFire { get; }

    public IngameFire()
    {
        PlayerFire = [];
        EnemyFire = [];
    }

    public void Clear()
    {
        PlayerFire.Clear();
        EnemyFire.Clear();
    }

    public void Act(ulong ticks)
    {
        foreach (var fire in PlayerFire)
            fire.Act(ticks);

        foreach (var fire in EnemyFire)
            fire.Act(ticks);
    }

    public void RemoveDeadFire()
    {
        PlayerFire.RemoveAll(static fire => fire.IsDead);
        EnemyFire.RemoveAll(static fire => fire.IsDead);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        PlayerFire.Draw(spriteBatch);
        EnemyFire.Draw(spriteBatch);
    }
}