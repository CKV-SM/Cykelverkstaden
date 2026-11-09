using Business.Models;
using Business.Services;
using Tests.Data;

namespace Tests;

public class WorkshopServiceTests
{
    [Fact]
    public void ValidDescription_Should_Return_Workorder() {
        //ARRANGE
        IWorkorderRepository dataRepo = new MemoryWorkorderRepository();
        var SUT = new WorkshopService(dataRepo);

        //ACT
        WorkOrder returnOrder = SUT.CreateWorkOrder("Fixa bromsar");
        
        //ASSERT
        Assert.Equal("Fixa bromsar", returnOrder.Description);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void EmptyDescription_Should_ThrowException(string description) {
        //ARRANGE
        IWorkorderRepository dataRepo = new MemoryWorkorderRepository();
        var SUT = new WorkshopService(dataRepo);

        //ACT && ASSERT
        Assert.Throws<ArgumentException>(() => SUT.CreateWorkOrder(description));
    }

    [Fact]
    public void SavedWorkorder_ShouldBe_Retrievable() {

        //ARRANGE
        IWorkorderRepository dataRepo = new MemoryWorkorderRepository();
        var SUT = new WorkshopService(dataRepo);

        //ACT
        WorkOrder returnOrder = SUT.CreateWorkOrder("Fixa bromsar");
        List<WorkOrder> orders = SUT.GetOrders();

        //ASSERT
        Assert.Single(orders);
    }
}
