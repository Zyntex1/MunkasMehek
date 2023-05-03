using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance { get; private set; }

    public CharacterController controller;
    public MouseLook mouseLook;

    float speed = 4.0f;
    float gravity = -9.81f;
    float jumpHeight = 1.0f;

    [SerializeField] Transform groundCheck;

    Vector3 velocity;
    bool isGrounded;

    public GameObject harvestingTool;
    public Bag bag;

    [SerializeField] Text pollenText;
    [SerializeField] Text honeyText;

    [SerializeField] GameObject eggPanel;
    [SerializeField] GameObject beeInfoPanel;
    [SerializeField] GameObject questPanel;
    [SerializeField] GameObject sidePanel;

    public int pollen;
    public int honey;

    public int royalJellies;
    public int cookies;

    public QuestGroup questGroup;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Movement();

        UpdateUI();

        UpdateQuestData();
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, ~(1 << 3), QueryTriggerInteraction.Ignore);

        if (isGrounded && velocity.y < 0.0f)
            velocity.y = -2.0f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsOnField()
    {
        return Physics.CheckSphere(groundCheck.position, 0.4f, 1 << 10);
    }

    public FieldType GetFieldType()
    {
        return Physics.OverlapSphere(groundCheck.position, 0.4f, 1 << 10)[0].GetComponent<Field>().fieldType;
    }

    void UpdateUI()
    {
        pollenText.text = $"Pollen: {pollen}/{bag.capacity}";
        honeyText.text = $"Méz: {honey}";

        if (eggPanel.active || beeInfoPanel.active || questPanel.active)
            return;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            sidePanel.SetActive(!sidePanel.active);
            mouseLook.SetCursorLockState(!sidePanel.active);
        }
    }

    public bool HasQuest()
    {
        if (questGroup != null)
            return (questGroup.quests.Count > 0);
        else
            return false;
    }

    void UpdateQuestData()
    {
        if (!HasQuest())
            return;

        bool allReached = true;

        foreach (Quest quest in questGroup.quests)
        {
            if (!quest.goal.IsReached())
                allReached = false;
        }

        if (allReached)
        {
            honey += questGroup.reward;

            if (questGroup.next != null)
                questGroup.next.isActive = true;

            questGroup = null;
        }
    }
}
