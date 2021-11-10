using System;
using System.Collections.Generic;
using Machina.Engine;
using StealthGame.Data.Enemy;

namespace StealthGame.Data
{
    public class BeatTracker
    {
        public event Action<int> BeatHit;
        public event Action LoopHit;
        private float internalBeat;
        public int CurrentBeat => (int) this.internalBeat;

        public void AddBeat(float dt)
        {
            var beats = Seconds2Beats(dt);
            var oldIntegerBeat = CurrentBeat;
            this.internalBeat += beats;

            if (CurrentBeat != oldIntegerBeat)
            {
                BeatHit?.Invoke(CurrentBeat);
            }

            if (this.internalBeat > TotalBeats)
            {
                LoopHit?.Invoke();
                this.internalBeat %= TotalBeats;
            }
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

        private readonly List<int> allBeatCounts = new List<int>();
        public int TotalBeats { get; private set; }

        public void AppendTotalBeatCount(int totalBeatsFromBehavior)
        {
            if (totalBeatsFromBehavior == 0)
            {
                return;
            }
            
            this.allBeatCounts.Add(totalBeatsFromBehavior);


            if (this.allBeatCounts.Count == 1)
            {
                TotalBeats = this.allBeatCounts[0];
            }
            else
            {
                var beatCountCopy = new List<int>(this.allBeatCounts);
                beatCountCopy.Sort();
                var maxBeat = beatCountCopy[beatCountCopy.Count - 1];
                beatCountCopy.RemoveAt(beatCountCopy.Count - 1);

                while (beatCountCopy.Count > 0)
                {
                    var lastItem = beatCountCopy[beatCountCopy.Count - 1];
                    beatCountCopy.RemoveAt(beatCountCopy.Count - 1);
                    Console.WriteLine("Racing " + lastItem + ", " + maxBeat);
                    maxBeat = RaceNumbers(lastItem, maxBeat);
                }

                TotalBeats = maxBeat;
            }
        }

        private int RaceNumbers(int a, int b)
        {
            var originalA = a;
            var originalB = b;
            while (a != b)
            {
                if (a > b)
                {
                    b += originalB;
                }
                else if (a < b)
                {
                    a += originalA;
                }
            }

            return a;
        }
    }
}