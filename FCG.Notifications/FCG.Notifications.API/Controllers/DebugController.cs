using FCG.Notifications.Domain.Shared.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Notifications.API.Controllers
{
    [ApiController]
    [Route("api/debug")]
    public class DebugController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        // Injetamos a interface de publicação do MassTransit
        public DebugController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("test-user-created")]
        public async Task<IActionResult> TestUserCreated()
        {
            // Simula os dados que viriam da UsersAPI
            var fakeEvent = new UserCreatedEvent(
                UserId: Guid.NewGuid(),
                Email: "teste@email.com",
                FullName: "Desenvolvedor Teste",
                EventId: Guid.NewGuid(),
                CorrelationId: Guid.NewGuid()
            );

            // Publica no RabbitMQ
            await _publishEndpoint.Publish(fakeEvent);

            return Ok(new { Message = "Evento UserCreatedEvent publicado!", EventData = fakeEvent });
        }
    }
}