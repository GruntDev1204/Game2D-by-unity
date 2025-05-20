using UnityEngine;

public class PlayerModel : IEntityGame
{
    private readonly GameObject prefab;
    private GameObject instance;

    public PlayerModel(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject Instance => this.instance;

    public void Spawn(Vector3 position)
    {
        if (this.instance != null) Object.Destroy(this.instance);
        instance = Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public void Move(Vector3 position)
    {
        if (this.instance == null) return;
        instance.transform.position += position;
    }
}