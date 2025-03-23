using UnityEngine;

public class MonkeyCatchableScript : BaseHittable
{
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private Animator anim;
    AudioManager audioManager;

    void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnEnable()
    {
        PlayerHitScript.OnEntityHit += OnEntityHit;
    }

    private void OnDisable()
    {
        PlayerHitScript.OnEntityHit -= OnEntityHit;
    }

    private void OnEntityHit(IHittable target, PlayerStats playerStats)
    {
        // Ensure the event is intended for this player instance.
        if ((Object) target == this) {
            OnHit();
        }
    }

    public override void OnHit()
    {
        if (anim == null)
        {
            Debug.LogError("Animator is not assigned!", this);
            return;
        }
        anim.SetBool("isRunning",false);
        anim.SetBool("isDead",true);
        audioManager.PlaySFX(audioManager.punchMonkey);
        Destroy(this.gameObject,0.5f);
        spawnPaper();
    }

    void spawnPaper() {
        Instantiate(paperPrefab, this.transform.position, Quaternion.identity);
    }
}
