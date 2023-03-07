using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    private static BoidManager _boidManager;
    public static BoidManager Instance
    {
        get { return _boidManager;}
        set { _boidManager = value; }
    }

    const int threadGroupSize = 1024;

    public BoidSetting settings;
    public ComputeShader compute;
    Boid[] boids;
    List<Boid> boidsList;
    public Transform player;
    public Transform Target;
    //Transform lastTarget;
    public bool isStop;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        boidsList = new List<Boid>();
    }
    void Start()
    {
        boids = FindObjectsOfType<Boid>();
        foreach (Boid b in boids)
        {
            b.Initialize(settings, player);
            boidsList.Add(b);
        }
        GameManager.Instance.followNum = boidsList.Count;
    }

    public void setTarget(Transform target)
    {
        if (target == null)
        {
            target = player;
        }
        foreach (Boid b in boidsList)
        {
            b.setTarget(target);
        }
    }

    void Update()
    {
        if (boidsList.Count > 0)
        {

            int numBoids = boidsList.Count;
            var boidData = new BoidData[numBoids];

            for (int i = 0; i < boidsList.Count; i++)
            {
                boidData[i].position = boidsList[i].position;
                boidData[i].direction = boidsList[i].forward;
            }

            var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
            boidBuffer.SetData(boidData);

            compute.SetBuffer(0, "boids", boidBuffer);
            compute.SetInt("numBoids", boidsList.Count);
            compute.SetFloat("viewRadius", settings.perceptionRadius);
            compute.SetFloat("avoidRadius", settings.avoidanceRadius);

            int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize);
            compute.Dispatch(0, threadGroups, 1, 1);

            boidBuffer.GetData(boidData);

            for (int i = 0; i < boidsList.Count; i++)
            {
                boidsList[i].avgFlockHeading = boidData[i].flockHeading;
                boidsList[i].centreOfFlockmates = boidData[i].flockCentre;
                boidsList[i].avgAvoidanceHeading = boidData[i].avoidanceHeading;
                boidsList[i].numPerceivedFlockmates = boidData[i].numFlockmates;

                boidsList[i].UpdateBoid(isStop);
            }

            boidBuffer.Release();
        }
    }

    public void AddBoid()
    {
        if (boidsList.Count > GameManager.Instance.ControlLimit) return;
        Boid newBoid = Spawner.Instance.ManagerSpawn();
        newBoid.Initialize(settings, player);
        boidsList.Add(newBoid);
        GameManager.Instance.followNum = boidsList.Count;
    }

    public void destroyBoid(Boid target)
    {
        boidsList.Remove(target);
        GameManager.Instance.followNum = boidsList.Count;
    }

    public struct BoidData
    {
        public Vector3 position;
        public Vector3 direction;

        public Vector3 flockHeading;
        public Vector3 flockCentre;
        public Vector3 avoidanceHeading;
        public int numFlockmates;

        public static int Size
        {
            get
            {
                return sizeof(float) * 3 * 5 + sizeof(int);
            }
        }
    }
}
