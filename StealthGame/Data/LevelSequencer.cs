using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StealthGame.Data.PlayerPath;

namespace StealthGame.Data
{
    public class LevelSequencer
    {
        private readonly GameScene gameScene;
        private List<Level> levels = new List<Level>();
        private int levelIndex = 0;

        public LevelSequencer(GameScene gameScene)
        {
            this.gameScene = gameScene;
        }
        
        public void NextLevel()
        {
            this.levelIndex++;
            if (this.levelIndex < this.levels.Count)
            {
                this.gameScene.LoadLevel(this.levels[this.levelIndex]);
            }
            else
            {
                this.gameScene.WinScreen();
            }
        }

        public Level AddLevel(PlayerPathBuilder playerPathBuilder, Action<Level> onLoad)
        {
            var level = new Level(gameScene, playerPathBuilder, this);
            level.onLoad += onLoad;
            this.levels.Add(level);

            if (this.levels.Count == 1)
            {
                level.Load();
            }
            return level;
        }
    }
}