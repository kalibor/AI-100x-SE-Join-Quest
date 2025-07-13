using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using OrderModule;

namespace OrderModule.Tests.StepDefinitions
{
    [Binding]
    public class Double11PromotionSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public Double11PromotionSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private OrderService GetOrderService()
        {
            if (!_scenarioContext.ContainsKey("OrderService"))
            {
                _scenarioContext["OrderService"] = new OrderService();
            }
            return (OrderService)_scenarioContext["OrderService"];
        }

        [Given(@"a simple Double11 test scenario")]
        public void GivenASimpleDouble11TestScenario()
        {
            // Walking skeleton - just pass
        }

        [When(@"I run the Double11 test")]
        public void WhenIRunTheDouble11Test()
        {
            // Walking skeleton - just pass
        }

        [Then(@"the Double11 test should pass")]
        public void ThenItShouldPassForDouble11()
        {
            Assert.IsTrue(true, "Double11 walking skeleton test passes");
        }

        [Given(@"Double11 promotion is enabled")]
        public void GivenDouble11PromotionIsEnabled()
        {
            // Enable Double11 promotion
            var orderService = GetOrderService();
            orderService.SetDouble11Promotion(true);
        }
    }
}
