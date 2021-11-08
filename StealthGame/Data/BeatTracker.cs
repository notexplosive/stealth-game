namespace StealthGame.Data
{
    public class BeatTracker
    {
        private float internalBeat;
        public int CurrentBeat => (int) this.internalBeat;

        public void AddBeat(float dt)
        {
            var beats = dt / durationOfOneBeat;
            this.internalBeat += beats;
        }

        public const float durationOfOneBeat = 0.1f;

        public static float TimeAt(int beatIndex)
        {
            return durationOfOneBeat * beatIndex;
        }
    }
}