using System.Collections.Immutable;
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
        public EditorHandle selected;

        public EditorScene(SceneLayers sceneLayers)
        {
            this.sceneLayers = sceneLayers;
            StealthGame.CurrentEditor = this;
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
            CreateEditorHandle(wallActor);
            new Editable<GameScene>(wallActor, this.playMode, (game) =>
            {
                game.CreateWall(boundingRect.Rect);
            });
        }

        private void CreateEditorHandle(Actor wallActor)
        {
            new EditorHandle(wallActor, this);
        }

        public void AddPlayerPath(PathBuilder path)
        {
            var root = this.scene.AddActor("PathRoot");
            var prevPosition = path.startPosition;
            foreach (var instruction in path.Instructions())
            {
                var currentNode = root.transform.AddActorAsChild("Node", prevPosition);

                CreateEditorHandle(currentNode);
                new PathInstructionWrapper(currentNode, instruction);
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
                    var instruction = child.GetComponent<PathInstructionWrapper>().Rebuild(nextPosition);
                    
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

            CreateEditorHandle(root);
            new Editable<GameScene>(root, this.playMode, (game) =>
            {
                game.CreateBlinkingEnemy(new TransformState(root.transform.Position, root.transform.Angle),
                    sequence);
            });
        }

        public void AddMovingEnemy(TransformBeatAnimation animation)
        {
            var root = CreateAnimationEditor(animation);

            new Editable<GameScene>(root, this.playMode, (game) =>
            {
                game.CreateMovingEnemy(CreateAnimation(root));
            });
        }

        private Actor CreateAnimationEditor(TransformBeatAnimation animation)
        {
            var root = this.scene.AddActor("AnimationRoot");

            var currentState = animation.startingState;

            var startNode = root.transform.AddActorAsChild("startNode", currentState.position);
            CreateEditorHandle(startNode);
            
            foreach (var instruction in animation.originalBuilder.instructions)
            {
                var node = root.transform.AddActorAsChild("PathNode", instruction.EndState(currentState).position);
                node.transform.Angle = instruction.EndState(currentState).Angle;

                if (!(instruction is ForceSetAngleInstruction || instruction is LookToInstruction || instruction is WaitForInstruction))
                {
                    CreateEditorHandle(node);
                }

                new AnimationInstructionWrapper(node, instruction);

                currentState = instruction.EndState(currentState);
            }
            
            return root;
        }

        private TransformBeatAnimation CreateAnimation(Actor root)
        {
            var newBuilder = new AnimationBuilder();
            var newStartingState = new TransformState(root.transform.ChildAt(0).transform);
            var pos = newStartingState;
                
            for (int i = 1; i < root.transform.ChildCount; i++)
            {
                var child = root.transform.ChildAt(i);
                var instruction = child.GetComponent<AnimationInstructionWrapper>().Rebuild(pos);
                newBuilder.AddInstruction(instruction);
                pos = instruction.EndState(pos);
            }

            return new TransformBeatAnimation(newBuilder, newStartingState);
        }

        public void Select(EditorHandle editorHandle)
        {
            this.selected = editorHandle;
        }
    }
}