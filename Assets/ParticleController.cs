using UnityEngine;

public class DustController : MonoBehaviour
{
    public ParticleSystem dust;
    public ParticleSystem hit;
    public ParticleSystem dash;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void CreateDust()
    {
        dust.Play();
    }

    void CreateHit()
    {
        hit.Play();
    }

    void CreateDash()
    {
        dash.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateDust();
            Debug.Log("Space key was pressed down!");
        }

        if (Input.GetKey(KeyCode.P))
        {
            CreateHit();
            Debug.Log("P key was pressed down!");
        }
        if (Input.GetKey(KeyCode.D))
        {
            CreateDash();
            Debug.Log("D key was pressed down!");
        }
    }
}