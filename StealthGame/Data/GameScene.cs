using System;
using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Components;
using StealthGame.Data.Enemy;

namespace StealthGame.Data
{
    public class GameScene
    {
        private readonly EditModeToggle editMode;
        private readonly Scene scene;
        private readonly BeatTracker worldBeatTracker;
        private readonly List<Wall> wallList = new List<Wall>();
        private readonly List<EnemyDetection> enemyDetections = new List<EnemyDetection>();
        private Actor player;

        public GameScene(Scene scene)
        {
            this.scene = scene;

            this.worldBeatTracker = new BeatTracker(true);
            this.worldBeatTracker.LoopHit += () => { MachinaGame.Print("Loop!"); };
            
            var world = scene.AddActor("World");
            var worldBeat = new AdvanceWorldBeat(world, this.worldBeatTracker);

            this.editMode = new EditModeToggle(world);

            this.editMode.EditModeToggled += worldBeat.OnEditModeToggled;
        }

        public Transform PlayerTransform => this.player.transform;

        public Actor CreatePlayer(WalkingPath path)
        {
            var playerBeatTracker = new BeatTracker(false);

            var player = this.scene.AddActor("Player");
            new PlayerInput(player, playerBeatTracker);
            new PlayerMovement(player, playerBeatTracker, path);
            new CircleRenderer(player, 32, Color.Orange);

            this.player = player;
            return player;
        }

        public Actor CreateWall(Rectangle rectangle)
        {
            var wallActor = this.scene.AddActor("enemy", rectangle.Location.ToVector2());
            new BoundingRect(wallActor, rectangle.Size);
            new BoundingRectFill(wallActor, Color.Orange);
            new Wall(wallActor, this.wallList);
            new Editable(wallActor, this.editMode);

            return wallActor;
        }

        private IList<Wall> GetWalls()
        {
            return this.wallList;
        }

        public Actor CreateBlinkingEnemy(Vector2 position, float angle, Blink.Sequence blinkSequence)
        {
            var enemyActor = this.scene.AddActor("enemy", position);
            new LineOfSight(enemyActor, this, GetWalls);
            new FacingDirection(enemyActor, angle);
            var cone = new ConeOfVision(enemyActor, MathF.PI / 2);
            var enemy = new Blink(cone, blinkSequence);
            this.worldBeatTracker.RegisterBehavior(enemy);
            new EnemyDetection(enemyActor, this.enemyDetections);
            new Editable(enemyActor, this.editMode);

            return enemyActor;
        }

        public Actor CreateMovingEnemy(TransformBeatAnimation animation)
        {
            var enemyActor = this.scene.AddActor("enemy", animation.startingState.position);
            new LineOfSight(enemyActor, this, GetWalls);
            new FacingDirection(enemyActor, animation.startingState.Angle);
            new ConeOfVision(enemyActor, MathF.PI / 2);
            var enemy = new AnimatedEnemy(enemyActor, animation);
            this.worldBeatTracker.RegisterBehavior(enemy);
            new EnemyDetection(enemyActor, this.enemyDetections);
            new Editable(enemyActor, this.editMode);

            return enemyActor;
        }

        public void CreatePath(WalkingPath walkingPath)
        {
            var path = this.scene.AddActor("Path");
            new PathRenderer(path, walkingPath, this.enemyDetections);
        }
    }
}