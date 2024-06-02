using UnityEngine;

public class ShockwaveSpawner : MonoBehaviour
{
    [SerializeField] Shockwave deathWave;
    [SerializeField] SpriteRenderer baseSprite;
    [Header("Settings")]
    [SerializeField] Color chargedColor;
    [SerializeField] Color unchargedColor = new Color(255, 255, 255);
    [SerializeField] int killsRequired = 20;
    [SerializeField] GameObject instructionText;
    Animator animator;

    const string SHOCKWAVE = "DeathWave";

    public static ShockwaveSpawner Instance;

    int kills = 0;

    bool charged = false;

    Color targetColor;
    float chargedSmoothTime = 0.5f;
    float smoothTime = 0.1f;

    void Awake()
    {   
        animator = GetComponent<Animator>();
        targetColor = unchargedColor;
        Instance = this;
    }

    void Start()
    {
        Controls.Instance.OnShockwaveAction += SpawnShockWave;
        instructionText.SetActive(false);
    }

    void Update()
    {   
        if (charged)
        {
            float t = Mathf.PingPong(Time.time / chargedSmoothTime, 1);
            baseSprite.color = Color.Lerp(unchargedColor, chargedColor, t); 
        }
        else
        {
            baseSprite.color = Color.Lerp(baseSprite.color, targetColor, smoothTime);
        }

    }

    void SpawnShockWave()
    {
        if (charged)
        {
            instructionText.SetActive(false);
            charged = false;
            animator.SetTrigger(SHOCKWAVE);
            kills = 0;
            targetColor = unchargedColor;
        }
    }

    public void Charge()
    {
        kills++;
        targetColor = Color.Lerp(unchargedColor, chargedColor, (float)kills / killsRequired);

        if (kills >= killsRequired)
        {
            instructionText.SetActive(true);
            charged = true;
        }
    }


}
