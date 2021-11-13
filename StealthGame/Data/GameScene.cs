using System;
using System.Collections.Generic;
using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Components;
using StealthGame.Data.Enemy;
using StealthGame.Data.Enemy.Animation;
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
        private PlayerInput playerInput;
        private Actor entityRoot;

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

            this.entityRoot = this.scene.AddActor("Entities");
            this.worldBeatTracker = new BeatTracker(true);
            this.worldBeatTracker.LoopHit += () => { MachinaGame.Print("Loop!"); };

            var world = this.scene.AddActor("World");
            new AdvanceWorldBeat(world, this.worldBeatTracker);

            this.editMode = new EditModeToggle<EditorScene>(world, new EditorScene(this.sceneLayers));
        }

        public PlayerMovement CreatePlayer(PlayerPathBuilder playerPathBuilder)
        {
            var path = playerPathBuilder.Build();
            var playerBeatTracker = new BeatTracker(false);

            this.player = this.entityRoot.transform.AddActorAsChild("Player");
            this.playerInput = new PlayerInput(this.player, playerBeatTracker);
            var playerMovement = new PlayerMovement(this.player, playerBeatTracker, path);
            new CircleRenderer(this.player, 32, Color.Orange);
            new Editable<EditorScene>(this.player, this.editMode, (editor) => { editor.AddPlayerPath(playerPathBuilder); });

            CreatePath(playerPathBuilder);
            return playerMovement;
        }

        public void CreateWall(Point topLeft, Point bottomRight)
        {
            var wallActor = this.entityRoot.transform.AddActorAsChild("enemy", topLeft.ToVector2());
            var boundingRect = new BoundingRect(wallActor, new Point(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y));
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
            var enemyActor = this.entityRoot.transform.AddActorAsChild("enemy", state.position);
            new LineOfSight(enemyActor, this, GetWalls);
            new FacingDirection(enemyActor, state.Angle);
            var cone = new ConeOfVision(enemyActor, MathF.PI / 2);
            var enemy = new Blink(cone, blinkSequence);
            this.worldBeatTracker.RegisterBehavior(enemy);
            new EnemyDetection(enemyActor, this.enemyDetections, this.playerInput);
            new Editable<EditorScene>(enemyActor, this.editMode, (editor) =>
            {
                editor.AddBlinkingEnemy(state, blinkSequence);
            });

            return enemyActor;
        }

        public void CreateMovingEnemy(TransformBeatAnimation animation)
        {
            var enemyActor = this.entityRoot.transform.AddActorAsChild("enemy", animation.startingState.position);
            new LineOfSight(enemyActor, this, GetWalls);
            new FacingDirection(enemyActor, animation.startingState.Angle);
            new ConeOfVision(enemyActor, MathF.PI / 2);
            var enemy = new AnimatedEnemy(enemyActor, animation);
            this.worldBeatTracker.RegisterBehavior(enemy);
            new EnemyDetection(enemyActor, this.enemyDetections, this.playerInput);
            new Editable<EditorScene>(enemyActor, this.editMode, (editor) =>
            {
                editor.AddMovingEnemy(animation);
            });
        }

        public void CreatePath(PlayerPathBuilder playerPathBuilder)
        {
            var path = this.entityRoot.transform.AddActorAsChild("Path");
            new PathRenderer(path, playerPathBuilder.Build(), this.enemyDetections);
        }

        public void LoadLevel(Level level)
        {
            DeleteAllEntities();
            this.worldBeatTracker.Reset();
            level.Load();
        }

        public void WinScreen()
        {
            DeleteAllEntities();
        }

        private void DeleteAllEntities()
        {
            for (int i = 0; i < this.entityRoot.transform.ChildCount; i++)
            {
                this.entityRoot.transform.ChildAt(i).Delete();
            }
        }
    }
}