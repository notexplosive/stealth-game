using System;
using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using StealthGame.Data.Enemy;
using StealthGame.Data.Enemy.Animation;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Data
{
    
    public class Level
    {
        private readonly GameScene gameScene;
        public event Action<Level> OnLoad;

        public Level(GameScene gameScene, PlayerPathBuilder playerPathBuilder, LevelSequencer levelSequencer)
        {
            this.gameScene = gameScene;

            OnLoad += (level) =>
            {
                var player = level.gameScene.CreatePlayer(playerPathBuilder);

                player.LevelFinished += levelSequencer.NextLevel;
            };
        }

        public void CreateBackAndForthPatroller(float startAngle, Vector2 p1, Vector2 p2 )
        {
            var backToStart = MathF.PI + startAngle;
            var start = new TransformState(p1, startAngle);
            var builder = new AnimationBuilder()
                    .MoveTo(p2)
                    .LookTo(backToStart, 20)
                    .MoveTo(p1)
                    .LookTo(startAngle, 20)
                ;

            this.gameScene.CreateMovingEnemy(new TransformBeatAnimation(builder, start));
        }

        public void CreateCamera(Vector2 position, float angle1, float angle2, int rotationSpeed = 20)
        {
            var start = new TransformState(position, angle1);
            var builder = new AnimationBuilder()
                    .LookTo(angle2, rotationSpeed)
                    .WaitFor(10)
                    .LookTo(angle1, rotationSpeed)
                    .WaitFor(10)
                ;

            this.gameScene.CreateMovingEnemy(new TransformBeatAnimation(builder, start));
        }

        public void Load()
        {
            OnLoad?.Invoke(this);
        }
    }
}