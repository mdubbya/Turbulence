using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public float detectionRadius;
    private TeamInfoController _teamInfoController;
    private List<Rigidbody> _enemiesDetected;

    public void Start()
    {
        _teamInfoController = GetComponent<TeamInfoController>();
    }

    public void FixedUpdate()
    {
        UpdatePotentialTargets();
    }
    
    public IList<Rigidbody> enemiesDetected
    {
        get { return _enemiesDetected.AsReadOnly(); }
    }

    private void UpdatePotentialTargets()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        List<Rigidbody> rigidBodies = (from p in nearbyColliders select p.GetComponent<Rigidbody>()).ToList();
        rigidBodies = (from p in rigidBodies where 
                        _teamInfoController.EnemyTeams.Contains(p.GetComponent<TeamInfoController>().OwningTeam)
                        select p).ToList();

        _enemiesDetected = rigidBodies;
    }
 }
