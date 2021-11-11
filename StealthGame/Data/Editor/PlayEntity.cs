using Machina.Engine;

namespace StealthGame.Data.Editor
{
    public abstract class PlayEntity<T, TDehydrated> where TDehydrated : Dehydrated<T>
    {
        public abstract Actor Actor { get; }
        
        protected PlayEntity(TDehydrated dehydrated, Scene scene)
        {
        }
    }
}