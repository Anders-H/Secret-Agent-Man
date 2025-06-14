using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes;

public class CutScene1 : RetroGame.Scene.IngameScene
{
    private bool _fireShot = false;
    private bool _enemyDied = false;
    private GameEventPointer _playerHasStopped = new();
    private bool _playerHasJumped = false;
    private bool _pressFireShown = false;
    private readonly Player _player;
    private readonly Npc _enemy;
    private readonly IngameFire _fire = new();
    private ulong _fakeTicks;

    public CutScene1(RetroGame.RetroGame parent) : base(parent)
    {
        MediaPlayer.Stop();
        _player = new Player(_fire.PlayerFire);
        _enemy = new Npc(Npc.StatusCutScene, _player, _fire.EnemyFire, 1);
        _player.X = -25;
        _player.Y = 180;
        _player.TweakPlayerSpeed(1);
        _enemy.X = 640;
        _enemy.Y = 180;
    }

    public override void BeginScene()
    {
        MediaPlayer.Play(Songs.CutSceneSong!);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        if (!_playerHasStopped.Occured)
        {
            if (ticks % 2 == 0)
            {
                _fakeTicks++;
                _player.Tick(_fakeTicks);
                _player.MoveRight();
            }

            if (_enemyDied)
                _enemy.Act(ticks);
            else
                _enemy.ActCutScene(ticks);

            if (_player.X > 130 && !_fireShot)
            {
                _player.CutSceneFire();
                _fireShot = true;
            }
        }

        if (_fireShot && !_enemyDied)
        {
            foreach (var f in _fire.PlayerFire)
            {
                if (_enemy.Hit(f))
                {
                    _fire.PlayerFire.Clear();
                    _enemy.Die(ticks);
                    _enemyDied = true;
                    break;
                }
            }
        }

        if (_player.X > 310 && !_playerHasStopped.Occured)
        {
            _playerHasStopped.Occure(ticks);
        }

        _fire.Act(ticks);
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        _player.Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, _player.CellIndex);

        if (_enemy.AliveStatus is Character.StatusAlive or Character.StatusDying)
            _enemy.Draw(spriteBatch);

        _fire.Draw(spriteBatch);
        Game1.CutSceneFrame!.Draw(spriteBatch, 0, 0, 0);
        base.Draw(gameTime, ticks, spriteBatch);
    }
}