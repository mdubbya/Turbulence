using System.Collections.Generic;
using UnityEngine;

public class TeamInfoController : MonoBehaviour
{
    public enum TeamNumber { team1,team2,team3,team4,neutralPassive,neutralHostile};

    public TeamNumber OwningTeam;
    public List<TeamNumber> EnemyTeams;
    public List<TeamNumber> AlliedTeams;
}

