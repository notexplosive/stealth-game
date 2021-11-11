using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.Engine.AssetLibrary;
using Microsoft.Xna.Framework;
using StealthGame.Components;
using StealthGame.Data;
using StealthGame.Data.Enemy;
using StealthGame.Data.Enemy.Animation;
using StealthGame.Data.PlayerPath;

namespace StealthGame
{
    public class StealthGame : MachinaGame
    {
        public StealthGame(string[] args) : base("Stealth Game", args, new Point(1920, 1080), new Point(1920, 1080),
            ResizeBehavior.MaintainDesiredResolution)
        {
        }

        protected override void OnGameLoad()
        {
            var gameScene = new GameScene(SceneLayers);
            gameScene.SwitchTo(null);

            var playerPathBuilder = new PlayerPathBuilder(new Vector2(200, 200))
                    .AddStraightLine(new Vector2(900, 400))
                    .AddStraightLine(new Vector2(900, 800))
                    .AddWaitPoint(5)
                    .AddStraightLine(new Vector2(1400, 200))
                    .AddWinPoint()
                ;

            var level = new Level(gameScene, playerPathBuilder);
            level.CreateBackAndForthPatroller(-MathF.PI / 2 + MathF.PI, new Vector2(500, 500), new Vector2(500, 300));
            
            level.CreateBackAndForthPatroller(MathF.PI, new Vector2(700, 700), new Vector2(500, 700));
            
            gameScene.CreateWall(new Rectangle(800, 600, 400, 50));
            
            gameScene.CreateBlinkingEnemy(new TransformState(new Vector2(850, 450), MathF.PI / 2),
                new Blink.Sequence()
                    .AddOn(20)
                    .AddOff(5)
            );
        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }
    }
}