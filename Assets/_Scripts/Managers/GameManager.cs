using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Units;
using CarGame.Managers;
using CarGame.Controllers;
using CarGame.Helpers;
using UnityEngine.UI;
using TMPro;

namespace CarGame
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int _distanceToAddEveryTick;
        [SerializeField] private float _secondsBetwenDistanceTick;

        [Header("Dependencies")]
        [SerializeField] private TextMeshProUGUI distanceUI;
        [SerializeField] private RectTransform HPPanel;
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private RoadLineController roadLineController;

        [Header("spawner settings")]
        private float timeBetweenObsticaleSpawns = 0.5f;


        private GameHUDController _gameHUDController;
  
    
        private PlayerManager _playerManager;
        private IObsticaleSpawner _obsticleSpawner;

        



        private void OnEnable()
        {
            _obsticleSpawner = GetComponent<IObsticaleSpawner>();
            _gameHUDController = new GameHUDController(distanceUI, HPPanel, heartPrefab);
        }


        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
            StartCoroutine(DistanceUpdateHandle(_distanceToAddEveryTick));
            StartCoroutine(SpawnOffRoadRandomObsticale());
            StartCoroutine(SpawnMainRoadRandomObsticale());
        }



        private void Update()
        {
            roadLineController.Advance(_playerManager.baseSpeed * _playerManager.playerCurrentState.speedMultiplier * 0.35f);
        }


        private IEnumerator DistanceUpdateHandle(int distanceToAdd)
        {
            while (true)
            {
                yield return new WaitForSeconds(_secondsBetwenDistanceTick);
                _gameHUDController.UpdateDistance(distanceToAdd);
            }
        }

        private IEnumerator SpawnOffRoadRandomObsticale()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenObsticaleSpawns);
                _obsticleSpawner.RandomOffRoadSpawn();
            }
        }

        private IEnumerator SpawnMainRoadRandomObsticale()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenObsticaleSpawns);
                _obsticleSpawner.RandomMainRoadSpawn();
            }
        }


    }
}