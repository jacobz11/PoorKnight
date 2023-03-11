
using Assets.player;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Player;
    private Vector3 updateingPosition;

    void Start()
    {
        updateingPosition = transform.position;
        m_Player = FindObjectOfType<PlayerController>().gameObject;
        
        if (m_Player == null)
        {
            Debug.LogError("in CameraFollow m_Player is empty");
        }
    }

    void Update()
    {
        if (m_Player == null)
        { 
            this.enabled = true;
        }
        else
        {
            updateingPosition.x = m_Player.transform.position.x;
            transform.position = updateingPosition;
        }
    }
}
