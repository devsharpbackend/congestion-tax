namespace CongestionTax.Domain.UnitTests.AggregatesModel;

public class VehicleAggregateTest
{
    public VehicleAggregateTest()
    { }

    [Fact]
    public void VehicleName_should_be_NotNull()
    {
        string? VehicleName = null;
        //Assert
        Assert.Throws<CongestionTaxDomainException>(() => new VehicleBuilder(VehicleName).Build());
    }

  
}