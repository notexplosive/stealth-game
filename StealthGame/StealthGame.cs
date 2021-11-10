using System;
using System.Collections.Generic;
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
                    .AddStraightLine(new Vector2(900, 400))
                    .AddStraightLine(new Vector2(900, 800))
                    .AddWaitPoint(20)
                    .AddStraightLine(new Vector2(1400, 200))
                ;

            var playerBeatTracker = new BeatTracker(false);
            var worldBeatTracker = new BeatTracker(true);
            var walkingPath = pathBuilder.Build();

            worldBeatTracker.LoopHit += () =>
            {
                MachinaGame.Print("Loop!");
            };

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
                CreateWall(gameScene, new Rectangle(800, 600, 100, 100))
            };

            var enemyDetections = new List<EnemyDetection>();

            CreateBlinkingEnemy(gameScene, new Vector2(850, 450), MathF.PI / 2, player, walls, worldBeatTracker, enemyDetections,
                new Blink.Sequence()
                    .AddOn(20)
                    .AddOff(5)
            );

            var cameraAngle = 0f;
            var cameraPosition = new Vector2(1200, 100);
            var enemyActor = gameScene.AddActor("enemy", cameraPosition);
            new LineOfSight(enemyActor, player.transform, walls);
            new FacingDirection(enemyActor, cameraAngle);
            new ConeOfVision(enemyActor, MathF.PI / 2);
            var ai = new AnimatedEnemy(enemyActor,
                new TransformBeatAnimation(enemyActor.transform)
                    .LookTo(MathF.PI, 20)
                    .WaitFor(5)
                    .LookTo(0, 20));
            worldBeatTracker.RegisterBehavior(ai);
            enemyDetections.Add(new EnemyDetection(enemyActor));

            var path = gameScene.AddActor("Path");
            new PathRenderer(path, walkingPath, enemyDetections);
        }

        private static void CreateBlinkingEnemy(Scene gameScene, Vector2 position, float angle, Actor player, Wall[] walls,
            BeatTracker worldBeatTracker, List<EnemyDetection> enemyDetections, Blink.Sequence blinkSequence)
        {
            var enemyActor = gameScene.AddActor("enemy", position);
            new LineOfSight(enemyActor, player.transform, walls);
            new FacingDirection(enemyActor, angle);
            var cone = new ConeOfVision(enemyActor, MathF.PI / 2);
            worldBeatTracker.RegisterBehavior(new Blink(
                cone,
                blinkSequence
            ));
            enemyDetections.Add(new EnemyDetection(enemyActor));
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