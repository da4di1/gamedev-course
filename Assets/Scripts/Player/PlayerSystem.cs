using System;
using System.Collections.Generic;
using InputReader;

namespace Player
{
    public class PlayerSystem : IDisposable
    {
        private readonly PlayerEntityHandler _player;
        private readonly PlayerBrain _playerBrain;

        public PlayerSystem(PlayerEntityHandler player, List<IEntityInputSource> inputSources)
        {
            _player = player;
            _playerBrain = new PlayerBrain(player, inputSources);
        }

        public void Dispose() => _playerBrain.Dispose();
    }
}