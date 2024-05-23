using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductProvider.Infrastructure.Helper.Validations;
using ProductProvider.Infrastructure.Models;
using ProductProvider.Infrastructure.Services;
using System.Text;


namespace ProductProvider.Functions
{
    public class GenerateProductConfirmation(ILogger<GenerateProductConfirmation> logger, ProductService productService, ServiceBusClient serviceBusClient)
    {
        private readonly ILogger<GenerateProductConfirmation> _logger = logger;
        private readonly ProductService _productService = productService;
        private readonly ServiceBusClient _serviceBusClient = serviceBusClient;

        [Function("GenerateProductConfirmation")]
        public async Task <IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "productconfirm")] HttpRequest req)
        {
            try
            {
                var request = await new StreamReader(req.Body).ReadToEndAsync();
                var model = JsonConvert.DeserializeObject<ProductConfirmModel>(request); 
                if (model == null)
                {
                    return new BadRequestResult();
                }
                var modelState = CustomValidation.ValidateModel(model);
                if (!modelState.IsValid)
                {
                    return new BadRequestObjectResult(modelState.ValidationResults);
                }

                //spara till databas?

                var emailRequest = _productService.GenereateEmailRequest(model);
                if (emailRequest == null)
                {
                    return new BadRequestResult();
                }
                var sender = _serviceBusClient.CreateSender("email_request");
                await sender.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(emailRequest))));
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR : GenerateProductConfirmation.Run() :: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }
    }
}