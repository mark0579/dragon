using Fusion;
using Niantic.Lightship.AR.LocationAR;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;

//������ ���Ͱ� ��� �÷��̾�� ����ȭ�ϱ�
public class MonsterSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject MonsterPrefab;
    private XROrigin arSessionOrigin;

    private int activePlayers;

    void Start()
    {
        arSessionOrigin = FindObjectOfType<XROrigin>();
    }

    public void PlayerJoined(PlayerRef player)
    {
        UpdateActivePlayers();

        if (activePlayers == 1)
        {
            SpwanMonster();
        }

    }

    private void UpdateActivePlayers()
    {
        activePlayers = Runner.ActivePlayers.Count();
    }

    private void SpwanMonster()
    {
        if (arSessionOrigin == null)
        {
            Debug.LogError("ARSessionOrigin is missing");
            return;
        }

        ARLocation arLocation = FindObjectOfType<ARLocation>(); // ARLocation ������Ʈ�� ã���ϴ�.
        if (arLocation == null)
        {
            Debug.LogError("ARLocation is missing");
            return;
        }

        // ARLocation ������Ʈ�� �ڽ����� Monster�� �����մϴ�.
        Fusion.NetworkObject monster = (Fusion.NetworkObject)Runner.Spawn(MonsterPrefab, Vector3.zero, Quaternion.identity);
        if (monster != null)
        {
            // ARLocation ������Ʈ�� �ڽ����� �����մϴ�.
            monster.transform.SetParent(arLocation.transform);

            // ���ϴ� ��ġ�� �̵�
            monster.transform.localPosition = new Vector3(-0.8415996f, -1.28f, 16.15f);
        }
        else
        {
            Debug.LogError("Failed to spawn monster.");
        }
    }
}