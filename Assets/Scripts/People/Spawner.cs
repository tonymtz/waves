using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private bool isSpawning;

    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private float offsetX;

    [SerializeField]
    private float digestTime = 3f;

    [SerializeField]
    private float enemyRotation;

    private float timeleft;

    private void LateUpdate()
    {
        if (!isSpawning) { return; }

        timeleft -= Time.deltaTime;

        if (timeleft <= 0)
        {
            timeleft = digestTime;
            Digest();
        }
    }

    private void Digest()
    {
        GameObject randomEnemyPrefab = enemies[Random.Range(0, enemies.Length)];
        GameObject newEnemy = Instantiate(randomEnemyPrefab);

        Vector3 newEnemyPosition = new Vector3(
            transform.position.x + offsetX,
            transform.position.y,
            transform.position.z
            );

        newEnemy.transform.position = newEnemyPosition;
        newEnemy.transform.Rotate(Vector3.up * enemyRotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
