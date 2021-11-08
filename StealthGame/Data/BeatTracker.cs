namespace StealthGame.Data
{
    public class BeatTracker
    {
        private float internalBeat;
        public int CurrentBeat => (int) this.internalBeat;

        public void AddBeat(float dt)
        {
            var beats = dt * 10;
            this.internalBeat += beats;
        }
    }
}