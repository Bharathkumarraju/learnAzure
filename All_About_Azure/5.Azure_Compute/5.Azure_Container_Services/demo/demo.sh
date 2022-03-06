azure contianer services

Service to connect pod to outside world.

docker run --rm -d  -p 5004:5004/tcp learnazure:latest

> Executing task: docker push bharathreaditacr.azurecr.io/learnazure:latest <

The push refers to repository [bharathreaditacr.azurecr.io/learnazure]
351989a1ea53: Pushed 
5f70bf18a086: Pushed 
4ae3ab281ac5: Pushed 
66f142d92dae: Pushed 
c7600186ee9a: Pushed 
55cb27085b24: Pushed 
bac5efef13fe: Pushed 
e5baccb54724: Pushed 
latest: digest: sha256:ddde2c6d4bf57c0ebc05967d3d89f844507cb65b95d036595bfc8e27e1c1a3e7 size: 2000


az account set --subscription 1cb67706-f0c0-4a7c-9940-4d9779fbce91

az aks create --resource-group readit-app-rg --name bkcart-aks --node-count 1 --generate-ssh-keys --attach-acr readitacr --node-vm-size Standard_DS1_v2

MacBook-Pro:cart bharathdasaraju$ az aks create --resource-group readit-app-rg --name bkcart-aks --node-count 2 --generate-ssh-keys --attach-acr readitacr --node-vm-size Standard_DS2_v2 --location eastasia
 / Running ..
 MacBook-Pro:cart bharathdasaraju$

az aks get-credentials --resource-group readit-app-rg --name bkcart-aks


MacBook-Pro:cart bharathdasaraju$ az aks install-cli
Downloading client to "/usr/local/bin/kubectl" from "https://storage.googleapis.com/kubernetes-release/release/v1.23.4/bin/darwin/amd64/kubectl"
Please ensure that /usr/local/bin is in your search PATH, so the `kubectl` command can be found.
Downloading client to "/var/folders/9z/70m5x6qx2ts90q68h26f17q80000gq/T/tmp8w04sfa7/kubelogin.zip" from "https://github.com/Azure/kubelogin/releases/download/v0.0.11/kubelogin.zip"
Please ensure that /usr/local/bin is in your search PATH, so the `kubelogin` command can be found.
MacBook-Pro:cart bharathdasaraju$ 


MacBook-Pro:cart bharathdasaraju$ az aks get-credentials --resource-group readit-app-rg --name bkcart-aks
Merged "bkcart-aks" as current context in /Users/bharathdasaraju/.kube/config
MacBook-Pro:cart bharathdasaraju$ kubectl get nodes -o wide
NAME                                STATUS   ROLES   AGE     VERSION   INTERNAL-IP   EXTERNAL-IP   OS-IMAGE             KERNEL-VERSION     CONTAINER-RUNTIME
aks-nodepool1-38245346-vmss000000   Ready    agent   3m23s   v1.21.9   10.240.0.4    <none>        Ubuntu 18.04.6 LTS   5.4.0-1069-azure   containerd://1.4.12+azure-2
aks-nodepool1-38245346-vmss000001   Ready    agent   3m12s   v1.21.9   10.240.0.5    <none>        Ubuntu 18.04.6 LTS   5.4.0-1069-azure   containerd://1.4.12+azure-2
MacBook-Pro:cart bharathdasaraju$ 

MacBook-Pro:cart bharathdasaraju$ kubectl apply -f deployment.yaml 
deployment.apps/readit-cart created
service/readit-cart created
MacBook-Pro:cart bharathdasaraju$ 
