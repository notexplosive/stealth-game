using System;
using Machina.Components;
using Machina.Engine;
using Machina.Engine.AssetLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using StealthGame.Components;
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
            var gameScene = SceneLayers.AddNewScene();

            var pathBuilder = new PathBuilder(new Vector2(200, 200))
                .StraightLine(new Vector2(900, 400))
                .StraightLine(new Vector2(900, 800))
                .WaitPoint(20)
                .StraightLine(new Vector2(1400, 200))
                ;
            
            var playerBeatTracker = new BeatTracker();
            var worldBeatTracker = new BeatTracker();
            var walkingPath = pathBuilder.Build();

            var world = gameScene.AddActor("World");
            new WorldBeat(world, worldBeatTracker);
            
            var player = gameScene.AddActor("Player");
            new PlayerInput(player, playerBeatTracker);
            new PlayerMovement(player, playerBeatTracker, walkingPath);
            new CircleRenderer(player, 32, Color.Orange);

            var walls = new Wall[]
            {
                CreateWall(gameScene, new Rectangle(600, 400, 100, 100)),
                CreateWall(gameScene, new Rectangle(700, 500, 100, 100)),
                CreateWall(gameScene, new Rectangle(800, 600, 100, 100)),
            };

            var enemy = gameScene.AddActor("enemy", new Vector2(850,450));
            new LineOfSight(enemy, player.transform, walls);
            new FacingDirection(enemy, MathF.PI / 4);
            var cone = new ConeOfVision(enemy, MathF.PI / 2);
            new EnemyBehavior(enemy, worldBeatTracker, new Blink(
                cone,
                new Blink.Sequence()
                    .AddOn(20)
                    .AddOff(5)
            ));

            var enemyDetections = new EnemyDetection[]
            {
                new EnemyDetection(enemy)
            };

            var path = gameScene.AddActor("Path");
            new PathRenderer(path, walkingPath, enemyDetections);
        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }

        public Wall CreateWall(Scene gameScene, Rectangle rectangle)
        {
            var wallActor = gameScene.AddActor("enemy", rectangle.Location.ToVector2());
            new BoundingRect(wallActor, rectangle.Size);
            new BoundingRectFill(wallActor, Color.Orange);
            return new Wall(wallActor);
        }
    }
}