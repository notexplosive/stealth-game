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

        public void AddPlayerPath(PathBuilder path)
        {
            var root = this.scene.AddActor("PathRoot");
            foreach (var instruction in path.Instructions())
            {
                var currentNode = root.transform.AddActorAsChild("Node", instruction.StartPosition);
                new EditorHandle(currentNode);
                new InstructionWrapper(currentNode, instruction);
            }

            new Editable<GameScene>(root, this.playMode, (game) =>
            {
                PathBuilder pathBuilder = null;
                for (int i = 0; i < root.transform.ChildCount; i++)
                {
                    var child = root.transform.ChildAt(i);
                    var instruction = child.GetComponent<InstructionWrapper>().instruction;
                    if (pathBuilder == null)
                    {
                        pathBuilder = new PathBuilder(instruction.StartPosition);
                    }
                    
                    pathBuilder.AddInstruction(instruction);
                }
                game.CreatePlayer(pathBuilder);
            });
        }
    }
}