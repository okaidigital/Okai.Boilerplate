using System.Net;
using Okai.Boilerplate.Application.DTOs.Base;

namespace Okai.Boilerplate.Application.Tests;

[TestClass]
public sealed class SuccessResponseDtoTests
{
    [TestMethod]
    public void Constructor_WithStatusCodeMarksResponseAsSucceeded()
    {
        var response = new SuccessResponseDto(HttpStatusCode.OK);

        Assert.IsTrue(response.Succeeded);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(string.Empty, response.Message);
    }

    [TestMethod]
    public void GenericConstructor_PreservesData()
    {
        var data = new SampleResponse("done");

        var response = new SuccessResponseDto<SampleResponse>(HttpStatusCode.OK, data);

        Assert.IsTrue(response.Succeeded);
        Assert.AreSame(data, response.Data);
    }

    private sealed record SampleResponse(string Message);
}
