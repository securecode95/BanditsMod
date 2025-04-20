// -------------------------------------------------------------------
// 2. Bandit Manager - loads legacy models & tracks active bandits
// -------------------------------------------------------------------
public static class BanditManager
{
    private static readonly List<EntityBandit> ActiveBandits = new List<EntityBandit>();

    // Load legacy bandit prefabs by name
    public static GameObject LoadLegacyModel(string prefabName)
    {
        // Uses the game's PrefabManager to get the original model
        var prefab = PrefabManager.Instance.GetPrefab(prefabName);
        if (prefab == null)
        {
            Debug.LogError($"[BanditsMod] Could not load legacy prefab: {prefabName}");
        }
        return prefab;
    }

    public static void Register(EntityBandit bandit)
    {
        ActiveBandits.Add(bandit);
    }

    public static void Unregister(EntityBandit bandit)
    {
        ActiveBandits.Remove(bandit);
    }

    public static IEnumerable<EntityBandit> GetAll() => ActiveBandits;
}