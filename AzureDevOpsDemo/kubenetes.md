#Installation

Download and run the installer for the latest release.
Or if using PowerShell, use this command:

`New-Item -Path 'c:\' -Name 'minikube' -ItemType Directory -Force
Invoke-WebRequest -OutFile 'c:\minikube\minikube.exe' -Uri 'https://github.com/kubernetes/minikube/releases/latest/download/minikube-windows-amd64.exe' -UseBasicParsing`


Add the minikube.exe binary to your PATH.
Make sure to run PowerShell as Administrator.

`$oldPath = [Environment]::GetEnvironmentVariable('Path', [EnvironmentVariableTarget]::Machine)
if ($oldPath.Split(';') -inotcontains 'C:\minikube'){
  [Environment]::SetEnvironmentVariable('Path', $('{0};C:\minikube' -f $oldPath), [EnvironmentVariableTarget]::Machine)
}
`

#Minikube

Start 
`minikube start --driver=docker`

Show pods
`kubectl get pod`
`kubectl get pod -o wide`

Show nodes
`kubectl get nodes`

Show pods
`kubectl get node`

Show services
`kubectl get services`

Create a deployment
`kubectl create deployment demo1 --image=thanhnmitc/webapi1`

Edit a deployment
`kubectl edit deployment demo1`

Show deployments
`kubectl get deployment`

Get Deployment info from etsd
`kubectl get deployment [deployment-name] -o yaml > [filename.yaml]`

Show replicaset
`kubectl get replicaset`

Debug pod
`kubectl logs [pod-name]`

Open bash in pod
`kubectl exec -it demo1-7c8d648c8f-jqw9w -- bash`

Exit bash in pod
`exit`

Delete deployment
`kubectl delete deployment [deployment name]`

Apply config file for 1 deployment
`kubectl apply -f [filename.yaml]`

Create secret from pfx
`kubectl create secret generic aspnetapppfx --from-file=pfx-cert=./certificates/aspnetapp.pfx`

Create configMap from pfx
`kubectl create configmap aspnetappcer --from-file=cert.pfx=./certificates/aspnetapp.pfx`