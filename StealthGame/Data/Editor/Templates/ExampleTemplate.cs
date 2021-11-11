using Machina.Components;
using Machina.Engine;

namespace StealthGame.Data.Editor.Templates
{
    public class Example
    {
        
    }
    
    public class ExampleTemplate
    {
        public class Factory : EntityFactory<Example, Play, Editor, Dehydrated>
        {
            public override Play CreatePlayEntity(Dehydrated dehydrated, Scene scene)
            {
                return new Play(dehydrated, scene);
            }

            public override Editor CreateEditorEntity(Dehydrated dehydrated, Scene scene)
            {
                return new Editor(dehydrated, scene);
            }

            public override Dehydrated DehydratePlayEntity(Play playEntity)
            {
                return new Dehydrated(playEntity.Actor.transform);
            }

            public override Dehydrated DehydrateEditorEntity(Editor editorEntity)
            {
                return new Dehydrated(editorEntity.Actor.transform);
            }
        }

        public class Editor : EditorEntity<Example, Dehydrated>
        {
            public Editor(Dehydrated dehydrated, Scene scene) : base(dehydrated, scene)
            {
                Actor = scene.AddActor("Example");
            }

            public override Actor Actor { get; }
        }

        public class Dehydrated : Dehydrated<Example>
        {
            public Dehydrated(Transform transform) : base(transform)
            {
            }

            public override string Serialize()
            {
                return "example";
            }
        }

        public class Play : PlayEntity<Example, Dehydrated>
        {
            public Play(Dehydrated dehydrated, Scene scene) : base(dehydrated, scene)
            {
                Actor = scene.AddActor("Example");
            }

            public override Actor Actor { get; }
        }
    }
}