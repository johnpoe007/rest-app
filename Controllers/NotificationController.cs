using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace rest_app.Controllers;

[ApiController]
[Route("[controller]")]
public class Notification : ControllerBase
{
    private readonly ILogger<Notification> _logger;
    private const string _plexurl = "";
    private const string _webhookUrl = "";

    public Notification(ILogger<Notification> logger)
    {
        _logger = logger;
    }

    [HttpPost("GetSendDownNotification")]
    public async Task<IActionResult> SendDownNotification()
    {
        var client = new HttpClient();
        var check = await client.GetAsync(_plexurl);
        var Down = @"{
            ""content"": ""Hey <@181087250547736577> This is your friendly reminder that plex is down.\nMillions of people's entertainment depend on your plex server."",
            ""embeds"": [
                {
                ""title"": ""Plex is Down ⏰"",
                ""description"": ""Service Name: Plex\n\nService URL: plex.matthewglen.gay"",
                ""color"": 16711680,
                ""author"": {
                    ""name"": ""Uptime Kuma""
                },
                ""image"": {
                    ""url"": ""https://media0.giphy.com/media/9SIXFu7bIUYHhFc19G/giphy.gif?cid=ecf05e471fzzv6m0ht2ndm26cmubkq66qve93u8e5rpxcn6a&ep=v1_gifs_search&rid=giphy.gif&ct=g""
                    }
                }
            ],
            ""attachments"": []
        }";

        var Up = @"{
            ""content"": ""Hey @here This is your friendly reminder that plex is up.\nThank you for your continued patience have a great day."",
            ""embeds"": [
                {
                ""title"": ""Plex is Up 👏"",
                ""description"": ""Service Name: Plex\n\nService URL: plex.matthewglen.gay"",
                ""color"": 2752256,
                ""author"": {
                    ""name"": ""Uptime Kuma""
                },
                ""image"": {
                    ""url"": ""https://media0.giphy.com/media/f9MHrbOwcwx2UlIusn/giphy.gif?cid=ecf05e47n6agsu4gi26w68y0pz0d8mrelrojw9sfqx8btdie&ep=v1_gifs_search&rid=giphy.gif&ct=g""
                    }
                }
            ],
            ""attachments"": []
        }";

        var content = check.IsSuccessStatusCode ? Up : Down;
        
        var httpsContent = new StringContent(content, Encoding.UTF8, "application/json");
        var result = await client.PostAsync(_webhookUrl, httpsContent);
        return Ok(result);
    }

    /// <summary>
    /// Send a notification to discord
    /// </summary>
    /// <param name="message">The message to send</param>
    /// <returns></returns>
    [HttpPost("GetSendUpNotification")]
    public async Task<IActionResult> SendUpNotification()
    {
        var client = new HttpClient();
        // var content = $"This is the msg that was sent: {headerValue}";
            // var json = JsonSerializer.Serialize(content);
            var message = new Message();
            // message.content = headerValue.ToString();
            message.embeds = new Message.Embed[1];
            message.embeds[0] = new Message.Embed();
            message.embeds[0].title = "Plex is Down ⏰";
            message.embeds[0].description = "Service Name: Plex\n\nService URL: plex.matthewglen.gay";
            message.embeds[0].color = 16711680;
            message.embeds[0].author = new Message.Author();
            message.embeds[0].author.name = "Uptime Kuma";
            message.embeds[0].image = new Message.Image();
            message.embeds[0].image.url = "https://media0.giphy.com/media/9SIXFu7bIUYHhFc19G/giphy.gif?cid=ecf05e471fzzv6m0ht2ndm26cmubkq66qve93u8e5rpxcn6a&ep=v1_gifs_search&rid=giphy.gif&ct=g";
            var json = JsonSerializer.Serialize(message);
        
        var httpsContent = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await client.PostAsync(_webhookUrl, httpsContent);
        return Ok(result);
    }
}