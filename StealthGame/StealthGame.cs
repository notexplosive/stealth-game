using System;
using Machina.Components;
using Machina.Data;
using Machina.Engine;
using Machina.Engine.AssetLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
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
            SceneLayers.BackgroundColor = Color.Black;
            var gameScene = new GameScene(SceneLayers);
            gameScene.SwitchTo(null);

            if (DebugLevel >= DebugLevel.Passive)
            {
                new AdHoc(SceneLayers.AddNewScene().AddActor("debug")).onMouseButton += (key, pos, state) =>
                {
                    if (key == MouseButton.Left && state == ButtonState.Pressed)
                    {
                        MachinaGame.Print(pos);
                    }
                };
            }

            var playerPathBuilder = new PlayerPathBuilder(
                        new Vector2(200, 400))
                    .AddStraightLine(new Vector2(900, 400))
                    .AddStraightLine(new Vector2(900, 800))
                    .AddStraightLine(new Vector2(1400, 800))
                    .AddStraightLine(new Vector2(1400, 200))
                    .AddWinPoint()
                ;

            var level = new Level(gameScene, playerPathBuilder);
            level.CreateBackAndForthPatroller(-MathF.PI / 2 + MathF.PI, new Vector2(500, 700), new Vector2(500, 200));
            // level.CreateBackAndForthPatroller(MathF.PI, new Vector2(700, 700), new Vector2(500, 700));

            level.CreateCamera(new Vector2(1500, 600), MathF.PI / 2, -MathF.PI / 2);
            
            gameScene.CreateWall(new Point(100, 300), new Point(350, 350));
            gameScene.CreateWall(new Point(100, 450), new Point(350, 500));
            
            gameScene.CreateWall(new Point(950, 400), new Point(1350, 750));
            gameScene.CreateBlinkingEnemy(new TransformState(new Vector2(850, 450), MathF.PI / 2),
                new Blink.Sequence()
                    .AddOn(5)
                    .AddOff(20)
            );
        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }
    }
}