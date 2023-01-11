namespace EventBus.Tests
{
    public class InMemory_SubscriptionManager_Tests
    {
        [Test]
        public void After_Creation_Should_Be_Empty()
        {
            InMemoryEventBusSubscriptionsManager manager = new();
            Assert.That(manager.IsEmpty, Is.True);
        }

        [Test]
        public void After_One_Event_Subscription_Should_Contain_The_Event()
        {
            InMemoryEventBusSubscriptionsManager manager = new();
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            Assert.That(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
        }

        [Test]
        public void After_All_Subscriptions_Are_Deleted_Event_Should_No_Longer_Exists()
        {
            InMemoryEventBusSubscriptionsManager manager = new();
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            Assert.That(manager.HasSubscriptionsForEvent<TestIntegrationEvent>(), Is.False);
        }

        [Test]
        public void Deleting_Last_Subscription_Should_Raise_On_Deleted_Event()
        {
            bool raised = false;
            InMemoryEventBusSubscriptionsManager manager = new();
            manager.OnEventRemoved += (o, e) => raised = true;
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            Assert.That(raised, Is.True);
        }

        [Test]
        public void Get_Handlers_For_Event_Should_Return_All_Handlers()
        {
            InMemoryEventBusSubscriptionsManager manager = new();
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
            manager.AddSubscription<TestIntegrationEvent, TestIntegrationOtherEventHandler>();
            IEnumerable<SubscriptionInfo> handlers = manager.GetHandlersForEvent<TestIntegrationEvent>();
            Assert.That(handlers.Count(), Is.EqualTo(2));
        }

    }
}
