using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSync : NetworkBehaviour
{
    [Networked]
    public float Health { get; set; } = 30; // 몬스터의 초기 체력을 100으로 설정

    // 체력을 감소시키는 메서드, 다른 클라이언트에서 호출할 수 있도록 RPC로 구현
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void TakeDamageRpc(float damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        // 몬스터가 죽었을 때의 로직, 예를 들면 파괴 애니메이션 실행, 게임 오브젝트 삭제 등
        Debug.Log("Monster died.");
        gameObject.SetActive(false);
        //Runner.Despawn(Object);
    }

    // 이벤트를 시각화하기 위한 간단한 GUI 표시
    private void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 20), "Health: " + Health);

        if (Object != null && Object.IsSpawnable)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Health: " + Health);
        }
        else
        {
            GUI.Label(new Rect(10, 10, 150, 20), "Monster not spawned yet.");
        }
    }

}
