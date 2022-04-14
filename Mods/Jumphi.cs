public override void Update()
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

    if (Input.GetButtonDown("Fire3") && dashUnlocked)
    {
        controller.Move(transform.forward * dashDist);
    }

    if (Input.GetButtonDown("Jump"))
    {
        if (isGrounded)
        {
            velocity.y = Jump+5;
        }
        else if (jumps > 0)
        {
            velocity.y = Jump+10;
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
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    MainEdit();
}