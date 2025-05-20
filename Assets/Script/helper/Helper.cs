using UnityEngine;

public class Helper
{
    public static Vector3 ReturnNewVector3(float x, float y, float z = 0)
    {
        return new Vector3(x, y, z);
    }

    public static Vector3 ViewportToWorld(float x, float y)
    {
        return Camera.main.ViewportToWorldPoint(ReturnNewVector3(x, y, Mathf.Abs(Camera.main.transform.position.z)));
    }

    public static GameObject SpawnStaticFab(GameObject staticFab, Vector3 position)
    {
        return Object.Instantiate(staticFab, position, Quaternion.identity);
    }
}