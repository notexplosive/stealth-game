using Machina.Engine;

namespace StealthGame.Data.Editor
{
    public abstract class EntityFactory<T, TPlay, TEditor, TDehydrated> 
        where TPlay : PlayEntity<T, TDehydrated>
        where TEditor : EditorEntity<T, TDehydrated>
        where TDehydrated : Dehydrated<T>
    {
        public abstract TPlay CreatePlayEntity(TDehydrated dehydrated, Scene scene);
        public abstract TEditor CreateEditorEntity(TDehydrated dehydrated, Scene scene);
        public abstract TDehydrated DehydratePlayEntity(TPlay playEntity);
        public abstract TDehydrated DehydrateEditorEntity(TEditor editorEntity);
    }
}