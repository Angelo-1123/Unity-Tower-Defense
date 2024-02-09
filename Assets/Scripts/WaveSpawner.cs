using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public enum SpawnState {SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int[] count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public float countdown = 20f;
    public int newGamePlus = 0;

    public TextMeshProUGUI waveCountdownText;
    public TextMeshProUGUI roundsText;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        newGamePlus = 0;
    }

    void Update ()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (countdown <= 0f)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
                countdown = timeBetweenWaves;
            }
        }
        
        if (state == SpawnState.COUNTING)
        {
            countdown -= Time.deltaTime;

            countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

            waveCountdownText.text = string.Format("{0:00.00}", countdown);
        }
    }

    void WaveCompleted ()
    {
        Debug.Log("Wave Cleared!");

        state = SpawnState.COUNTING;
        countdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = -1;
            newGamePlus++;
            Debug.Log ("All waves complete, Looping!");
        }

        nextWave++;
    }

    bool EnemyIsAlive ()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if(GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        PlayerStats.Rounds++;

        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        roundsText.text = "Round " + PlayerStats.Rounds + ": " + _wave.name;

        for(int j = 0; j < _wave.enemy.Length; j++)
        {
            for(int i = 0; i < (_wave.count[j] + newGamePlus); i++)
            {
                SpawnEnemy(_wave.enemy[j]);
                yield return new WaitForSeconds(1f/_wave.rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }
    
    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);

        Enemy e = _enemy.GetComponent<Enemy>();
        e.startHealth = e.baseHealth * (1 + newGamePlus*3/2);
        e.worth = (float)(e.baseWorth * (1 + newGamePlus));

        if(e.baseProwl > 0)
        {
            e.startProwl = (int)(e.baseProwl * (1 + newGamePlus/2));
        }
        if(e.Crumbling)
        {
            e.startArmor = (int)(e.baseArmor * (1 + newGamePlus/2));
        }
        else
        {
            if(e.startArmor < 100)
            {
                e.startArmor = (int)e.baseArmor + (2 * newGamePlus);
            }
        }

    }
}
