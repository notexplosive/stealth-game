using Machina.Components;
using Machina.Engine;
using Microsoft.Xna.Framework;
using StealthGame.Components;
using StealthGame.Data.Enemy;
using StealthGame.Data.Enemy.Animation;
using StealthGame.Data.PlayerPath;

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
            var prevPosition = path.startPosition;
            foreach (var instruction in path.Instructions())
            {
                var currentNode = root.transform.AddActorAsChild("Node", prevPosition);
                new EditorHandle(currentNode);
                new InstructionWrapper(currentNode, instruction);
                prevPosition = instruction.EndPosition;
            }

            new Editable<GameScene>(root, this.playMode, (game) =>
            {
                PathBuilder pathBuilder = null;
                for (int i = 0; i < root.transform.ChildCount; i++)
                {
                    var child = root.transform.ChildAt(i);
                    var nextPosition = 
                        root.transform.HasChildAt(i + 1) ? 
                            root.transform.ChildAt(i+ 1).transform.Position 
                            : new Vector2();
                    var instruction = child.GetComponent<InstructionWrapper>().Rebuild(nextPosition);
                    
                    if (pathBuilder == null)
                    {
                        // initialize PathBuilder on first iteration
                        pathBuilder = new PathBuilder(path.startPosition);
                    }
                    
                    pathBuilder.AddInstruction(instruction);
                }
                game.CreatePlayer(pathBuilder);
            });
        }

        public void AddBlinkingEnemy(TransformState state, Blink.Sequence sequence)
        {
            var root = this.scene.AddActor("EnemyRoot", state.position, state.Angle);

            new EditorHandle(root);
            new Editable<GameScene>(root, this.playMode, (game) =>
            {
                game.CreateBlinkingEnemy(new TransformState(root.transform.Position, root.transform.Angle),
                    sequence);
            });
        }

        public void AddMovingEnemy(TransformBeatAnimation animation)
        {
            var root = this.scene.AddActor("EnemyPathRoot");

            new Editable<GameScene>(root, this.playMode, (game) =>
            {
                var newAnimation = animation; // todo
                
                game.CreateMovingEnemy(newAnimation);
            });
        }
    }
}