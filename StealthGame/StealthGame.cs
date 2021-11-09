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
            
            var beatTracker = new BeatTracker();
            var walkingPath = pathBuilder.Build();

            var actor = gameScene.AddActor("BeatTracker", new Vector2(200, 200));
            new PlayerInput(actor, beatTracker);
            new BoundingRect(actor, new Point(200, 200));
            var beatText = new BoundedTextRenderer(actor, "0", Assets.GetSpriteFont("DefaultFont"), Color.White);

            var player = gameScene.AddActor("Player");
            new PlayerMovement(player, beatTracker, walkingPath);
            new CircleRenderer(player, 32, Color.Orange);

            var walls = new Wall[]
            {
                CreateWall(gameScene, new Rectangle(600, 400, 100, 100)),
                CreateWall(gameScene, new Rectangle(700, 500, 100, 100)),
                CreateWall(gameScene, new Rectangle(800, 600, 100, 100)),
            };
            
            var eye = gameScene.AddActor("eye", new Vector2(800,500));
            new LineOfSight(eye, player.transform, walls);
            new FacingDirection(eye, MathF.PI / 4);
            var cone = new ConeOfVision(eye, MathF.PI / 2);

            var path = gameScene.AddActor("Path");
            new PathRenderer(path, walkingPath);
            
            new AdHoc(actor).onUpdate += (dt) =>
            {
                beatText.Text = beatTracker.CurrentBeat.ToString();
                if (cone.IsWithinCone(player.transform.Position))
                {
                    MachinaGame.Print("Can See");
                }
                else
                {
                    MachinaGame.Print("Cannot See");
                }
            };
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