using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public enum Status
    {
        Ready,
        Fail,
        Success
    }
    public Status status;
    public bool touchball;
    public bool success;
    Rigidbody rb;
    LineRenderer line;
    public float rotatespeed;
    public float shotpower;
    public int combo;
    float xrot;
    float yrot;
    GameMaster gamemaster;
    Vector3 startpos;
    public bool startaccess;
    // Start is called before the first frame update
    void Start()
    {
        gamemaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        startaccess = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        status = Status.Ready;
        startpos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        startaccess = gamemaster.start;
        if (startaccess)
        {
            MouseWork();
        }
    }
    void MouseWork()
    {
        if (Input.GetMouseButton(0) && touchball)
        {
            xrot -= Input.GetAxis("Mouse X") * rotatespeed;
            yrot += Input.GetAxis("Mouse Y") * rotatespeed;
            transform.rotation = Quaternion.Euler(yrot, xrot, 0);
            line.enabled = true;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, transform.position + transform.forward * 4);
            if (yrot < -50)
            {
                yrot = -50;
            }
        }
        if (Input.GetMouseButtonUp(0) && touchball)
        {
            Shooting();
        }
    }
    void Shooting()
    {
        rb.velocity = transform.forward * shotpower;
        line.enabled = false;
        touchball = false;
        rb.useGravity = true;
        StartCoroutine(StatusRefresh());
    }
    private void OnMouseDown()
    {
        touchball = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "hole")
        {
            status = Status.Success;
            success = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="ground" && success==false)
        {
            status = Status.Fail;
        }
    }
    IEnumerator StatusRefresh()
    {
        yield return new WaitForSeconds(4.0f);
        gamemaster.shotcount++;
        if (status==Status.Fail)
        {
            combo = 0;
        }
        if (status == Status.Success)
        {
            gamemaster.successshot++;
            combo++;
            gamemaster.currentscore += combo;
        }
        status = Status.Ready;
        transform.position = startpos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        success = false;
        line.SetPosition(0, new Vector3(0, 0, 0));
        line.SetPosition(1, new Vector3(0, 0, 1));
    }
}