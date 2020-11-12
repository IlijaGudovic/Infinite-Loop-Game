using UnityEngine;

public class shieldMovment : MonoBehaviour
{

    private Rigidbody2D rb;

    public float speed = 10;

    public float waitTime = 0.4f;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        newMove();
    }


    public void MoveTowards(Vector2 nesPosition)
    {

        Vector2 direction = new Vector2(nesPosition.x - transform.position.x, nesPosition.y - transform.position.y);

        float distance = Vector3.Distance(nesPosition, transform.position);

        transform.right = direction;
        rb.velocity = transform.right * speed ;

        float calculateTimeForStop = distance / speed;

        Invoke("stopMove", calculateTimeForStop);

    }

    public void stopMove()
    {

        rb.velocity = Vector2.zero;

        Invoke("newMove", waitTime);

    }

    private void newMove()
    {

        int randomInt = Random.Range(20, 40);

        Vector2 randomVector = new Vector2(-transform.position.x, transform.position.y - (float)randomInt / 10);

        MoveTowards(randomVector);

    }

}
