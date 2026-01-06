# deploy.ps1

Write-Host "--------------------------------------------------" -ForegroundColor Cyan
Write-Host "🚀 Iniciando Deploy da Notifications API (Pasta k8s/)" -ForegroundColor Cyan
Write-Host "--------------------------------------------------" -ForegroundColor Cyan

# 1. Aplicar ConfigMap e Secrets (Caminho k8s/...)
Write-Host "1. Aplicando Configurações..." -ForegroundColor Yellow
kubectl apply -f k8s/configmap.yaml
kubectl apply -f k8s/secret.yaml

# 2. Aplicar Deployment
Write-Host "2. Aplicando Deployment..." -ForegroundColor Yellow
kubectl apply -f k8s/deployment.yaml

# 3. Aplicar Service
Write-Host "3. Aplicando Service..." -ForegroundColor Yellow
kubectl apply -f k8s/service.yaml

# 4. Reiniciar Pods (O nome do deployment não muda, pois já está no cluster)
Write-Host "4. Forçando atualização dos Pods..." -ForegroundColor Yellow
kubectl rollout restart deployment/notifications-api-deployment

# 5. Validação
Write-Host "--------------------------------------------------" -ForegroundColor Cyan
Write-Host "✅ Deploy enviado! Verificando status..." -ForegroundColor Cyan
Write-Host "--------------------------------------------------" -ForegroundColor Cyan

# Pequena pausa para o K8s processar
Start-Sleep -Seconds 2 

Write-Host "PODS:" -ForegroundColor Green
kubectl get pods -l app=notifications-api

Write-Host "`nSERVICES:" -ForegroundColor Green
kubectl get svc notifications-api
