using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    Rigidbody rb;
    public int bulletSpeed = 100;

	public GunScript Gun { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = GunScript.direction * bulletSpeed;
    }

	private void OnCollisionEnter(Collision other)
	{
		if (!rb.useGravity)
		{
			if (other.gameObject.CompareTag("Enemy"))
			{

				if (other.gameObject.GetComponent<Animator>().GetFloat("Slash") < 0.5f)
				{
					print("hit!");

					other.gameObject.GetComponent<EnemyRun>().TurnOnRagdoll((other.transform.position-transform.position).normalized);
				}
				else
					rb.velocity = (transform.position - other.transform.position).normalized * bulletSpeed;
			}

			rb.useGravity = true;

			Gun.maxBalls--;

			if (Gun.maxBalls == 0 && EnemyRun.Totalenemies != 0)
			{
				LevelsManager.Instance.RestartLevel(1);
				Destroy(this);
			}
			if (EnemyRun.Totalenemies == 0)
			{
				LevelsManager.Instance.ToNextLevel(1);
				Destroy(this);
			}

		}
	}

    private void OnBecameInvisible()
    {
		if(!rb.useGravity)
		{
			Gun.maxBalls--;

			if (Gun.maxBalls == 0 && EnemyRun.Totalenemies != 0)
			{
				LevelsManager.Instance.RestartLevel(1);
				Destroy(this);
			}
			if (EnemyRun.Totalenemies == 0)
			{
				LevelsManager.Instance.ToNextLevel(1);
				Destroy(this);
			}
		}

		Destroy(gameObject);
    }
}
