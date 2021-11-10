using System.Collections.Generic;
using System.Transactions;
using Machina.Engine;
using StealthGame.Components;

namespace StealthGame.Data.Enemy
{
    public class Blink : IEnemyBehavior
    {
        private readonly ConeOfVision cone;
        private readonly bool[] sequence;

        public Blink(ConeOfVision cone, Sequence sequence)
        {
            this.cone = cone;
            this.sequence = sequence.GetContent();
        }

        public void OnBeat(int currentBeat)
        {
            var isOpen = this.sequence[currentBeat % TotalBeats];
            this.cone.SetActive(isOpen);
        }

        public int TotalBeats => this.sequence.Length;

        public class Sequence
        {
            private readonly List<bool> content = new List<bool>();

            public Sequence()
            {
            }

            public bool[] GetContent()
            {
                return this.content.ToArray();
            }

            public Sequence AddOn(int beats)
            {
                for (var i = 0; i < beats; i++)
                {
                    this.content.Add(true);
                }

                return this;
            }

            public Sequence AddOff(int beats)
            {
                for (var i = 0; i < beats; i++)
                {
                    this.content.Add(false);
                }

                return this;
            }
        }
    }
}