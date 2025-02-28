using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    [SerializeField] public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (!player)
        {
            GameObject[] tmp = GameObject.FindGameObjectsWithTag("Player");
            player = tmp[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);
    }
}
