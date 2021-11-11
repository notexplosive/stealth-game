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

        public Level(GameScene gameScene, PlayerPathBuilder playerPathBuilder)
        {
            this.gameScene = gameScene;
            this.gameScene.CreatePlayer(playerPathBuilder);
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
    }
}