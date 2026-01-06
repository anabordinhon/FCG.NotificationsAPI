# 🔔 FCG Notifications API

Microsserviço responsável pelo gerenciamento e envio de notificações do ecossistema **FIAP Cloud Games**.
Este projeto atua como **Consumer**, escutando eventos de integração (como o cadastro de usuários) via RabbitMQ e processando os disparos de notificação.

## 🚀 Tecnologias Utilizadas

* **Runtime:** .NET 8
* **Mensageria:** RabbitMQ (MassTransit)
* **Containerização:** Docker & Kubernetes (K8s)
* **Documentação:** Swagger / OpenAPI

## 🏗️ Arquitetura e Padrões

O projeto segue os princípios da **Clean Architecture** e foca no padrão de **Event-Driven Architecture**.

* **API:** Entry point para health checks e visualização (Swagger).
* **Application:** Consumers (MassTransit) e Serviços de Notificação.
* **Domain:** Modelos de eventos e regras de negócio.
* **Infrastructure:** Configurações do MassTransit e Conexões.

### Fluxo de Consumo de Notificação
1.  **RabbitMQ** recebe um evento (ex: `UserCreatedEvent`).
2.  **MassTransit Consumer** processa a mensagem da fila.
3.  **Service** executa a lógica de envio (Simulação de E-mail/SMS).
4.  **Logger** registra o sucesso da operação garantindo rastreabilidade.

---

## 📋 Pré-requisitos

Para executar este projeto localmente utilizando a infraestrutura automatizada, você precisará de:

1.  **Docker Desktop** instalado e rodando.
2.  **Kubernetes** habilitado nas configurações do Docker Desktop.
3.  **PowerShell** (para executar o script de deploy).
4.  **Infraestrutura Base:** O RabbitMQ deve estar rodando no cluster (geralmente iniciado pela `UsersAPI` ou script de infraestrutura comum).

---

## ⚡ Como Rodar (Deploy Automatizado)

Foi criado um script de automação (`deploy.ps1`) que realiza o build da imagem Docker e aplica as configurações do Kubernetes na ordem correta.

1.  Abra o PowerShell na raiz do projeto.
2.  Execute o script:

```powershell
.\deploy.ps1
```

**O que o script faz:**
* 🐳 **Build:** Cria a imagem `notifications-api:latest`.
* 🔐 **Configs:** Aplica **ConfigMaps** (Hosts) e **Secrets** (Senhas).
* 🚀 **App:** Realiza o deployment da aplicação (`Deployment`).
* 🌐 **Network:** Expõe o serviço via LoadBalancer (`Service`).
* 🔄 **Refresh:** Força a reinicialização dos Pods para carregar novas configurações.

---

## 🧪 Como Testar

Após o deploy ser concluído com sucesso:

### 1. Acessar a API (Swagger)
Para verificar se o serviço está rodando:
👉 **[http://localhost/swagger](http://localhost/swagger)**

*(Nota: Se a porta 80 estiver ocupada, verifique se o script redirecionou para a 3000 ou verifique o `service.yaml`).*

### 2. Verificar o Consumo
Para testar o fluxo real:
1.  Gere um evento através da **Users API** (criando um usuário).
2.  Observe os logs da **Notifications API** para ver o recebimento da mensagem.

---

## 🔍 Observabilidade e Logs

A aplicação implementa **Structured Logging** com foco em rastreabilidade distribuída. O `CorrelationId` recebido no evento é mantido para unificar o rastro entre os microsserviços.

### Padrões de Log Implementados (Requisitos):

1.  ✅ **Log de Consumo (Início):** Registra que a mensagem chegou do RabbitMQ.
    * *Mensagem:* `Mensagem recebida na fila. Event: UserCreatedEvent, CorrelationId: {Guid}`
2.  ✅ **Log de Sucesso:** Registra que a notificação foi "enviada".
    * *Mensagem:* `Notificação enviada com sucesso para: {Email}. CorrelationId: {Guid}`
3.  ✅ **Log de Erro:** Registra falhas no processamento da mensagem.
    * *Mensagem:* `Erro ao processar notificação. CorrelationId: {Guid}`
4.  🚫 **Log de Publicação:** **N/A (Não Aplicável)**.
    * *Nota:* Este microsserviço atua primariamente como Consumidor neste fluxo.

### Como ver os logs no Kubernetes:
Para acompanhar o consumo em tempo real:

```powershell
kubectl logs -l app=notifications-api -f
```

---

## 📂 Estrutura de Pastas (Kubernetes)

Os arquivos de manifesto do Kubernetes estão localizados na pasta `/k8s`:

* `configmap.yaml`: Endereço do Host do RabbitMQ.
* `secret.yaml`: Senha do RabbitMQ (Base64/Opaque).
* `deployment.yaml`: Definição do Pod, Réplicas e Variáveis de Ambiente.
* `service.yaml`: Configuração de Rede (LoadBalancer/ClusterIP).

---

## 📝 Evento de Integração

O contrato de evento esperado (`UserCreatedEvent`) deve ser idêntico ao publicado pela origem para garantir a desserialização correta:

```csharp
public class UserCreatedEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    
    // Rastreabilidade
    public Guid EventId { get; set; }
    public Guid CorrelationId { get; set; } // Usado para Log e Rastreamento
}
```
