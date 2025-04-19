using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RetroGame;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes;

public class BonusLevelScene : RetroGame.Scene.IngameScene
{
    private readonly DateTime _bonusLevelStartTime;
    private readonly IngameFire _fire = new();
    private readonly Player _player;
    private List<Npc> Npcs { get; }
    private readonly AddScoreDelegate _addScore;
    private int _secondsPassed;
    public CoinList Coins { get; }

    public BonusLevelScene(RetroGame.RetroGame parent, int score, AddScoreDelegate addScore) : base(parent)
    {
        Npcs = [];
        Coins = [];
        _player = new Player(_fire.PlayerFire);
        Score = score;
        _addScore = addScore;

        for (var x = 0; x < 100; x++)
            Npcs.Add(Npc.CreateBonus(x));

        Coins.CreateBonusRoundSquare();
        _bonusLevelStartTime = DateTime.Now;
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        _secondsPassed = (int)Math.Ceiling(DateTime.Now.Subtract(_bonusLevelStartTime).TotalSeconds);

        if (_secondsPassed >= Game1.BonusRoundSeconds)
        {
            Parent.CurrentScene = Game1.CurrentIngameScene;
            return;
        }

        foreach (var npc in Npcs)
            npc.ActBonus(ticks);

        foreach (var coin in Coins)
        {
            coin.Act(ticks);

            if (coin.Collide(_player))
            {
                Game1.PlayerCoin!.PlayRandom();
                Coins.Remove(coin);
                _addScore(50);
                Score += 50;
                break;
            }

            foreach (var npc in Npcs)
            {
                if (coin.Collide(npc))
                {
                    Game1.EnemyCoin!.PlayRandom();
                    Coins.Remove(coin);
                    goto OuterBail;
                }
            }
        }

        OuterBail:;

        _fire.Act(ticks);

        foreach (var npc in Npcs)
        {
            foreach (var fire in _fire.PlayerFire)
            {
                if (!npc.Hit(fire))
                    continue;

                _fire.PlayerFire.Remove(fire);

                if (npc.AliveStatus == Character.StatusAlive)
                {
                    npc.Die(ticks);
                    Score = _addScore(5);
                }

                break;
            }
        }

        foreach (var npc in Npcs.Where(npc => npc.AliveStatus == Character.StatusDead && !npc.IsGraveStone))
        {
            Npcs.Remove(npc);
            break;
        }

        _player.PlayerControlBonusRound(ticks, Keyboard);
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        var playerIsDrawn = false;
        var lastY = IngameScene.SpriteUpperLimit - 1;

        foreach (var t in Npcs.OrderBy(x => x.IntY))
        {
            if (!playerIsDrawn && _player.Y >= lastY && _player.Y <= t.IntY)
            {
                _player.Draw(spriteBatch, Game1.CharactersTexture, _player.CellIndex, Color.White);
                playerIsDrawn = true;
            }

            t.Draw(spriteBatch);
            lastY = t.IntY;
        }

        if (!playerIsDrawn)
            _player.Draw(spriteBatch, Game1.CharactersTexture, _player.CellIndex, Color.White);

        Coins.Draw(spriteBatch);
        _fire.Draw(spriteBatch);
        Game1.Frame!.Draw(spriteBatch, 0, 0, 0);
        Game1.BonusLevelFrame!.Draw(spriteBatch, ticks % 20 < 10 ? 0 : 1, 0, 0);
        Text.DirectDraw(spriteBatch, 508, 12, ScoreString, ColorPalette.White);

        if (ticks % 15 < 7)
            Text.DirectDraw(spriteBatch, 12, 12, (Game1.BonusRoundSeconds - _secondsPassed).ToString(), ColorPalette.White);

        base.Draw(gameTime, ticks, spriteBatch);
    }
}