using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    BoidSetting settings;
    public Vector2 position;

    public Vector2 forward;
    Vector2 velocity;

    Vector2 acceleration;   //º”ÀŸ∂»

    public Vector2 avgFlockHeading;

    public Vector2 avgAvoidanceHeading;

    public Vector2 centreOfFlockmates;

    public int numPerceivedFlockmates;


    Material material;
    Transform cachedTransform;
    Transform target;
    Transform parentTarget;

    private void Awake()
    {
        material = transform.GetComponent<MeshRenderer>().material;
        cachedTransform = transform;
    }

    public void SetColour(Color col)
    {
        if (material != null)
        {
            material.color = col;
        }
    }

    public void Initialize (BoidSetting settings, Transform target)
    {
        this.target = target;
        parentTarget = target;
        this.settings = settings;

        position = cachedTransform.position;
        forward = cachedTransform.forward;

        //float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2;
        float startSpeed = settings.minSpeed;
        velocity = transform.forward * startSpeed;
    }

    public void UpdateBoid(bool isStop)
    {
        Vector2 acceleration = Vector2.zero;
        Vector2 targetPos = new Vector2();
        if (target != null)
        {
            targetPos = new Vector2(target.position.x, target.position.y);
            Vector2 offsetToTarget = (targetPos - position);
            acceleration = SteerTowards(offsetToTarget) * settings.targetWeight;
        }

        if(numPerceivedFlockmates != 0)
        {
            centreOfFlockmates /= numPerceivedFlockmates;

            Vector2 offsetToFlockmateCentre = (centreOfFlockmates - position);

            var alignmentForce = SteerTowards(avgFlockHeading) * settings.alignWeight;
            var cohesionForce = SteerTowards(offsetToFlockmateCentre) * settings.cohesionWeight;
            var seperationForce = SteerTowards(avgAvoidanceHeading) * settings.seperateWeight;

            acceleration += alignmentForce;
            acceleration += cohesionForce;
            acceleration += seperationForce;
        }
        if (IsHeadingForCollision())
        {
            Vector2 collisionAvoidDir = ObstacleRays();
            Vector2 collisionAvoidForce = SteerTowards(collisionAvoidDir) * settings.avoidCollisionWeight;
            acceleration += collisionAvoidForce;
        }
        
        velocity += acceleration * Time.deltaTime;
        float speed = velocity.magnitude;
        Vector2 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;

        //cachedTransform.position += velocity * Time.deltaTime;
        float dis = Vector2.Distance(targetPos, position);
        if (dis < 5 || (isStop && target.name == parentTarget.name) || (dis < 10 && target.name == parentTarget.name))
        { }
        else
        {
            if(dis > 200)
            {
                Destroy();
            }
            if(dis > 150)
            {
                //target = parentTarget;
            }
            Vector2 transformPos2D = cachedTransform.position;
            cachedTransform.position = transformPos2D + (velocity * Time.deltaTime);
        }
        cachedTransform.forward = dir;
        position = cachedTransform.position;
        forward = dir;
    }

    bool IsHeadingForCollision()
    {
        RaycastHit hit;
        if (Physics.SphereCast(position, settings.boundsRadius, forward, out hit, settings.collisionAvoidDst, settings.obstacleMask))
        {
            return true;
        } else { }
        return false;
    }

    Vector2 ObstacleRays()
    {
        Vector2[] rayDirections = BoidHelper.directions;

        for (int i = 0; i < rayDirections.Length; i++)
        {
            Vector2 dir = cachedTransform.TransformDirection(rayDirections[i]);
            Ray ray = new Ray(position, dir);
            if (!Physics.SphereCast(ray, settings.boundsRadius, settings.collisionAvoidDst, settings.obstacleMask))
            {
                return dir;
            }
        }

        return forward;
    }

    Vector2 SteerTowards (Vector2 vector)
    {
        //Vector3 velocity2D = new Vector3 (velocity.x, velocity.y);
        Vector2 v = vector.normalized * settings.maxSpeed - velocity;
        return Vector2.ClampMagnitude(v, settings.maxSteerForce);
    }

    public void Destroy()
    {
        BoidManager.Instance.destroyBoid(this);
        if(target.name != parentTarget.name)
        {
            Collider[] res = Physics.OverlapSphere(transform.position, 30, 1 << 7);
            foreach(Collider r in res)
            {
                r.gameObject.SendMessage("BoidDestroy", this);
            }
        }
        Destroy(gameObject);
    }

    public void setTarget(Transform target)
    {
        if (target == null)
        {
            this.target = parentTarget;
            return;
        }
        this.target = target;
    }
}
