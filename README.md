🔔 **FCG Notifications Worker**

Microsserviço especializado no processamento assíncrono de notificações do ecossistema **FIAP Cloud Games**. Este projeto opera como um **Worker Service** (background processor), utilizando **MassTransit** para consumir eventos de integração e orquestrar disparos de e-mails via Handlers desacoplados.

* * * * *

⚙️ **Configurações (Variáveis de Ambiente)**

O Worker utiliza as seguintes variáveis para se comunicar com o Broker. Estas variáveis são configuradas via ConfigMap e Secret no ambiente de orquestração:

-   **RabbitMq__Host**: Endereço/DNS do servidor RabbitMQ (Configurado via ConfigMap). Exemplo: rabbitmq.

-   **RabbitMq__Username**: Usuário para autenticação no Broker (Configurado via ConfigMap). Exemplo: guest.

-   **RabbitMq__Password**: Senha para autenticação no Broker (Dado Sensível configurado via Secret).

* * * * *

🏗️ **Arquitetura e Padrões (Ports & Adapters)**

O projeto foi construído seguindo os princípios de **Clean Architecture** e **CQRS**, garantindo que a lógica de notificação seja independente do provedor de mensageria.

-   **Infrastructure (Consumers):** Adaptadores de entrada que escutam o RabbitMQ e convertem eventos em Comandos de aplicação.

-   **Application (Commands/Handlers):** Orquestração da lógica de negócio via ICommandHandler<T>.

-   **Domain (Events):** Contratos de integração sincronizados com os microsserviços de origem para garantir a integridade da desserialização.

* * * * *

🚀 **Fluxos de Notificação Implementados**

Atualmente, o Worker processa dois fluxos críticos de negócio de forma totalmente desacoplada:

1.  **Boas-vindas (Welcome Email):**

-   Evento: UserCreatedEvent (Publicado pela Users API).

-   Ação: Disparo de e-mail de boas-vindas após a criação bem-sucedida de uma conta.

1.  **Confirmação de Compra (Purchase Confirmation):**

-   Evento: PaymentProcessedEvent (Publicado pela Payments API).

-   Ação: Envio de recibo e confirmação de liberação de jogo após a aprovação do pagamento.

* * * * *

🛠️ **Tecnologias Utilizadas**

-   Runtime: .NET 8 (Worker Service)

-   Mensageria: RabbitMQ + MassTransit

-   Padrões: CQRS, Ports & Adapters, SOLID, Imutabilidade

-   Containerização: Docker & Kubernetes (K8s)

* * * * *

⚡ **Como Rodar (Orquestração Local)**

**Via Docker Compose** Na raiz do projeto orquestrador, execute o comando para subir o serviço: `docker-compose up -d fcg-notifications-processor`

**Via Kubernetes (Deploy Automatizado)** Utilize o script PowerShell na raiz do repositório: `.\orchestrator.ps1`

* * * * *

🔍 **Observabilidade e Logs Estruturados**

A aplicação utiliza **Structured Logging** para garantir a rastreabilidade distribuída. O **CorrelationId** recebido no evento de usuário é mantido para unificar o rastro entre os microsserviços.

**Exemplos de Logs de Sucesso:**

-   [Sucesso] E-mail enviado | Template: Welcome | UserId: {Guid} | Para: {Email}

-   [Sucesso] E-mail enviado | Template: PurchaseConfirmation | OrderId: {Guid} | Status: Approved

* * * * *

📝 **Contratos de Integração (Events)**

**PaymentProcessedEvent**

C#

```
public class PaymentProcessedEvent
{
    public Guid OrderId { get; init; }
    public int UserId { get; init; }
    public Guid GameId { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime ProcessedAt { get; init; }
}

```

**UserCreatedEvent**

C#

```
public class UserCreatedEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid EventId { get; set; }
    public Guid CorrelationId { get; set; }
}

```

* * * * *

📂 **Estrutura de Manifestos (K8s)**

Os arquivos de infraestrutura estão localizados na pasta **/k8s**:

-   **deployment.yaml**: Define o Pod com imagePullPolicy: Never.

-   **configmap.yaml**: Centraliza o Host do RabbitMQ (rabbitmq).

-   **secret.yaml**: Armazena credenciais do RabbitMQ (RabbitMq__Password).
