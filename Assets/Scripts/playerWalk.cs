using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerWalk : MonoBehaviour
{
    public Transform self;

    public CharacterController controller;

    public float Speed = 12f;
    public float gravity = -9.81f;
    public float Jump = 3f;

    public Vector3 velocity;

    public Transform groundCheck;
    public float checkDist = 0.3f;
    public LayerMask GroundMask;
    bool isGrounded;
    public Transform grapple;
    public float grappleDist = 100f;
    public float grappleOffDist = 0.1f;
    bool grapplecheck;
    public bool grappleUnlocked = true;
    Vector3 grappledir;
    bool grappling;
    public float grappleSpeed;
    RaycastHit grappleHit;

    public bool dashUnlocked = true;
    public float dashDist = 5f;

    public int jumps = 0;
    public int maxJumps = 0;

    public float[] maxHealth = new float[10] {25f, 100f, 10f, 5f, 10f, 5f, 10f, 5f, 10f, 5f};
    public float maxHealthTEMP = 44f;
    public float healthTEMP = 44f;
    public float maxMana = 100f;
    public float mana = 100f;
    public float maxStam = 100f;
    public float stam = 100f;

    public bool canMove = true;

    public Shop shopTEMP;
    public string slot;

    public bool dead = false;

    private void OnDisable()
    {
        if (!dead)
        {
            Save Save = new Save();

            Save.Set();

            string json = JsonUtility.ToJson(Save);
            Debug.Log(json);
            File.WriteAllText(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json", json);
        }
    }
    void OnEnable()
    {
        slot = PlayerPrefs.GetString("Slot");
    }
    public virtual void Start()
    {
        if (File.Exists(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json"))
        {
            Debug.Log(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json");
            string sav = File.ReadAllText(Application.dataPath.Replace("/Assets", "") + "/save" + slot + ".json");
            Save save = JsonUtility.FromJson<Save>(sav);
            controller.enabled = false;
            transform.position = save.playerPos;
            controller.enabled = true;
            maxMana = save.playerMaxMana;
            velocity = save.playerVel;
            jumps = save.playerJumps;
            maxHealthTEMP = save.playerMaxHealth;
            maxJumps = save.playerMaxJumps;
            mana = save.playerMana;
            healthTEMP = save.playerHealth;
            dashUnlocked = save.playerDash;
            transform.GetComponent<Inventory>().inventoryItems = save.playerInventory;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (canMove)
        {
            Walk();
        }
    }
    void Walk()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkDist, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (isGrounded)
        {
            jumps = maxJumps;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * Speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire3") && dashUnlocked && mana >= 15)
        {
            controller.Move(transform.forward * dashDist);
            mana -= 15;
        }

        if (Input.GetKeyDown("h"))
        {
            shopTEMP.open();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                velocity.y = Jump;
            }
            else if (jumps > 0)
            {
                velocity.y = Jump;
                jumps -= 1;
            }
        }

        Ray ray = new Ray(grapple.position, grapple.forward);
        grapplecheck = Physics.Raycast(ray, out grappleHit, grappleDist, GroundMask);

        if (Input.GetButtonDown("Fire1") && grapplecheck && grappleUnlocked)
        {
            grappling = true;
        }

        if (grappling && Mathf.Abs((grappleHit.point - transform.position).magnitude) < grappleOffDist)
        {
            grappling = false;
        }

        if (grappling)
        {
            controller.Move(self.InverseTransformDirection((grappleHit.point - transform.position).normalized * grappleSpeed * Time.deltaTime));
        }

        velocity.y += gravity * Time.deltaTime;

        if (grappling)
        {
            velocity.y = 0f;
        }

        controller.Move(velocity * Time.deltaTime);

        if (healthTEMP <= 0)
        {
            healthTEMP = maxHealthTEMP;
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
            dead = true;
        }
        MainEdit();
    }

    public virtual void OnTriggerStay(Collider other)
    {
        Damage DMG = other.GetComponent<Damage>();
        if (DMG != null)
        {
            healthTEMP -= DMG.DMG;
        }
    }

    public virtual void MainEdit()
    {

    }
}
