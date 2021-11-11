using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Components;

namespace StealthGame.Data
{
    public class EditorScene : IScene
    {
        private readonly SceneLayers sceneLayers;
        private Scene scene;
        private EditModeToggle<GameScene> playMode;

        public EditorScene(SceneLayers sceneLayers)
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
            
            var world = this.scene.AddActor("World");
            this.playMode = new EditModeToggle<GameScene>(world, new GameScene(this.sceneLayers));
        }

        public void AddWall(Rectangle rectangle)
        {
            var wallActor = this.scene.AddActor("enemy", rectangle.Location.ToVector2());
            var boundingRect = new BoundingRect(wallActor, rectangle.Size);
            new BoundingRectFill(wallActor, Color.Orange);
            new EditorHandle(wallActor);
            new Editable<GameScene>(wallActor, this.playMode, (game) =>
            {
                game.CreateWall(boundingRect.Rect);
            });
        }
    }
}