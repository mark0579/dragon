using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSync : NetworkBehaviour
{
    [Networked]
    public float Health { get; set; } = 30; // ������ �ʱ� ü���� 100���� ����

    // ü���� ���ҽ�Ű�� �޼���, �ٸ� Ŭ���̾�Ʈ���� ȣ���� �� �ֵ��� RPC�� ����
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
        // ���Ͱ� �׾��� ���� ����, ���� ��� �ı� �ִϸ��̼� ����, ���� ������Ʈ ���� ��
        Debug.Log("Monster died.");
        gameObject.SetActive(false);
        //Runner.Despawn(Object);
    }

    // �̺�Ʈ�� �ð�ȭ�ϱ� ���� ������ GUI ǥ��
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
