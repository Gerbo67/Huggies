using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChanger : MonoBehaviour
{
    public Sprite jumpSPrite;
    public Sprite idleSprite;

    private Rigidbody2D _leaderrb;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        GameObject leader = GameObject.FindGameObjectWithTag("LeaderTag");
        if (leader != null)
        {
            _leaderrb = leader.GetComponent<Rigidbody2D>();
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpriteBasedOnMovement();
    }

    void UpdateSpriteBasedOnMovement()
    {
        if (_leaderrb.velocity.y < 0)
        {
            _spriteRenderer.sprite = jumpSPrite;
        }
        else if (_leaderrb.velocity.y >= 0)
        {
            _spriteRenderer.sprite = idleSprite;
        }
    }
}
