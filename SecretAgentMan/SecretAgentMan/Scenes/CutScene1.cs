using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using RetroGame.Scene;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes;

public class CutScene1 : RetroGame.Scene.IngameScene
{
    private readonly Scene _nextLevel;
    private bool _fireShot;
    private bool _enemyDied;
    private GameEventPointer _playerHasStopped = new();
    private GameEventPointer _playerHasJumped = new();
    private bool _pressFireShown;
    private readonly Player _player;
    private readonly Npc _enemy;
    private readonly IngameFire _fire = new();
    private ulong _fakeTicks;
    private readonly List<int> _jumpChangesY;
    private int _jumpChangesIndex;
    private const string PressFireToContinue = "press fire to continue";
    private readonly int _pressFireToContinueX;
    private const int PressFireToContinueY = 300;
    private readonly List<Npc> _huntingZombies = [];

    public CutScene1(RetroGame.RetroGame parent, Scene nextLevel) : base(parent)
    {
        MediaPlayer.Stop();
        _nextLevel = nextLevel;
        _player = new Player(_fire.PlayerFire);
        _enemy = new Npc(Npc.StatusCutScene, _player, _fire.EnemyFire, 1);
        _player.X = -25;
        _player.Y = 180;
        _player.TweakPlayerSpeed(1);
        _enemy.X = 640;
        _enemy.Y = 180;
        _jumpChangesY = [-5, -4, -3, -2, -1, -1, 0, 1, 1, 2, 3, 4, 5];
        _pressFireToContinueX = 320 - PressFireToContinue.Length * 4;
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
                _player.MoveRightForce();
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
            if (_fire.PlayerFire.Any(f => _enemy.Hit(f)))
            {
                _fire.PlayerFire.Clear();
                _enemy.Die(ticks, true);
                _enemyDied = true;
            }
        }
        
        if (_player.X > 285 && !_playerHasStopped.Occured)
            _playerHasStopped.Occure(ticks);
        
        if (_playerHasStopped.OccuredTicksAgo(ticks, 50) && !_playerHasJumped.Occured)
        {
            if (_jumpChangesIndex < _jumpChangesY.Count)
            {
                _player.Y += _jumpChangesY[_jumpChangesIndex];
                _jumpChangesIndex++;
            }
            else
            {
                _playerHasJumped.Occure(ticks);
            }
        }

        if (_playerHasJumped.OccuredTicksAgo(ticks, 25))
        {
            _player.Tick(ticks);
            _player.MoveLeftForce();

            if (ticks % 3 == 0)
            {
                var zombie = new Npc(Npc.StatusCutScene, null, new FireList(), (ulong)Game1.Random.Next(1, 7))
                {
                    X = 631 + Game1.Random.Next(0, 50),
                    Y = 88 + Game1.Random.Next(0, 183)
                };

                _huntingZombies.Add(zombie);
            }

            _huntingZombies.ForEach(z => z.ActCutScene(ticks));
        }

        if (_playerHasJumped.OccuredTicksAgo(ticks, 120))
        {
            _pressFireShown = true;
        }

        if (_playerHasJumped.OccuredTicksAgo(ticks, 600) || (_pressFireShown && Keyboard.IsFirePressed()))
        {
            Parent.CurrentScene = _nextLevel;
        }

        _fire.Act(ticks);
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        _player.Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, _player.CellIndex);

        if (_enemy.AliveStatus is Character.StatusAlive or Character.StatusDying)
            _enemy.Draw(spriteBatch);

        foreach (var huntingZombie in _huntingZombies.OrderBy(x => x.Y))
            huntingZombie.Draw(spriteBatch);

        _fire.Draw(spriteBatch);
        Game1.CutSceneFrame!.Draw(spriteBatch, 0, 0, 0);

        if (_pressFireShown && ticks % 80 < 40)
            Text.DirectDraw(spriteBatch, _pressFireToContinueX, PressFireToContinueY, PressFireToContinue, ColorPalette.Blue);
        
        base.Draw(gameTime, ticks, spriteBatch);
    }
}