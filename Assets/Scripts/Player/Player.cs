using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerObject
{

    private Rigidbody2D rigid;    

    // Start is called before the first frame update
    protected override void PlayerStart()
    {
        rigid = GetComponent<Rigidbody2D>();
        baseSpeed = 4;
    }

    // Update is called once per frame
    protected override void PlayerUpdate()
    { }

    /// <summary>
    /// Call OnMove to calculate player moviment.
    /// </summary>
    private void FixedUpdate()
    {
        OnMove();
    }

    #region Moviment
    /// <summary>
    /// Calculate player moviment.
    /// </summary>
    void OnMove()
    {
        rigid.MovePosition(rigid.position + direction * currentSpeed * Time.fixedDeltaTime);
    }

    #endregion
    /// <summary>
    /// Get initial player state. By default player init idle.
    /// </summary>
    /// <returns></returns>
    protected override UnitState getFirstState()
    {
        return new PlayerIdle();
    }
}
