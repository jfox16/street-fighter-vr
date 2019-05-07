
using UnityEngine;

public class UCFighterInput : FighterInput
{
    // Start is called before the first frame update

    // Update is called once per frame
    public GameObject FireballParticle;
    public Transform spawnLocation;
    private float timestamp;
    private float cooldown = .5f;

    private void Awake()
    {
    }
    public override void kick()
    {
        animator.SetTrigger("Kick");
    }

    public override void punch()
    {
        animator.SetTrigger("Light_Punch");
    }

    public override void special()
    {
        if (fighter.special >= 25 && timestamp < Time.time)
        {
            fighter.special -= 1;
            timestamp = Time.time + cooldown;
            animator.SetTrigger("Special");
        }
    }
    public void spawnFireball()
    {
        Camera _camera;
        GameObject controller = GameObject.Find("Game Controller");
        GameController g = controller.GetComponent<GameController>();
        _camera = g.getCamera();
        GameObject _ball;
        timestamp = Time.time + cooldown;
        _ball = Instantiate(FireballParticle);
        _ball.transform.position = spawnLocation.position;
        _ball.transform.rotation = spawnLocation.rotation;
        _ball.GetComponent<Attack>().ownerID = gameObject.GetInstanceID();
        _ball.GetComponent<Attack>().owner = gameObject;
    }
}
