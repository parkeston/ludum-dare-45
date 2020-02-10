using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRun : MonoBehaviour
{
    private bool direction = false;
    [SerializeField] int enemySpeedMin=2, enemySpeedMax = 5;
    private int enemySpeed;
    private int timer;
    private int testTimer;
    private int minTime = 2, maxTime = 5;
    private Animator animator;
    private int ChoiceTogo = 0;

	public static int Totalenemies { get; set; }

	private List<Collider> ragdollColliders;

	private new Rigidbody rigidbody;

	private void Awake()
	{
		Totalenemies++;

		animator = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody>();
		ragdollColliders = new List<Collider>();
		TurnOffRagdoll();
	}

	// Start is called before the first frame update
	void Start()
    {
        ChoiceTogo = Random.Range(0, 2);
        switch (ChoiceTogo)
        {
            case 0: direction = false; animator.SetFloat("Velocity", 1); break;
            case 1: direction = true; animator.SetFloat("Velocity", -1); break;
        }
        timer = (int)Time.time + Random.Range(minTime, maxTime);
        enemySpeed = Random.Range(enemySpeedMin, enemySpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Debug.Log("AnimationPlay");
            if (System.Math.Abs(animator.GetFloat("Slash")) < Mathf.Epsilon)
            {
                StartCoroutine(smoothAnimaChange(1));
                timer = (int)Time.time + Random.Range(minTime, maxTime);
            }
            else
            {
                StartCoroutine(smoothAnimaChange(0));
                timer = (int)Time.time + Random.Range(3,5);
            }
        }

		if (direction == false)
		{
			rigidbody.velocity = Vector3.left * enemySpeed;
		}
		else
			rigidbody.velocity = Vector3.right * enemySpeed;
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LeftWall")
        {
            direction = true;
            animator.SetFloat("Velocity", -1);
        }

        if (other.tag == "RightWall")
        {
        
            direction = false;
            animator.SetFloat("Velocity", 1);
        }
    }

    IEnumerator smoothAnimaChange(float value)
    {
        float startValue = animator.GetFloat("Slash");

        while(System.Math.Abs(animator.GetFloat("Slash") - value) > Mathf.Epsilon)
        {
            startValue = Mathf.MoveTowards(startValue,value,0.07f);
            animator.SetFloat("Slash", startValue);

            yield return null;
        }
    }

	private void TurnOffRagdoll()
	{
		var colliders = GetComponentsInChildren<Collider>();

		foreach(var c in colliders)
		{
			if (c.gameObject!=gameObject)
			{
				c.isTrigger = true;
				c.attachedRigidbody.useGravity = false;
				ragdollColliders.Add(c);
			}
		}
	}

	public void TurnOnRagdoll(Vector3 forceDirection)
	{

		print("turn on ragdoll!");
		
		GetComponent<Collider>().enabled = false;
		animator.enabled = false;
		rigidbody.velocity = Vector3.zero;
		rigidbody.useGravity = false;

		Totalenemies--;
		

		foreach(var c in ragdollColliders)
		{
			c.isTrigger = false;
			c.attachedRigidbody.useGravity = true;
			c.attachedRigidbody.velocity = Vector3.zero;
			c.attachedRigidbody.AddForce((forceDirection + Vector3.up*2)*7, ForceMode.Impulse);
		}

		Destroy(this);
	}
}
