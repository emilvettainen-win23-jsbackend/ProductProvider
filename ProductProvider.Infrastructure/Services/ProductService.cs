using Microsoft.Extensions.Logging;
using ProductProvider.Infrastructure.Models;

namespace ProductProvider.Infrastructure.Services;

public class ProductService
{
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public EmailRequestModel GenereateEmailRequest(ProductConfirmModel model)
    {
		try
		{
            var emailRequest = new EmailRequestModel
            {
                To = model.Email,
                Subject = $"Confirmation of your booking: {model.CourseTitle}",
                HtmlBody = $@"<html lang='en'>
                                <head>
                                <meta charset='UTF-8'>
                                <meta name='viewport' content='with=device-width, initial-scale=1.0'>
                                <title>Course booking confirmation</title>
                                </head>
                                <body>
                                    <div style='max-width: 600px; margin: 20px auto; padding: 20px; background-color: #ffffff;'>
                                        <div style='background-color: #0046ae; color: white; padding: 10px 20px; text-align: center;'>
                                            <h1>Booking</h1>
                                        </div>
                                        <div style='padding: 20px;'>
                                            <p>Hello, {model.FirstName}</p>
                                            <p>Thank you for booking the course: {model.CourseTitle}, by:{model.AuthorName}.</p>
                                            <p>If you did not book this course, please ignore this email.</p>
                                            <p>Thank you!<br>Silicon</p>
                                        </div>
                                    </div>
                                </body>
                                </html>",
                PlainText = $"Hello, Thank you for booking the course: {model.CourseTitle}, by:{model.AuthorName}. If you did not book this course, please ignore this email. Thank you! Silicon"
            };
            return emailRequest;
        }
		catch (Exception ex)
		{
            _logger.LogError($"ERROR : ProductService.GenereateEmailRequest() :: {ex.Message}");
        }
		return null!;
    }
}
