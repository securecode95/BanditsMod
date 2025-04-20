// -------------------------------------------------------------------
// 3. Bandit AI - overrides targeting logic
// -------------------------------------------------------------------
namespace Patches
{
    [HarmonyPatch(typeof(EntityBandit), "UpdateTarget")]
    public static class BanditPriorityPatch
    {
        public static bool Prefix(EntityBandit __instance)
        {
            // 1. Try to target highest-priority player
            var player = BanditAI.FindHighestPriorityPlayer(__instance);
            if (player != null)
            {
                __instance.SetTarget(player);
                return false; // skip default
            }
            // 2. Default behavior: zombies, etc.
            return true;
        }
    }
}

public static class BanditAI
{
    // Example: pick nearest visible player
    public static Character FindHighestPriorityPlayer(EntityBandit bandit)
    {
        Character best = null;
        float bestDist = float.MaxValue;

        foreach (var player in GameManager.Instance.World.Entities.Players.list)
        {
            if (player.IsDead()) continue;
            // simple visibility check
            if (!bandit.CanSeePlayer(player)) continue;

            float dist = Vector3.Distance(bandit.position, player.position);
            if (dist < bestDist)
            {
                bestDist = dist;
                best = player;
            }
        }
        return best;
    }
}