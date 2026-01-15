Write-Host "--------------------------------------------------" -ForegroundColor Cyan
Write-Host "🚀 Iniciando Deploy do Notifications Event Processor" -ForegroundColor Cyan
Write-Host "--------------------------------------------------" -ForegroundColor Cyan

# 0. Build da imagem Docker
Write-Host "0. Construindo imagem Docker..." -ForegroundColor Yellow
cd C:\Estudos\tech-challeng-dois\FCG.NotificationsAPI\FCG.Notifications
docker build -t notifications-event-processor:latest -f FCG.Notifications.EventProcessor/Dockerfile .

# 1. Aplicar ConfigMap e Secrets
Write-Host "1. Aplicando Configurações (ConfigMap/Secrets)..." -ForegroundColor Yellow
kubectl apply -f k8s/configmap.yaml
kubectl apply -f k8s/secret.yaml

# 2. Aplicar Deployment
Write-Host "2. Aplicando Deployment..." -ForegroundColor Yellow
kubectl apply -f k8s/deployment.yaml

# 3. Aplicar Service (obrigatório por ementa)
Write-Host "3. Aplicando Service..." -ForegroundColor Yellow
kubectl apply -f k8s/service.yaml

# 4. Reiniciar Pods
Write-Host "4. Forçando atualização dos Pods..." -ForegroundColor Yellow
kubectl rollout restart deployment/notifications-processor-deployment

# 5. Validação do Status
Write-Host "--------------------------------------------------" -ForegroundColor Cyan
Write-Host "✅ Deploy enviado! Verificando status..." -ForegroundColor Cyan
Write-Host "--------------------------------------------------" -ForegroundColor Cyan

Start-Sleep -Seconds 3 

Write-Host "PODS ATIVOS:" -ForegroundColor Green
kubectl get pods -l app=notifications-processor

Write-Host "`nSTATUS DO SERVICE:" -ForegroundColor Green
kubectl get svc notifications-api

Write-Host "--------------------------------------------------" -ForegroundColor Cyan
Write-Host "DICA: Se o Service ficar com EXTERNAL-IP <pending>, isso é esperado no Docker Desktop." -ForegroundColor Gray
