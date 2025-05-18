using UnityEngine;

public class EnemyModel : IEntityGame
{
    private readonly GameObject prefab;
    private GameObject instance;

    public EnemyModel(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject Instance => instance;

    public void Spawn(Vector3 position)
    {
        if (this.instance != null) Object.Destroy(this.instance);
        instance = Object.Instantiate(prefab, position, Quaternion.identity);
    }
}
