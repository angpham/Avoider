using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 startingPoint;
   
    public float speed = 0.1f;

    public float doubleClickTimeMax = 0.5f;
    private float doubleClickTimeCur = 0f;

    public bool isDashing = false;
    public int dashModAmount = 2;
    public float dashTimeMax = 1.5f;
    private float dashTimeCur = 0f;

    public bool isCaught = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingPoint = new Vector3(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Move Towards the Clicked Location Until Reached
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CountClicks(mouseInSpace));
        }

        if (isCaught == true)
        {
            StopAllCoroutines();
            transform.position = startingPoint;
            isCaught = false;
        }
    }

    IEnumerator MoveTo(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator CountClicks(Vector3 mouseInSpace)
    {
        yield return new WaitForEndOfFrame();

        doubleClickTimeCur = 0f;
        while (doubleClickTimeCur < doubleClickTimeMax)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDashing = true;
                StopAllCoroutines();
                Debug.Log("Dash Speed: " + (speed * dashModAmount));
                StartCoroutine(MoveTo(transform.position, mouseInSpace, speed * dashModAmount));
                yield break;
            }
            doubleClickTimeCur += Time.fixedDeltaTime;
            yield return null;
        }

        StopAllCoroutines();
        StartCoroutine(MoveTo(transform.position, mouseInSpace, speed));

        if (isDashing && dashTimeCur < dashTimeMax)
        {
            dashTimeCur += Time.fixedDeltaTime;
        }
        else
        {
            dashTimeCur = 0f;
            isDashing = false;
        }
    } 
}
