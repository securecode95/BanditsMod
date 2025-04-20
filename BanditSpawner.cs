 // -------------------------------------------------------------------
    // 4. Bandit Spawner - spawns bandits server-side using legacy models
    // -------------------------------------------------------------------
    public static class BanditSpawner
    {
        // To be called on server start
        public static void Initialize()
        {
            var world = GameManager.Instance.World;
            world.OnWorldInitialized += OnWorldInitialized;
        }

        private static void OnWorldInitialized()
        {
            // Example: spawn a bandit every 60 seconds at random bandit spawn points
            GameManager.Instance.Schedule.StartCoroutine(SpawnRoutine());
        }

        private static System.Collections.IEnumerator SpawnRoutine()
        {
            var spawnPoints = FindSpawnPoints();
            while (true)
            {
                yield return new WaitForSeconds(60f);

                var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                SpawnLegacyBandit(spawnPoint.position);
            }
        }

        private static List<Transform> FindSpawnPoints()
        {
            // Locate objects tagged or named in scene as "BanditSpawn"
            var results = new List<Transform>();
            foreach (var obj in GameObject.FindObjectsOfType<Transform>())
            {
                if (obj.name.Contains("BanditSpawn")) results.Add(obj);
            }
            return results;
        }

        private static void SpawnLegacyBandit(Vector3 position)
        {
            // Choose a prefab name, e.g. "banditSkeleton" or "banditNormal"
            string prefabName = Random.value < 0.5f ? "banditSkeleton" : "banditNormal";
            var prefab = BanditManager.LoadLegacyModel(prefabName);
            if (prefab == null) return;

            var go = GameObject.Instantiate(prefab, position, Quaternion.identity);
            var bandit = go.GetComponent<EntityBandit>();
            if (bandit != null)
            {
                BanditManager.Register(bandit);
            }
        }
    }