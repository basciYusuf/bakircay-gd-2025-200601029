using UnityEngine;
using UnityEngine.SceneManagement;

public class matchMaker : MonoBehaviour
{
    // Yeni eklenen deðiþkenler
    public Transform pointA; // Point A noktasý
    public float magnetStrength = 10f; // Mýknatýs kuvveti

    private GameObject selectedFruit1 = null;
    private GameObject selectedFruit2 = null;

    public GameObject explosionEffectPrefab;
    public GameObject[] availableFruits;
    public Transform[] fruitSpawnPoints;

    public float pushForce = 15f;
    public Transform tableCenterPoint;
    private int playerScore = 0;

    private int totalFruitsRemaining;
    private float timeRemaining = 60f;
    private bool gameEnded = false;
    private bool bonusActive = false;
    private bool timerPaused = false;
    private float timerResumeTime = 0f;

    private bool bonusLocked = false;
    private float bonusUnlockTime = 0f;
    private bool pauseLocked = false;
    private float pauseUnlockTime = 0f;

    private GUIStyle scoreStyle = new GUIStyle();
    private GUIStyle buttonStyle = new GUIStyle();
    private GUIStyle timeStyle = new GUIStyle();
    private GUIStyle endGameStyle = new GUIStyle();

    void Start()
    {
        // Yazý ve arka plan renklerini ayný yapýyoruz
        Color commonColor = Color.white; // Burada tek renk belirledik, ihtiyaca göre deðiþtirebilirsiniz

        scoreStyle.fontSize = 20;
        scoreStyle.normal.textColor = commonColor;

        buttonStyle.fontSize = 20;
        buttonStyle.normal.textColor = commonColor;
        buttonStyle.alignment = TextAnchor.MiddleLeft;

        timeStyle.fontSize = 20;
        timeStyle.normal.textColor = commonColor;
        timeStyle.alignment = TextAnchor.MiddleLeft;

        endGameStyle.fontSize = 25;
        endGameStyle.normal.textColor = commonColor;
        endGameStyle.alignment = TextAnchor.MiddleLeft;

        InitializeFruits();
    }

    void Update()
    {
        if (!gameEnded && !timerPaused)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndGame();
            }
        }

        if (timerPaused && Time.time >= timerResumeTime)
        {
            timerPaused = false;
        }

        if (bonusLocked && Time.time >= bonusUnlockTime)
            bonusLocked = false;

        if (pauseLocked && Time.time >= pauseUnlockTime)
            pauseLocked = false;

        // Eðer bir meyve seçildiyse, mýknatýs etkisini uygulayalým
        if (selectedFruit1 != null)
        {
            AttractToPoint(selectedFruit1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameEnded) return;

        if (selectedFruit1 == null)
        {
            selectedFruit1 = other.gameObject;
            Rigidbody rb = selectedFruit1.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        else if (selectedFruit2 == null && selectedFruit1 != other.gameObject)
        {
            selectedFruit2 = other.gameObject;
            Rigidbody rb = selectedFruit2.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            EvaluateMatch();
        }
    }

    // Point A noktasýna çekim kuvvetini uygular
    void AttractToPoint(GameObject fruit)
    {
        if (pointA == null) return;

        Vector3 directionToPoint = pointA.position - fruit.transform.position;
        float distance = directionToPoint.magnitude;

        if (distance > 0.5f) // Point A'ya yaklaþmaya baþlarsa çekim kuvveti uygula
        {
            // Çekim kuvveti, mesafeye orantýlý olarak azalacak
            Vector3 force = directionToPoint.normalized * magnetStrength * Mathf.Clamp01(1.0f - distance / 5f); // Kuvvet mesafeye göre deðiþiyor
            Rigidbody rb = fruit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(force, ForceMode.Force);
            }
        }
    }

    void EvaluateMatch()
    {
        if (selectedFruit1 != null && selectedFruit2 != null)
        {
            if (selectedFruit1.tag == selectedFruit2.tag)
            {
                Debug.Log("Matching fruits destroyed!");
                TriggerEffect(selectedFruit1.transform.position);
                TriggerEffect(selectedFruit2.transform.position);

                playerScore += bonusActive ? 300 : 100;
                bonusActive = false;

                Destroy(selectedFruit1, 0.5f);
                Destroy(selectedFruit2, 0.5f);
                selectedFruit1 = null;
                selectedFruit2 = null;

                totalFruitsRemaining -= 2;
            }
            else
            {
                Debug.Log("Fruits do not match. Second fruit ejected.");
                EjectFruit(selectedFruit2);
                selectedFruit2 = null;
            }
        }
    }

    void TriggerEffect(Vector3 position)
    {
        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1.5f);
        }
    }

    void EjectFruit(GameObject fruit)
    {
        Vector3 ejectDirection = (fruit.transform.position - tableCenterPoint.position).normalized;
        Rigidbody rb = fruit.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(ejectDirection * pushForce, ForceMode.Impulse);
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game over! Final score: " + playerScore);
    }

    void InitializeFruits()
    {
        totalFruitsRemaining = fruitSpawnPoints.Length;

        for (int i = 0; i < fruitSpawnPoints.Length; i++)
        {
            GameObject fruit = Instantiate(availableFruits[i % availableFruits.Length], fruitSpawnPoints[i].position, Quaternion.identity);
            fruit.tag = availableFruits[i % availableFruits.Length].tag;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 20), "Time: " + Mathf.Ceil(timeRemaining).ToString() + "s", timeStyle);
        GUI.Label(new Rect(10, 40, 200, 20), "Score: " + playerScore, scoreStyle);

        if (GUI.Button(new Rect(10, 70, 150, 30), "RESET", buttonStyle))
        {
            ReloadScene();
        }

        buttonStyle.normal.background = CreateTexture(150, 30, bonusLocked ? Color.clear : new Color(1f, 0.6f, 0.8f));
        GUI.enabled = !bonusLocked;
        if (GUI.Button(new Rect(10, 110, 150, 30), "BONUS", buttonStyle))
        {
            ActivateBonus();
            bonusLocked = true;
            bonusUnlockTime = Time.time + 5f;
        }
        GUI.enabled = true;

        buttonStyle.normal.background = CreateTexture(150, 30, pauseLocked ? Color.clear : new Color(0.8f, 0.8f, 0.5f));
        GUI.enabled = !pauseLocked;
        if (GUI.Button(new Rect(10, 150, 150, 30), "PAUSE TIMER", buttonStyle))
        {
            PauseTimer();
            pauseLocked = true;
            pauseUnlockTime = Time.time + 5f;
        }
        GUI.enabled = true;

        if (gameEnded)
        {
            GUI.Label(new Rect(10, 190, 400, 50), "Game Over!\nScore: " + playerScore, endGameStyle);
        }
    }

    void PauseTimer()
    {
        timerPaused = true;
        timerResumeTime = Time.time + 10f;
        Debug.Log("Timer paused!");
    }

    void ActivateBonus()
    {
        if (!gameEnded)
        {
            bonusActive = true;
            Debug.Log("Bonus activated! Next match scores 300 points.");
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    Texture2D CreateTexture(int width, int height, Color color)
    {
        Texture2D tex = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }
}
