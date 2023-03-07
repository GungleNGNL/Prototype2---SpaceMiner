using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static Spawner _spawner;
    public static Spawner Instance
    {
        get { return _spawner; }
        set { _spawner = value; }
    }
    public enum GizmoType { Never, SelectedOnly, Always }

    public Boid prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public Color colour;
    public GizmoType showSpawnRegion;

    void Awake()
    {
        if(Instance == null)
        Instance = this;
        for (int i = 0; i < spawnCount; i++)
        {
            spawn();
        }
    }

    void spawn()
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
        Boid boid = Instantiate(prefab);
        boid.transform.position = pos;
        boid.transform.forward = Random.insideUnitSphere;
        boid.SetColour(colour);
    }

    public Boid ManagerSpawn()
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
        Boid boid = Instantiate(prefab);
        boid.transform.position = pos;
        boid.transform.forward = Random.insideUnitSphere;
        boid.SetColour(colour);
        return boid;
    }

    private void OnDrawGizmos()
    {
        if (showSpawnRegion == GizmoType.Always)
        {
            DrawGizmos();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showSpawnRegion == GizmoType.SelectedOnly)
        {
            DrawGizmos();
        }
    }

    void DrawGizmos()
    {
        Gizmos.color = new Color(colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere(transform.position, spawnRadius);
    }
}
