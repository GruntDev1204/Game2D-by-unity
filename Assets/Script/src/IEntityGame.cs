using UnityEngine;

public interface IEntityGame
{
    GameObject Instance { get; }
    void Spawn(Vector3 position);

    void Move(Vector3 position);
}
