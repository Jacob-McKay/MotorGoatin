using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour
{

    public Transform m_root;
    public GameObject m_projectilePrefab;
    public float m_power = 1f;
    public float m_lastLaunch = 0f;

    void Update()
    {

        if (Input.GetMouseButtonDown(0) && Time.time - m_lastLaunch > .2f)
        {
            m_lastLaunch = Time.time;

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.transform.position.z;
            mousePos.y = -mousePos.y;
            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);
            
            GameObject go = Instantiate(m_projectilePrefab, Camera.main.transform.position, Camera.main.transform.rotation) as GameObject;
            go.transform.parent = m_root;

            go.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * m_power);

        }

    }
}
