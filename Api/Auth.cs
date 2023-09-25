namespace Tailwind.CMS.Api;
using Tailwind.CMS.Data;

public static class Auth{

  public static void MapRoutes(IEndpointRouteBuilder app, ContentLibrary lib)
  {

    app.MapGet("api/auth/send-code", (string dir) => {
      //this is where you hook up your email sender and send off the code
      Random gen = new Random();
      var code = gen.Next(111111, 999999);
      //pop this out to the logs so we can test...
      Console.WriteLine(code);
    });

    app.MapPost("api/auth/validate-code", (string dir) => {

    });
    
  }
  
}