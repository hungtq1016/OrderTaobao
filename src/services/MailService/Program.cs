using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using MassTransit;
using Microsoft.Extensions.Configuration;
using MailService.Infrastructure;
using Newtonsoft.Json;
using MailService.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var rabbitMQService = new RabbitMQService();

// Tạo một instance của EmailService để xử lý thông điệp nhận được từ RabbitMQ
var emailService = new BaseMailServices();

// Đăng ký EmailService như một IMessageHandler
rabbitMQService.ConsumeMessage(message =>
{
    var resetPasswordRequest = JsonConvert.DeserializeObject<ResetPasswordRequest>(message);

    // Xử lý thông điệp
    Console.WriteLine("Received ResetPasswordRequest");
    Console.WriteLine("UserId: {0}, Token: {1}", resetPasswordRequest.Email, resetPasswordRequest.Password);

    emailService.SendEmail(resetPasswordRequest.Email);
}, "ResetPasswordRequest");

rabbitMQService.ConsumeMessage(message =>
{
    var otp = JsonConvert.DeserializeObject<OTP>(message);

    // Xử lý thông điệp
    Console.WriteLine("Received ResetPasswordRequest");
    Console.WriteLine("UserId: {0}, Token: {1}", otp.Email, otp.Code);

    emailService.SendEmailOTP(otp);
}, "OTP");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
