using NUnit.Framework;
using Zenject;
using AI.Task;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class TaskPrioritizerTests
 
{

    TaskPrioritizer _taskPrioritizer;

    [SetUp]
    public void Install()
    {
        _taskPrioritizer = new GameObject().AddComponent<TaskPrioritizer>();
    }

	//Test higher priority is returned
    [TestCase(2,1)]
    //Test oldest priority is returned if duplicate priority
    [TestCase(2,2)]
	public void GetCurrentMovementPriorityTests(float priorityOne, float priorityTwo)
    {
        Vector3 expectedResult = new Vector3(5,0,5);
        IAIMoveTargetTask taskPriorityOne = Substitute.For<IAIMoveTargetTask>();
        IAIMoveTargetTask taskPriorityTwo = Substitute.For<IAIMoveTargetTask>();
        taskPriorityOne.GetPriority().Returns(priorityOne);
        taskPriorityTwo.GetPriority().Returns(priorityTwo);
        taskPriorityOne.GetNavigationTarget().Returns(expectedResult);
        _taskPrioritizer.Inject(new IAIMoveTargetTask[]{taskPriorityOne,taskPriorityTwo},new IAIAttackTargetTask[]{});
        Vector3 currentPriority = _taskPrioritizer.GetCurrentMovementPriority();

        Assert.AreEqual(expectedResult, currentPriority);
	}

    [Test]
    public void RemoveMoveTargetTaskTest()
    {
        Vector3 expectedResult = new Vector3(5,0,5);
        IAIMoveTargetTask taskPriorityOne = Substitute.For<IAIMoveTargetTask>();
        IAIMoveTargetTask taskPriorityTwo = Substitute.For<IAIMoveTargetTask>();
        taskPriorityOne.GetPriority().Returns(2);
        taskPriorityTwo.GetPriority().Returns(1);
        taskPriorityTwo.GetNavigationTarget().Returns(expectedResult);
        _taskPrioritizer.Inject(new IAIMoveTargetTask[]{taskPriorityOne,taskPriorityTwo},new IAIAttackTargetTask[]{});
        _taskPrioritizer.RemoveMoveTargetTask(taskPriorityOne);

        Vector3 currentPriority = _taskPrioritizer.GetCurrentMovementPriority();
        Assert.AreEqual(expectedResult, currentPriority);
    }

    [TestCase(2,1)]
    //Test oldest priority is returned if duplicate priority
    [TestCase(2,2)]
	public void GetCurrentAttackPriorityTests(float priorityOne, float priorityTwo)
    {
        Vector3 expectedResult = new Vector3(5,0,5);
        IAIAttackTargetTask taskPriorityOne = Substitute.For<IAIAttackTargetTask>();
        IAIAttackTargetTask taskPriorityTwo = Substitute.For<IAIAttackTargetTask>();
        taskPriorityOne.GetPriority().Returns(priorityOne);
        taskPriorityTwo.GetPriority().Returns(priorityTwo);
        taskPriorityOne.GetAttackTarget().Returns(expectedResult);
        _taskPrioritizer.Inject(new IAIMoveTargetTask[]{},new IAIAttackTargetTask[]{taskPriorityOne,taskPriorityTwo});
        Vector3 currentPriority = _taskPrioritizer.GetCurrentAttackPriority();

        Assert.AreEqual(expectedResult, currentPriority);
	}

    [Test]
    public void RemoveAttackTargetTaskTest()
    {
        Vector3 expectedResult = new Vector3(5,0,5);
        IAIAttackTargetTask taskPriorityOne = Substitute.For<IAIAttackTargetTask>();
        IAIAttackTargetTask taskPriorityTwo = Substitute.For<IAIAttackTargetTask>();
        taskPriorityOne.GetPriority().Returns(2);
        taskPriorityTwo.GetPriority().Returns(1);
        taskPriorityTwo.GetAttackTarget().Returns(expectedResult);
        _taskPrioritizer.Inject(new IAIMoveTargetTask[]{},new IAIAttackTargetTask[]{taskPriorityOne,taskPriorityTwo});
        _taskPrioritizer.RemoveAttackTargetTask(taskPriorityOne);

        Vector3 currentPriority = _taskPrioritizer.GetCurrentAttackPriority();
        Assert.AreEqual(expectedResult, currentPriority);
    }

    [Test]
    public void GetCurrentMovementPriorityTestWhenEmpty()
    {
        Vector3 expectedResult = new Vector3(5,0,5);
        _taskPrioritizer.gameObject.transform.position = expectedResult;
        IAIAttackTargetTask taskPriorityOne = Substitute.For<IAIAttackTargetTask>();
        IAIAttackTargetTask taskPriorityTwo = Substitute.For<IAIAttackTargetTask>();
        taskPriorityOne.GetPriority().Returns(2);
        taskPriorityTwo.GetPriority().Returns(1);
        taskPriorityTwo.GetAttackTarget().Returns(new Vector3(10,0,10));
        _taskPrioritizer.Inject(new IAIMoveTargetTask[]{},new IAIAttackTargetTask[]{taskPriorityOne,taskPriorityTwo});
        Vector3 currentPriority = _taskPrioritizer.GetCurrentMovementPriority();

        Assert.AreEqual(expectedResult, currentPriority);
    }

    [Test]
    public void GetCurrentAttackPriorityTestWhenEmpty()
    {
        Vector3 expectedResult = new Vector3(5,0,5);
        _taskPrioritizer.gameObject.transform.position = expectedResult;
        IAIMoveTargetTask taskPriorityOne = Substitute.For<IAIMoveTargetTask>();
        IAIMoveTargetTask taskPriorityTwo = Substitute.For<IAIMoveTargetTask>();
        taskPriorityOne.GetPriority().Returns(2);
        taskPriorityTwo.GetPriority().Returns(1);
        taskPriorityTwo.GetNavigationTarget().Returns(new Vector3(10,0,10));
        _taskPrioritizer.Inject(new IAIMoveTargetTask[]{taskPriorityOne,taskPriorityTwo},new IAIAttackTargetTask[]{});
        Vector3 currentPriority = _taskPrioritizer.GetCurrentAttackPriority();

        Assert.AreEqual(expectedResult, currentPriority);
    }


}
