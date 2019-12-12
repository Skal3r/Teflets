using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 20f;
    private Vector3 moveVelocity;
    Rigidbody rb;
    public float atk_spd = 1.5f;
    public GameObject bulletObject;
    public GameObject bulletSpawnPoint;
    private float atk_cooldown = 0;

    [SerializeField]
    private int currentHealth = 100;

    public AudioClip walkSound;
    public AudioClip hurtSound;
    private AudioSource Audio_source;
    public float stepTime = 0.7f;
    private float time_since_last_step = 0.0f;

    public int MaxHealthPoints = 100;
    private float damageTime = 0.5f;
    private float MaxDamageTime = 0.5f;
    private bool CanTakeDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Audio_source = GetComponent<AudioSource>();
    }


    void FaceMouse()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;



        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 15f * Time.deltaTime);
        }

    }

    public void DoDamage(int amount)
    {
        if (CanTakeDamage)
        {
            GetComponent<HealthBehavior>().DoDamage(amount);
            CanTakeDamage = false;
            Audio_source.PlayOneShot(hurtSound, 0.7f);
        }
    }

    private void DamageStatus()
    {
        if (!CanTakeDamage)
        {
            damageTime -= Time.deltaTime;

        }
        if (damageTime < 0)
        {
            CanTakeDamage = true;
            damageTime = MaxDamageTime;
        }

    }

    void MovePlayer()
    {
        time_since_last_step += Time.deltaTime;
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (time_since_last_step > stepTime)
            {
                Audio_source.PlayOneShot(walkSound, 0.7f);
                time_since_last_step = 0;
            }
        }

        moveVelocity = moveInput * playerSpeed;
    }
    void Attack()
    {
        atk_cooldown += Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            if (atk_cooldown > 1 / atk_spd)//magic number I know, but it works
            {
                Instantiate(bulletObject, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation).GetComponent<BulletBehavior>().SetPlayerReference(this.gameObject);
                atk_cooldown = 0;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {

        MovePlayer();
        FaceMouse();
        Attack();
        DamageStatus();
        if (CanTakeDamage)
        {
            this.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        else
        {
            this.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }
    }



    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
        
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene("MenuScene");
    }



}
