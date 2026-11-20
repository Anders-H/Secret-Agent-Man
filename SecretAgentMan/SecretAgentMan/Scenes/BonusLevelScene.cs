using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RetroGame;
using SecretAgentMan.OtherResources;
using SecretAgentMan.Sprites;

namespace SecretAgentMan.Scenes;

public class BonusLevelScene : RetroGame.Scene.IngameScene
{
    private TimeSpan _elapsed;
    private readonly IngameFire _fire = new();
    private readonly Player _player;
    private NpcList Npcs { get; }
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
        MediaPlayer.Stop();
    }

    public override void BeginScene()
    {
        _elapsed = TimeSpan.Zero;
        _secondsPassed = 0;
        if (Songs.BonusLevelSong != null)
            MediaPlayer.Play(Songs.BonusLevelSong);
    }

    public override void Update(GameTime gameTime, ulong ticks)
    {
        _elapsed += gameTime.ElapsedGameTime;
        _secondsPassed = (int)_elapsed.TotalSeconds;

        if (_secondsPassed >= Game1.BonusRoundSeconds)
        {
            Parent.CurrentScene = new SignScene(Parent, "game continues", Game1.CurrentIngameScene!);
            MediaPlayer.Stop();
            return;
        }

        foreach (var npc in Npcs)
            npc.ActBonus(ticks);

        if (Coins.Act(ticks, _player, Npcs))
            Score = _addScore(50);

        _fire.Act(ticks);
        _fire.RemoveDeadFire();

        foreach (var npc in Npcs)
        {
            foreach (var fire in _fire.PlayerFire)
            {
                if (!npc.Hit(fire))
                    continue;

                _fire.PlayerFire.Remove(fire);

                if (npc.AliveStatus == Character.StatusAlive)
                {
                    npc.Die(ticks, true);
                    Score = _addScore(5);
                }

                break;
            }
        }

        Npcs.RemoveAll(static npc => npc.AliveStatus == Character.StatusDead);

        _player.PlayerControlBonusRound(ticks, Keyboard);
        base.Update(gameTime, ticks);
    }

    public override void Draw(GameTime gameTime, ulong ticks, SpriteBatch spriteBatch)
    {
        var playerIsDrawn = false;
        var lastY = IngameScene.SpriteUpperLimit - 1;

        foreach (var t in Npcs.YSorted())
        {
            if (!playerIsDrawn && _player.Y >= lastY && _player.Y <= t.IntYForYSort)
            {
                _player.Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, _player.CellIndex, Color.White);
                playerIsDrawn = true;
            }

            t.Draw(spriteBatch);
            lastY = t.IntY;
        }

        if (!playerIsDrawn)
            _player.Draw(spriteBatch, IngameBackgroundResources.CharactersTexture, _player.CellIndex, Color.White);

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
