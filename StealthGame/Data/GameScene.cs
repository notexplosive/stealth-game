using System;
using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Components;
using StealthGame.Data.Enemy;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Data
{
    public interface IScene
    {
        void SwitchTo(Scene originalScene);
    }

    public class GameScene : IScene
    {
        private readonly SceneLayers sceneLayers;
        private readonly List<Wall> wallList = new List<Wall>();
        private readonly List<EnemyDetection> enemyDetections = new List<EnemyDetection>();
        private BeatTracker worldBeatTracker;
        private EditModeToggle<EditorScene> editMode;
        private Scene scene;
        private Actor player;

        public GameScene(SceneLayers sceneLayers)
        {
            this.sceneLayers = sceneLayers;
        }

        public void SwitchTo(Scene originalScene)
        {
            if (originalScene != null)
            {
                this.sceneLayers.RemoveScene(originalScene);
            }

            this.scene = this.sceneLayers.AddNewScene();

            this.worldBeatTracker = new BeatTracker(true);
            this.worldBeatTracker.LoopHit += () => { MachinaGame.Print("Loop!"); };

            var world = this.scene.AddActor("World");
            new AdvanceWorldBeat(world, this.worldBeatTracker);

            this.editMode = new EditModeToggle<EditorScene>(world, new EditorScene(this.sceneLayers));
        }

        public Transform PlayerTransform => this.player.transform;

        public void CreatePlayer(PathBuilder pathBuilder)
        {
            var path = pathBuilder.Build();
            var playerBeatTracker = new BeatTracker(false);

            this.player = this.scene.AddActor("Player");
            new PlayerInput(this.player, playerBeatTracker);
            new PlayerMovement(this.player, playerBeatTracker, path);
            new CircleRenderer(this.player, 32, Color.Orange);
            new Editable<EditorScene>(this.player, this.editMode, (editor) => { editor.AddPlayerPath(pathBuilder); });

            CreatePath(pathBuilder);
        }

        public void CreateWall(Rectangle rectangle)
        {
            var wallActor = this.scene.AddActor("enemy", rectangle.Location.ToVector2());
            var boundingRect = new BoundingRect(wallActor, rectangle.Size);
            new BoundingRectFill(wallActor, Color.Orange);
            new Wall(wallActor, this.wallList);
            new Editable<EditorScene>(wallActor, this.editMode, (editor) => { editor.AddWall(boundingRect.Rect); });
        }

        private IList<Wall> GetWalls()
        {
            return this.wallList;
        }

        public Actor CreateBlinkingEnemy(TransformState state, Blink.Sequence blinkSequence)
        {
            var enemyActor = this.scene.AddActor("enemy", state.position);
            new LineOfSight(enemyActor, this, GetWalls);
            new FacingDirection(enemyActor, state.Angle);
            var cone = new ConeOfVision(enemyActor, MathF.PI / 2);
            var enemy = new Blink(cone, blinkSequence);
            this.worldBeatTracker.RegisterBehavior(enemy);
            new EnemyDetection(enemyActor, this.enemyDetections);
            new Editable<EditorScene>(enemyActor, this.editMode, (editor) =>
            {
                editor.AddBlinkingEnemy(state, blinkSequence);
            });

            return enemyActor;
        }

        public void CreateMovingEnemy(TransformBeatAnimation animation)
        {
            var enemyActor = this.scene.AddActor("enemy", animation.startingState.position);
            new LineOfSight(enemyActor, this, GetWalls);
            new FacingDirection(enemyActor, animation.startingState.Angle);
            new ConeOfVision(enemyActor, MathF.PI / 2);
            var enemy = new AnimatedEnemy(enemyActor, animation);
            this.worldBeatTracker.RegisterBehavior(enemy);
            new EnemyDetection(enemyActor, this.enemyDetections);
            new Editable<EditorScene>(enemyActor, this.editMode, (editor) =>
            {
                editor.AddMovingEnemy(animation);
            });
        }

        public void CreatePath(PathBuilder pathBuilder)
        {
            var path = this.scene.AddActor("Path");
            new PathRenderer(path, pathBuilder.Build(), this.enemyDetections);
        }
    }
}