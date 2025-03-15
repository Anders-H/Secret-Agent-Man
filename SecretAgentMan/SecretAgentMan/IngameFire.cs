using Microsoft.Xna.Framework.Graphics;

namespace SecretAgentMan;

public class IngameFire
{
    public FireList PlayerFire { get;}
    public FireList EnemyFire { get; }

    public IngameFire()
    {
        PlayerFire = new FireList();
        EnemyFire = new FireList();
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

    public void RemoveOneDeadFire()
    {
        foreach (var fire in PlayerFire)
        {
            if (!fire.IsDead)
                continue;

            PlayerFire.Remove(fire);
            return;
        }

        foreach (var fire in EnemyFire)
        {
            if (!fire.IsDead)
                continue;

            EnemyFire.Remove(fire);
            return;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        PlayerFire.Draw(spriteBatch);
        EnemyFire.Draw(spriteBatch);
    }
}