using UnityEngine;

public class Ball : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private Trampoline trampoline;
    [SerializeField] private Vector2 velocityOfRigidBodey = new Vector2(0f, 13f);
    [SerializeField] private AudioClip[] ballSounds;
    [SerializeField] private float randFactor = 0.2f;

    //state
    private Vector2 distanceToTrampoline;
    private bool hasStarted = false;

    //Cached component references
    private AudioSource myAudioSource;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        distanceToTrampoline = transform.position - trampoline.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!hasStarted)
        {
            StopBall();
            LaunchBall();
        }
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            rigidbody2D.velocity = velocityOfRigidBodey;
        }
    }

    private void StopBall()
    {
        Vector2 trampolinePos = new Vector2(trampoline.transform.position.x, trampoline.transform.position.y);
        transform.position = trampolinePos + distanceToTrampoline;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 velocityTweak = new Vector2(Random.Range(0f,randFactor),Random.Range(0f,randFactor));

        if (hasStarted) 
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0,ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            rigidbody2D.velocity += velocityTweak;
        }
    }
}
