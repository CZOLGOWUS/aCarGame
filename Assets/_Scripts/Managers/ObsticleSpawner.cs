using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using CarGame.Managers;

namespace CarGame
{
    public class ObsticleSpawner : MonoBehaviour, IObsticaleSpawner
    {
        //dependencies
        private PlayerManager playerManager;

        [SerializeField] private List<Obsticale> offRoadObsticales = new List<Obsticale>();
        [SerializeField] private List<Obsticale> mainRoadObsticales = new List<Obsticale>();
        [SerializeField] private MeshCollider leftOffRoad;
        [SerializeField] private MeshCollider mainRoad;
        [SerializeField] private MeshCollider rightOffRoad;

        private Bounds leftOffRoadSpawnZone = new Bounds();
        private Bounds mainRoadSpawnZone = new Bounds();
        private Bounds rightOffRoadSpawnZone = new Bounds();

        private System.Random random = new System.Random(); 



        private void Start()
        {
            playerManager = FindObjectOfType<PlayerManager>();
            SetUpSpawningZones();

        }

        private void SetUpSpawningZones()
        {
            leftOffRoadSpawnZone = leftOffRoad.bounds;
            leftOffRoadSpawnZone.center += Vector3.forward * leftOffRoadSpawnZone.extents.z * 1.2f;
            leftOffRoadSpawnZone.extents = Vector3.right * leftOffRoadSpawnZone.extents.x ;

            rightOffRoadSpawnZone = rightOffRoad.bounds;
            rightOffRoadSpawnZone.center += Vector3.forward * rightOffRoadSpawnZone.extents.z * 1.2f;
            rightOffRoadSpawnZone.extents = Vector3.right * rightOffRoadSpawnZone.extents.x;

            mainRoadSpawnZone = mainRoad.bounds;
            mainRoadSpawnZone.center += Vector3.forward * mainRoadSpawnZone.extents.z * 1.2f;
            mainRoadSpawnZone.extents = Vector3.right * mainRoadSpawnZone.extents.x;
        }

        public void RandomOffRoadSpawn()
        {
            Vector3 randomSpawn = random.NextDouble() >= 0.5 ?
                leftOffRoadSpawnZone.center + Vector3.right * random.Next(-(int)leftOffRoadSpawnZone.extents.x, (int)leftOffRoadSpawnZone.extents.x) :
                rightOffRoadSpawnZone.center + Vector3.right * random.Next(-(int)rightOffRoadSpawnZone.extents.x, (int)rightOffRoadSpawnZone.extents.x);


            GameObject obsticale = Instantiate( offRoadObsticales[ random.Next(offRoadObsticales.Count)].gameObject , randomSpawn, Quaternion.Euler(0f,(float)random.NextDouble() * 360f,0f));
            obsticale.GetComponent<Obsticale>().playerManager = playerManager;
            Destroy(obsticale,10f);
        }

        public void RandomMainRoadSpawn()
        {
            Vector3 randomSpawn = mainRoadSpawnZone.center + Vector3.right * random.Next(-(int)mainRoadSpawnZone.extents.x + 1, (int)mainRoadSpawnZone.extents.x - 1);
      

            GameObject obsticale = Instantiate(mainRoadObsticales[random.Next(mainRoadObsticales.Count)].gameObject, randomSpawn, Quaternion.identity);
            obsticale.GetComponent<Obsticale>().playerManager = playerManager;
            Destroy(obsticale, 10f);
        }
    }
}
