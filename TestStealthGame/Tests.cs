using FluentAssertions;
using NUnit.Framework;
using StealthGame.Data;

namespace TestStealthGame
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void convert_beats_to_seconds()
        {
            BeatTracker.Beat2Seconds(10).Should().Be(1);
        }
        
        [Test]
        public void convert_seconds_to_beats()
        {
            BeatTracker.Seconds2Beats(1).Should().Be(10);
        }
    }
}