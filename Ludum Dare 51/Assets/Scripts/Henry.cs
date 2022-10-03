using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Henry : MonoBehaviour
{
    public ParticleSystem deathParticles;
    public GameObject dedPanel;
    // Start is called before the first frame update
    void Awake()
    {
        dedPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Instantiate(deathParticles, transform.position, Quaternion.Euler(-90, 0, 0));
        gameObject.SetActive(false);
    }
        
}
