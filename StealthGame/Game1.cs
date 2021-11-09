using Machina.Components;
using Machina.Engine;
using Machina.Engine.AssetLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StealthGame.Components;
using StealthGame.Data;

namespace StealthGame
{
    public class Game1 : MachinaGame
    {
        public Game1(string[] args) : base("Stealth Game", args, new Point(1920, 1080), new Point(1920, 1080),
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
            new AdHoc(actor).onUpdate += (dt) => { beatText.Text = beatTracker.CurrentBeat.ToString(); };
            
            var player = gameScene.AddActor("Player");
            new PlayerMovement(player, beatTracker, walkingPath);
            new CircleRenderer(player, 32, Color.Orange);

            var path = gameScene.AddActor("Path");
            new PathRenderer(path, walkingPath);
        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }
    }
}