using UnityEngine;

public class VictoryStateScript : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats.caughtAllMonkeys()) {
                GameStateManager.instance.victory();
            }
        }
    }
}
