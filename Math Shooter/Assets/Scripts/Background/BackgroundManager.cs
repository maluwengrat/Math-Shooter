using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Math Shooter — BackgroundManager
/// </summary>
public class BackgroundManager : MonoBehaviour
{
    [Header("Referência ao PixelBackground")]
    public PixelBackground pixelBg;

    // Singleton
    public static BackgroundManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (pixelBg == null)
                pixelBg = GetComponent<PixelBackground>();

            if (pixelBg == null)
                pixelBg = gameObject.AddComponent<PixelBackground>();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (pixelBg == null)
        {
            Debug.LogError("PixelBackground NÃO encontrado! Verifique a referência.");
        }
        else
        {
            Debug.Log($"PixelBackground encontrado: {pixelBg.gameObject.name}");
            Debug.Log($"Tipo atual: {pixelBg.backgroundType}");
            Debug.Log($"Posição Z: {pixelBg.transform.position.z}");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryFindPixelBackground();
        if (GameManager.instance != null && GameManager.instance.JogoRodando())
            SetStage(GameManager.instance.GetFaseAtual());
        else
            SetMainMenu();
    }

    void TryFindPixelBackground()
    {
        if (pixelBg == null)
            pixelBg = FindObjectOfType<PixelBackground>();

        if (pixelBg == null)
            Debug.LogWarning("BackgroundManager: nenhum PixelBackground encontrado na cena!");
    }

    public void ChangeBackground(PixelBackground.BackgroundType type)
    {
        if (pixelBg == null)
        {
            TryFindPixelBackground();
            if (pixelBg == null) return;
        }
        pixelBg.backgroundType = type;
    }

    // ── Métodos públicos ────────────────────────────────────

    public void SetMainMenu()
    {
        // Menu principal — espaço / nebulosa com planetas
        ChangeBackground(PixelBackground.BackgroundType.MainMenu);
    }

    public void SetHowToPlay()
    {
        ChangeBackground(PixelBackground.BackgroundType.HowToPlay);
    }

    /// <summary>
    /// Tela de próxima fase — não usa PixelBackground (é UI Canvas).
    /// Mantém o fundo da fase atual sem alteração.
    /// </summary>
    public void SetNextLevel()
    {
        // Sem ação: o Canvas da tela de próxima fase fica por cima do PixelBackground.
        // Se quiser um fundo específico descomente uma das linhas abaixo:
        // ChangeBackground(PixelBackground.BackgroundType.MainMenu);
    }

    public void SetGameOver()
    {
        ChangeBackground(PixelBackground.BackgroundType.GameOver);
    }

    public void SetStage(int stageNumber)
    {
        switch (stageNumber)
        {
            case 1:
                // Fase 1 — cidade ao entardecer com carros
                ChangeBackground(PixelBackground.BackgroundType.Stage1_Space);
                break;
            case 2:
                // Fase 2 — fábrica / engrenagens
                ChangeBackground(PixelBackground.BackgroundType.Stage2_Cave);
                break;
            case 3:
                // Fase 3 — mar / oceano
                ChangeBackground(PixelBackground.BackgroundType.Stage3_Forest);
                break;
            case 4:
                // Fase 4 — espaço nebulosa vermelha
                ChangeBackground(PixelBackground.BackgroundType.Stage4_City);
                break;
            default:
                Debug.LogWarning($"Fase {stageNumber} não existe!");
                break;
        }
    }
}