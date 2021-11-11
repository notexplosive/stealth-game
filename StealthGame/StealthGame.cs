using System;
using Machina.Engine;
using Machina.Engine.AssetLibrary;
using Microsoft.Xna.Framework;
using StealthGame.Data;
using StealthGame.Data.Enemy;

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
            var gameScene = new GameScene(SceneLayers.AddNewScene());

            var playerPathBuilder = new PathBuilder(new Vector2(200, 200))
                    .AddStraightLine(new Vector2(900, 400))
                    .AddStraightLine(new Vector2(900, 800))
                    .AddWaitPoint(20)
                    .AddStraightLine(new Vector2(1400, 200))
                ;

            var walkingPath = playerPathBuilder.Build();

            var player = gameScene.CreatePlayer(walkingPath);

            gameScene.CreateWall(new Rectangle(600, 400, 100, 100));
            gameScene.CreateWall(new Rectangle(700, 500, 100, 100));
            gameScene.CreateWall(new Rectangle(800, 600, 100, 100));
            
            gameScene.CreateBlinkingEnemy(new Vector2(850, 450), MathF.PI / 2, player,
                new Blink.Sequence()
                    .AddOn(20)
                    .AddOff(5)
            );

            gameScene.CreateMovingEnemy(player,
                new TransformBeatAnimation(new TransformState(new Vector2(1200, 100), 0))
                    .LookTo(MathF.PI, 20)
                    .MoveTo(new Vector2(800, 100))
                    .WaitFor(5)
                    .LookTo(MathF.PI * 2, 20)
                    .ForceSetAngle(0f) // this should happen automatically
                    .MoveTo(new Vector2(1200, 100))
            );

            gameScene.CreatePath(walkingPath);
        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }
    }
}