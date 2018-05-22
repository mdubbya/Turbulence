using NUnit.Framework;
using Zenject;
using AI.Task;
using NSubstitute;
using UnityEngine;
using GameControl;
using AI.PathCalculation;
using System.Collections.Generic;

[TestFixture]
public class AttackClosestTargetInRangeTests
{
    private AttackClosestTargetInRange _attackClosestTargetInRange;
    private IRadar _radar;

    [SetUp]
    public void Install()
    {
        GameObject testObject = new GameObject();
        testObject.transform.position = new Vector3(0, 0, 0);
        _attackClosestTargetInRange = testObject.AddComponent<AttackClosestTargetInRange>();
        
        _radar = Substitute.For<IRadar>();
        
        _attackClosestTargetInRange.Inject(_radar);
    }
    
    [TestCase(1, 0, 4, 4)]
    [TestCase(2, 0, 4, 8)]
    [TestCase(1, 6, 8, 10)] 
    [TestCase(2, 6, 8, 20)]
    public void GetPriorityTest(int relativePriority, float x, float y, float expectedResult) 
    {   
        GameObject enemyTestObject = new GameObject();
        _radar.GetDetectedEnemies().Returns(new List<GameObject>(){enemyTestObject});
        _attackClosestTargetInRange.relativePriority = relativePriority;
        enemyTestObject.transform.position = new Vector3(x, 0, y);
        Assert.AreEqual(expectedResult,_attackClosestTargetInRange.GetPriority());
    }
    

    [Test]
    public void GetPriorityTestMultipleObjects()
    {
        GameObject enemyTestObject1 = new GameObject();
        GameObject enemyTestObject2 = new GameObject();

        _attackClosestTargetInRange.relativePriority = 1;
        enemyTestObject1.transform.position = new Vector3(0,0,4);
        enemyTestObject2.transform.position = new Vector3(6,0,8);
        _radar.GetDetectedEnemies().Returns(new List<GameObject>(){enemyTestObject2,enemyTestObject1});
        Assert.AreEqual(4,_attackClosestTargetInRange.GetPriority());
    }

    [Test]
    public void GetNavigationTargetTest()
    {
        GameObject enemyTestObject1 = new GameObject();
        GameObject enemyTestObject2 = new GameObject();

        _attackClosestTargetInRange.relativePriority = 1;
        enemyTestObject1.transform.position = new Vector3(0,0,4);
        enemyTestObject2.transform.position = new Vector3(6,0,8);
        _radar.GetDetectedEnemies().Returns(new List<GameObject>(){enemyTestObject2,enemyTestObject1});
        Assert.AreEqual(new Vector3(0,0,4),_attackClosestTargetInRange.GetAttackTarget());
    }

    [Test]
    public void GetNavigationTargetTestWhenEmpty()
    {
        _attackClosestTargetInRange.gameObject.transform.position = new Vector3(4,0,4);
        _attackClosestTargetInRange.relativePriority = 1;
        _radar.GetDetectedEnemies().Returns(new List<GameObject>(){});
        Assert.AreEqual(new Vector3(4,0,4),_attackClosestTargetInRange.GetNavigationTarget());
    }

    [Test]
    public void GetAttackTargetTestWhenEmpty()
    {
        _attackClosestTargetInRange.gameObject.transform.position = new Vector3(4,0,4);
        _attackClosestTargetInRange.relativePriority = 1;
        _radar.GetDetectedEnemies().Returns(new List<GameObject>(){});
        Assert.AreEqual(new Vector3(4,0,4),_attackClosestTargetInRange.GetAttackTarget());
    }

    [Test]
    public void GetCurrentPriorityTestWhenEmpty()
    {
        _attackClosestTargetInRange.gameObject.transform.position = new Vector3(4,0,4);
        _attackClosestTargetInRange.relativePriority = 1;
        _radar.GetDetectedEnemies().Returns(new List<GameObject>(){});
        Assert.AreEqual(0,_attackClosestTargetInRange.GetPriority());
    }


}
