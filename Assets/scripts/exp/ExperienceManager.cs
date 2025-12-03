using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager instance;

    [Header("EXP Settings")]
    public int currentExp = 0;
    public int requiredExp = 100;
    public float expGrowthRate = 1.25f;

    [Header("Level")]
    public int level = 1;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Nhận EXP từ orb hoặc kẻ địch
    /// </summary>
    public void AddExp(int amount)
    {
        currentExp += amount;

        // Nếu đủ EXP thì lên level
        while (currentExp >= requiredExp)
        {
            currentExp -= requiredExp;
            LevelUp();
        }
    }

    /// <summary>
    /// Xử lý khi lên level
    /// </summary>
    private void LevelUp()
    {
        level++;

        // Tăng EXP cần cho level sau
        requiredExp = Mathf.RoundToInt(requiredExp * expGrowthRate);

        Debug.Log("LEVEL UP! Level hiện tại: " + level);

        // ⭐ THƯỞNG 1 POINT CHO NGƯỜI CHƠI
        StatUpgradeManager.instance.AddPoint(1);
    }
}
