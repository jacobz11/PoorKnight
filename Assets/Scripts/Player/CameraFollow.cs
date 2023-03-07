
using Assets.player;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;

    void Start()
    {
        m_Player = FindObjectOfType<PlayerController>().gameObject;

        if (m_Player == null)
        {
            Debug.LogError("in CameraFollow m_Player is empty");
        }
        else
        {
            Debug.Log("CameraFollow ok");
        }
    }

    void Update()
    {
        if(m_Player != null)
            transform.position = new Vector3(m_Player.transform.position.x, 0, -5);
    }
}
