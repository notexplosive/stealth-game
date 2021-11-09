namespace StealthGame.Data
{
    public class BeatTracker
    {
        private float internalBeat;
        public int CurrentBeat => (int) this.internalBeat;

        public void AddBeat(float dt)
        {
            var beats = Seconds2Beats(dt);
            this.internalBeat += beats;
        }

        public const float SecondsPerBeat = 0.1f;

        public static float Beat2Seconds(int beats)
        {
            return SecondsPerBeat * beats;
        }

        public static float Seconds2Beats(float seconds)
        {
            return seconds / SecondsPerBeat;
        }
    }
}