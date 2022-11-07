using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardController : MonoBehaviour
{
    [SerializeField] public float time_to_recharge = 0.7f;
    float recharge_cooldown = 0;
    [SerializeField] public float time_between_restore = 0.3f;
    float restore_cooldown = 0;
    [SerializeField] float time_between_consume = 0.3f;
    float consume_cooldown;
    [SerializeField] public int max_shards = 3;
    public int available_shards { get; private set; }
    public int recharge_queue { get; private set; } = 0;
    public int restore_queue { get; private set; } = 0;
    [SerializeField] GameObject canvas;
    private ShardIndicator shard_indicator;
    private NewPlayerMovement p_mov;

    // Start is called before the first frame update
    void Start()
    {
        p_mov = GetComponent<NewPlayerMovement>();

        available_shards = max_shards;

        shard_indicator = canvas.GetComponent<ShardIndicator>();
        shard_indicator.Init(this);
    }

    public bool isShardAvailable()
    {
        return available_shards > 0;
    }

    //Consume (consume cooldown) -> Shard goes into recharge queue
    public void consumeShard()
    {
        if (available_shards > 0 && consume_cooldown <= 0)
        {
            available_shards--;
            if (recharge_queue == 0)
            {
                recharge_cooldown = time_to_recharge;
            }
            recharge_queue++;

            consume_cooldown = time_between_consume;
            shard_indicator.consumeShard();
        }
    }
    //Shard recharges recharging (time to recharge) -> Shard goes into restore queue
    void rechargeShard()
    {
        if (recharge_queue > 0 && recharge_cooldown <= 0)
        {
            recharge_queue--;
            if (restore_queue == 0)
            {
                restore_cooldown = time_between_restore;
            }
            restore_queue++;

            recharge_cooldown = time_to_recharge;
        }
    }
    //Shard is restored (restore cooldown) ->Shard goes into available shards
    void restoreShard()
    {
        if (restore_queue > 0 && restore_cooldown <= 0 && p_mov.isOnGround)
        {
            restore_queue--;
            available_shards++;

            restore_cooldown = time_between_restore;
            shard_indicator.restoreShard();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (consume_cooldown > 0)
        {
            consume_cooldown -= Time.deltaTime;
        }
        if (recharge_cooldown > 0)
        {
            recharge_cooldown -= Time.deltaTime;
        }
        if (restore_cooldown > 0)
        {
            restore_cooldown -= Time.deltaTime;
        }
        rechargeShard();
        restoreShard();
    }
}
