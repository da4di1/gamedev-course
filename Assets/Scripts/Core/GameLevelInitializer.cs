using System.Collections.Generic;
using Core.Tools;
using InputReader;
using Player;
using UnityEngine;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private WorldBoundaries _levelBorders;
        [SerializeField] private PlayerEntityHandler _player;
        [SerializeField] private GameUIInputView _gameUIInputView;

        private ExternalDeviceInputReader _externalDeviceInputReader;
        private PlayerBrain _playerBrain;

        private bool _onPause = false;

        private void Awake()
        {
            _levelBorders.OnAwake();
            
            _externalDeviceInputReader = new ExternalDeviceInputReader();
            _playerBrain = new PlayerBrain(_player, new List<IEntityInputSource>
            {
                _gameUIInputView,
                _externalDeviceInputReader,
            });
        }
        
        private void Update()
        {  
            if (_onPause)
                return;
            
            _externalDeviceInputReader.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (_onPause)
                return;
            
            _playerBrain.OnFixedUpdate();
        }
    }
}