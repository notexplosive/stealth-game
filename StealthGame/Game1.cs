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
        public Game1(string[] args) : base("Stealth Game", args, new Point(1920,1080), new Point(1920,1080), ResizeBehavior.MaintainDesiredResolution)
        {
        }

        protected override void OnGameLoad()
        {
            var gameScene = SceneLayers.AddNewScene();
            var actor = gameScene.AddActor("BeatTracker", new Vector2(200,200));
            
            var tracker = new BeatTracker();

            new PlayerInput(actor, tracker);
            new BoundingRect(actor, new Point(200, 200));
            var beatText = new BoundedTextRenderer(actor, "0", Assets.GetSpriteFont("DefaultFont"), Color.White);
            new AdHoc(actor).onUpdate += (dt) =>
            {
                beatText.Text = tracker.CurrentBeat.ToString();
            };

        }

        protected override void PrepareDynamicAssets(AssetLoadTree tree)
        {
        }
    }
}
