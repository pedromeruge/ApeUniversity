using UnityEngine;

public class OreStaticCollectible : MonoBehaviour, IStaticCollectible
{
    [SerializeField] private int oreValue = 200;
    public void onCollect(PlayerStats playerStats) {
        playerStats.modifyMoney(oreValue);
        playMoneyParticles();
        Destroy(this.gameObject);
    }

    private void playMoneyParticles() {
        // TODO: add particles
    }
}

