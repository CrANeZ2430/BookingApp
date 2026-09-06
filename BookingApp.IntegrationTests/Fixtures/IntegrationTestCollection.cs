namespace BookingApp.IntegrationTests.Fixtures;

[CollectionDefinition("IntegrationTests")]
public class IntegrationTestCollection : ICollectionFixture<CustomWebApplicationFactory>
{
    // This class has no code; it's purely used for xUnit collection mapping
}