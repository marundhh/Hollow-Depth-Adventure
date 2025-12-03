using UnityEngine;

public class ExpOrb : MonoBehaviour
{
    [Header("EXP Value")]
    public int expAmount = 10;

    [Header("Attract Settings")]
    public float attractDistance = 2f;
    public float attractSpeed = 6f;

    private Transform player;

    void Start()
    {
        player = PlayerStats.instance.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Khi đứng gần thì hút vào player
        if (distance <= attractDistance)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                attractSpeed * Time.deltaTime
            );
        }
    }

    // ⭐ Chạm vào Player = nhận EXP + biến mất
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExperienceManager.instance.AddExp(expAmount);
            Destroy(gameObject);
        }
    }
}
