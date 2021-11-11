using Machina.Engine;

namespace StealthGame.Data.Editor
{
    public abstract class EditorEntity<T, TDehydrated> where TDehydrated : Dehydrated<T>
    {
        public abstract Actor Actor { get; }
        
        protected EditorEntity(TDehydrated dehydrated, Scene scene)
        {
        }
    }
}