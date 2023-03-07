using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 velocity;
    Controller controller;
    [SerializeField] private float speed;
    [SerializeField] private float rotateLimit;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float detectRange;
    float width, height;
    public Joystick joystick;
    Transform lastTarget;
    Vector3 direction;
    //[SerializeField] Vector2 limit;
    private void Awake()
    {
        controller = new Controller();
        velocity = Vector2.zero;

        controller.Enable();
        controller.Move.Press.started += _ => StartMove();
        controller.Move.Press.canceled += _ => StopMove();
        controller.Move.Double.performed += tapCtx => findRes(tapCtx);
    }

    private void Start()
    {
        width = Screen.width;
        height = Screen.height;
    }

    private void findRes(InputAction.CallbackContext context)
    {
        if (!context.performed || GameManager.Instance.isGameOver())
            return;
        if (context.interaction is MultiTapInteraction)
        {
            Debug.Log("doubleClicked");
            if (lastTarget != null)
            {
                BoidManager.Instance.setTarget(null);
                lastTarget = null;
                Debug.Log("evoid");
                return;
            }
            Vector3 curPos = transform.position;
            Vector2 xyPos = Pointer.current.position.ReadValue();
            Vector3 des = Camera.main.ScreenToWorldPoint(new Vector3(xyPos.x, xyPos.y, 200));
            Debug.Log(des);
            //Collider[] targetInRange = Physics.OverlapSphere(transform.position, detectRange, 1 << 7);  //GameResource
            Collider[] targetInRange = Physics.OverlapSphere(des, detectRange, 1 << 7);
            if (targetInRange.Length == 0)
            {
                BoidManager.Instance.setTarget(null);
                Debug.Log("no target detected");
                return;
            }
            Transform target = null;
            float minDist = 0;
            bool fristOne = true;
            foreach (var t in targetInRange)
            {
                float dist = Vector3.Distance(t.transform.position, des);//curPos);
                Debug.Log(t.name + " with dis " + dist);
                if (fristOne)
                {
                    minDist = dist;
                    fristOne = false;
                    target = t.transform;
                    continue;
                }
                if (dist < minDist)
                {
                    minDist = dist;
                    target = t.transform;
                }
            }
            if(lastTarget == target && lastTarget != null)
            {
                BoidManager.Instance.setTarget(null);
                lastTarget = null;
                Debug.Log("evoid");
                return;
            }
            lastTarget = target;
            BoidManager.Instance.setTarget(target);
            Debug.Log("found " + target.transform.name);
        }
    }


    void StartMove()
    {
        SetJoyStickPosition();
        joystick.gameObject.GetComponent<Mask>().enabled = false;
        BoidManager.Instance.isStop = false;
    }

    void StopMove()
    {
        joystick.gameObject.GetComponent<Mask>().enabled = true;
        BoidManager.Instance.isStop = true;
    }

    void SetJoyStickPosition()
    {
        joystick.gameObject.transform.position = Pointer.current.position.ReadValue();
        

    }


    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver()) return;
        Vector2 dir = new Vector2(joystick.Horizontal, joystick.Vertical);
        velocity = dir * speed;
        Vector2 des = new Vector2(transform.position.x, transform.position.y) + velocity * Time.fixedDeltaTime;
        Vector2 limit = Camera.main.WorldToScreenPoint(des);
        if (limit.x > width - 5 || limit.y > height - 5 || limit.x < 0 + 5 || limit.y < 0 + 5) return;
        //if (des.x > limit.x || des.y > limit.y || des.x < -limit.x || des.y < -limit.y) return;
        if (dir != Vector2.zero)
            direction = new Vector3(des.x, des.y, 0) - transform.position;
        transform.up = direction.normalized;
        Vector3 rotated = transform.eulerAngles;
        //rotated.y = dir.x * rotateLimit;
        transform.eulerAngles = rotated;
        gameObject.transform.position = des;
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver())
        {
            controller.Disable();
        }
        else
        {
            controller.Enable();
        }
    }

    private void OnDisable()
    {
        controller.Disable();
    }

    private void OnEnable()
    {
        controller.Enable();
    }
}

