using NUnit.Framework;
using Zenject;
using AI.Prioritization;
using NSubstitute;

[TestFixture]
public class TargetPrioritizerTests : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        Container.Bind<TargetPrioritizer>();
    }

	[Test]
	public void TargetPriorityTest()
    {
		
	}
}
