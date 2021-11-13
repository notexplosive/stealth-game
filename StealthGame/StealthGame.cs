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
        public StealthGame(string[] args) : base("Stealth Game", args, new Point(1600, 900), new Point(1920, 1080),
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
                        Print(pos);
                    }
                };
            }

            var levelSequencer = new LevelSequencer(gameScene);

            levelSequencer.AddLevel("Intro level", new PlayerPathBuilder(
                    new Vector2(200, 600))
                .AddStraightLine(new Vector2(800, 600))
                .AddWinPoint(), (level) =>
            {
                gameScene.CreateBlinkingEnemy(new TransformState(new Vector2(850, 450), MathF.PI / 2),
                    new Blink.Sequence()
                        .AddOn(5)
                        .AddOff(20)
                );
            });
            
            levelSequencer.AddLevel("Spiral", new PlayerPathBuilder(
                        new Vector2(200,200))
                    .AddStraightLine(new Vector2(1000, 200))
                    .AddStraightLine(new Vector2(1000, 700))
                    .AddStraightLine(new Vector2(300, 700))
                    .AddStraightLine(new Vector2(300, 300))
                ,
                (level) =>
                {
                    gameScene.CreateWall(new Point(300, 250), new Point(350, 300));
                    level.CreateCamera(new Vector2(600,400),0f, MathF.PI * 2, 60);
                }
            );

            levelSequencer.AddLevel("Sandbox",new PlayerPathBuilder(
                    new Vector2(200, 400))
                .AddStraightLine(new Vector2(900, 400))
                .AddStraightLine(new Vector2(900, 800))
                .AddStraightLine(new Vector2(1400, 800))
                .AddStraightLine(new Vector2(1400, 200))
                .AddWinPoint(), (level) =>
            {
                level.CreateBackAndForthPatroller(-MathF.PI / 2 + MathF.PI, new Vector2(500, 700),
                    new Vector2(500, 200));
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
            });
        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }
    }
}