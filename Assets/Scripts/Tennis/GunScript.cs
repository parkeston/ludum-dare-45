using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    Touch touch;
    public static Vector3 direction;
    public GameObject Bullet;
    public GameObject bulletSpawn;
    public int maxBalls = 10;
    private new Camera camera;
    public GameObject ballsImage;
    public Transform panel;
    private LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        for (int i = 0; i < maxBalls; i++)
        {
            GameObject objImage = Instantiate(ballsImage, panel) as GameObject;
            objImage.name = "ballsImage";
        }
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && maxBalls > 0)
        {
            touch = Input.GetTouch(0);

            if (lineRenderer.enabled == false)
            {
                lineRenderer.enabled = true;
            }

            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit))
            {
                SetupLine(hit.point);
            }


            if (touch.phase == TouchPhase.Ended && panel.childCount>0)
            {

                direction = hit.point - transform.position;
                direction.y = 0;
                direction.Normalize();
                var bullet = Instantiate(Bullet, bulletSpawn.transform.position, Quaternion.identity);
				bullet.GetComponent<BulletMove>().Gun = this;

				Destroy(panel.GetChild(0).gameObject);
            }

        }
        else lineRenderer.enabled = false;
        
    }
    void SetupLine(Vector3 hit)
    {
        lineRenderer.positionCount=2;
        lineRenderer.SetPosition(0, new Vector3(bulletSpawn.transform.position.x,bulletSpawn.transform.position.y,bulletSpawn.transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(hit.x,bulletSpawn.transform.position.y,hit.z));
        lineRenderer.startWidth=1f;
        lineRenderer.endWidth=.3f;
        lineRenderer.useWorldSpace = true;
    }
}
