using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CarGame.Units;
using CarGame.Managers;
using CarGame.Controllers;
using CarGame.Helpers;
using UnityEngine.UI;
using TMPro;
using System;

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
        [SerializeField] private GameHUDManager _gameHUDController;
        [SerializeField] private DeathScreenManager _deathMenuManager;

        [Header("spawner settings")]
        private float timeBetweenObsticaleSpawns = 0.5f;
        private float timeBetweenVehicleSpawns = 0.8f;

        private int playersScore = 0;
  
    
        private PlayerManager _playerManager;
        private IObsticaleSpawner _obsticleSpawner;

        



        private void OnEnable()
        {
            _obsticleSpawner = GetComponent<IObsticaleSpawner>();
        }


        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();

            _playerManager.OnPlayerHit += _gameHUDController.UpdateHP;

            _playerManager.OnPlayerDeath += DeathScreenShow;

            _playerManager.ModifyHP(_playerManager.startingHP);


            StartCoroutine(DistanceUpdateHandle(_distanceToAddEveryTick));
            StartCoroutine(SpawnOffRoadRandomObsticale());
            StartCoroutine(SpawnMainRoadRandomObsticale());
        }



        private void Update()
        {
            roadLineController.Advance(_playerManager.baseSpeed * _playerManager.playerCurrentState.speedMultiplier * 0.35f);
        }


        public void DeathScreenShow()
        {
            _deathMenuManager.DisplayScore(playersScore);

            _gameHUDController.gameObject.SetActive(false);
            _deathMenuManager.gameObject.SetActive(true);

            Time.timeScale = 0f;
        }


        private IEnumerator DistanceUpdateHandle(int distanceToAdd)
        {
            while (true)
            {
                yield return new WaitForSeconds(_secondsBetwenDistanceTick);
                playersScore = _gameHUDController.UpdateDistance( Mathf.FloorToInt(distanceToAdd * _playerManager.playerCurrentState.speedMultiplier));
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
                yield return new WaitForSeconds(timeBetweenVehicleSpawns);
                _obsticleSpawner.RandomMainRoadSpawn();
            }
        }


    }
}